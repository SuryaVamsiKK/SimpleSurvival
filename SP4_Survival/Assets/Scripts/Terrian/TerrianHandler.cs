using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrianHandler : MonoBehaviour
{
    public float X;
    public float Z;

    private void Awake()
    {
        X = Random.Range(-500000, 500000);
        Z = Random.Range(-500000, 500000);
        NoiseAssign();
    }

    void OnValidate()
    {
        NoiseAssign();
    }

    void NoiseAssign()
    {

        this.transform.GetChild(0).GetComponent<MeshGenerator>().noiseX = X;
        this.transform.GetChild(0).GetComponent<MeshGenerator>().noiseZ = Z;

        this.transform.GetChild(1).GetComponent<MeshGenerator>().noiseX = (X/2) + 50;
        this.transform.GetChild(1).GetComponent<MeshGenerator>().noiseZ = (Z / 2) + 50;

        this.transform.GetChild(2).GetComponent<MeshGenerator>().noiseX = (X / 2) + 50;
        this.transform.GetChild(2).GetComponent<MeshGenerator>().noiseZ = (Z / 2);

        this.transform.GetChild(3).GetComponent<MeshGenerator>().noiseX = (X / 2) + 50;
        this.transform.GetChild(3).GetComponent<MeshGenerator>().noiseZ = (Z / 2) - 50;

        this.transform.GetChild(4).GetComponent<MeshGenerator>().noiseX = (X / 2);
        this.transform.GetChild(4).GetComponent<MeshGenerator>().noiseZ = (Z / 2) + 50;

        this.transform.GetChild(5).GetComponent<MeshGenerator>().noiseX = (X / 2);
        this.transform.GetChild(5).GetComponent<MeshGenerator>().noiseZ = (Z / 2) - 50;

        this.transform.GetChild(6).GetComponent<MeshGenerator>().noiseX = (X / 2) - 50;
        this.transform.GetChild(6).GetComponent<MeshGenerator>().noiseZ = (Z / 2) + 50;

        this.transform.GetChild(7).GetComponent<MeshGenerator>().noiseX = (X / 2) - 50;
        this.transform.GetChild(7).GetComponent<MeshGenerator>().noiseZ = (Z / 2);

        this.transform.GetChild(8).GetComponent<MeshGenerator>().noiseX = (X / 2) - 50;
        this.transform.GetChild(8).GetComponent<MeshGenerator>().noiseZ = (Z / 2) - 50;


    }
}
