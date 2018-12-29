using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;
    Mesh Colmesh;

    Vector3[] vertices;
    Vector3[] nonFlatShadedVerts;
    int[] triangles;
    int[] nonFlatShadedtriangles;
    Color[] colors;


    public int xSize = 20;
    public int zSize = 20;

    [Header("Noise Values")]
    public float scale = 0.3f;
    public float height = 4f;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public AnimationCurve multiplier;
    public bool editTerrian;

    public float noiseX, noiseZ;
    
    [Header("Color Values")]
    public Gradient TerrianColor;
    public bool FlatShadeCheck;

    float MinNoiseHeight = 0f;
    float MaxNoiseHeight = 1f;

    [Header("Disease Values")]
    public Transform diseaseObj;
    public Gradient DiseaseColor;
    public Vector3 diseasePos; 
    public float diseaseRadius;

    [Header("Prefabs")]
    public Node[] nodes;

    void OnValidate()
    { 
        if(!Application.isPlaying)
        {
            GetComponent<MeshCollider>().enabled = false;
            mesh = new Mesh();
            Colmesh = new Mesh();
            CreateShape(scale, height, octaves, persistance, lacunarity);
            UpdateMesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }       
    }

    private void Start()
    {
        mesh = new Mesh();
        Colmesh = new Mesh();

        CreateShape(scale, height, octaves, persistance, lacunarity);
        UpdateMesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GetComponent<MeshCollider>().enabled = true;
        GetComponent<MeshCollider>().sharedMesh = Colmesh;
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().convex = false;
        diseasePos = diseaseObj.position;
    }

    private void Update()
    {
        diseaseRadius = diseaseObj.GetComponent<disease>().radius;
        ColorTerrian();
    }

    void CreateShape(float scale, float height, int octaves, float persistance, float lacunarity)
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        nonFlatShadedVerts = new Vector3[(xSize + 1) * (zSize + 1)];
        
        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int o = 0; o < octaves; o++)
                {
                    float y = Mathf.PerlinNoise((x + noiseX) / scale * frequency, (z + noiseZ) / scale * frequency) * 2 - 1;
                    noiseHeight += y * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if(noiseHeight > MaxNoiseHeight)
                {
                    MaxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < MinNoiseHeight)
                {
                    MinNoiseHeight = noiseHeight;
                }
                vertices[i] = new Vector3(x, multiplier.Evaluate(noiseHeight) * height, z);
                NodeSpwaner(vertices[i]);
                i++;
            }
        }
        
        triangles = new int[xSize * zSize * 6];
        nonFlatShadedtriangles = new int[xSize * zSize * 6];

        nonFlatShadedVerts = vertices;

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                
                nonFlatShadedtriangles[tris + 0] = triangles[tris + 0];
                nonFlatShadedtriangles[tris + 1] = triangles[tris + 1];
                nonFlatShadedtriangles[tris + 2] = triangles[tris + 2];
                nonFlatShadedtriangles[tris + 3] = triangles[tris + 3];
                nonFlatShadedtriangles[tris + 4] = triangles[tris + 4];
                nonFlatShadedtriangles[tris + 5] = triangles[tris + 5];

                vert++;
                tris += 6;
            }
            vert++;
        }
        
        if (FlatShadeCheck)
        {
            FlatShade();
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        if (Application.isPlaying)
        {
            Debug.Log(nonFlatShadedVerts.Length + " : " + nonFlatShadedtriangles.Length + " -> Collider Mesh");
            Debug.Log(vertices.Length + " : " + triangles.Length + " -> Display Mesh");
            Colmesh.Clear();

            Colmesh.vertices = nonFlatShadedVerts;
            Colmesh.triangles = nonFlatShadedtriangles;

            Colmesh.RecalculateNormals();
        }
    }
    
    void ColorTerrian()
    {
        colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            float yPose = Mathf.InverseLerp(MinNoiseHeight, MaxNoiseHeight, vertices[i].y);

            colors[i] = TerrianColor.Evaluate(yPose);

            if (Vector3.Distance(diseasePos, vertices[i]) > diseaseRadius)
            {
                float yPoseD = Mathf.InverseLerp(MinNoiseHeight, MaxNoiseHeight, vertices[i].y);
                colors[i] = DiseaseColor.Evaluate(yPoseD) * TerrianColor.Evaluate(yPose);
            }
        }
        if (mesh != null)
        {
            mesh.colors = colors;
        }
    }

    void FlatShade()
    {
        Vector3[] flatShadeVerts = new Vector3[triangles.Length];

        for (int i = 0; i < triangles.Length; i++)
        {
            flatShadeVerts[i] = vertices[triangles[i]];
            triangles[i] = i;
        }

        vertices = flatShadeVerts;
    }

    void NodeSpwaner(Vector3 vert)
    {
        if(Application.isPlaying)
        for (int i = 0; i < nodes.Length; i++)
        {
            if (vert.y > nodes[i].ObjectNodeHeight.x && vert.y < nodes[i].ObjectNodeHeight.y && Random.Range(0, nodes[i].ObjectProbab) < 2)
            {
                GameObject g = Instantiate(nodes[i].Object, transform);
                g.transform.position = vert;
            }
        }       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (!Application.isPlaying)
        {
            diseasePos = diseaseObj.position;
            diseaseRadius = diseaseObj.GetComponent<disease>().radius;
            ColorTerrian();
        }
        Gizmos.DrawWireSphere(diseasePos, 2f);
    }
}

[System.Serializable]
public class Node
{
    public GameObject Object;
    public Vector2 ObjectNodeHeight;
    public float ObjectProbab;
}
