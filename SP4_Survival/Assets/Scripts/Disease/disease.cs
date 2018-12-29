using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disease : MonoBehaviour
{
    public float radius;
    public float rateOfRadiation;
    public float rateOfReduction = 1;
    public MeshGenerator range;

    void Awake()
    {
        this.transform.position = new Vector3(Random.Range(0, range.xSize), 0, Random.Range(0, range.zSize));
    }
    
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position.y, transform.position.z);

        if(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position, this.transform.position) > radius)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().radiation += Time.deltaTime * rateOfRadiation;
        }
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position, this.transform.position) < radius)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().radiation -= Time.deltaTime * rateOfRadiation;
        }

        if (radius > 0)
        {
            radius -= Time.deltaTime * rateOfReduction;
        }        
    }
}
