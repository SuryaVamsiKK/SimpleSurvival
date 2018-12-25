using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
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

    public float noiseX, noiseZ;
    
    [Header("Color Values")]
    public Gradient TerrianColor;
    public bool FlatShadeCheck;

    float MinNoiseHeight = 0f;
    float MaxNoiseHeight = 1f;

    [Header("Disease Values")]
    public Gradient DiseaseColor;
    public Vector3 DiseasePos;
    public float DiseaseRadius;

    [Header("Tree")]
    [Header("Prefabs")]
    public GameObject Tree;
    public Vector2 TreeNodeHeight;
    public float TreeProab;

    [Header("Stone")]
    public GameObject Stone;
    public Vector2 StoneNodeHeight;
    public float StoneProab;

    [Header("Ore")]
    public GameObject Ore;
    public Vector2 OreNodeHeight;
    public float OreProab;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        CreateShape(scale, height, octaves, persistance, lacunarity);
        UpdateMesh();
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().convex = false;
    }

    void CreateShape(float scale, float height, int octaves, float persistance, float lacunarity)
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
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

        //for (int i = 0; i < (xSize + 1) * (zSize + 1); i++)
        //{
        //    vertices[i].y = Mathf.InverseLerp(MinNoiseHeight, MaxNoiseHeight, vertices[i].y);
        //}

        triangles = new int[xSize * zSize * 6];

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

                vert++;
                tris += 6;
            }
            vert++;
        }

        Debug.Log(vertices.Length);

        if (FlatShadeCheck)
        {
            FlatShade();
        }
        
        colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            float yPose = Mathf.InverseLerp(MinNoiseHeight ,MaxNoiseHeight ,vertices[i].y);

            colors[i] = TerrianColor.Evaluate(yPose);

            if (Vector3.Distance(DiseasePos, vertices[i]) > DiseaseRadius)
            {
                float yPoseD = Mathf.InverseLerp(MinNoiseHeight, MaxNoiseHeight, vertices[i].y);
                colors[i] = DiseaseColor.Evaluate(yPoseD) * TerrianColor.Evaluate(yPose);
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
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
        if(vert.y > TreeNodeHeight.x && vert.y < TreeNodeHeight.y && Random.Range(0,TreeProab) < 10)
        {
            GameObject g = Instantiate(Tree, transform);
            g.transform.position = vert;
        }
    }
}
