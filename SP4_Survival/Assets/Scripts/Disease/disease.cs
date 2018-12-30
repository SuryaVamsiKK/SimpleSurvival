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
        this.transform.position = new Vector3(Random.Range(radius, (range.xSize/2) + ((range.xSize / 2) - radius)), 0, Random.Range(radius, (range.xSize / 2) + ((range.xSize / 2) - radius)));
    }
    
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position.y, transform.position.z);


        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Melee>().maskWearing == null || GameObject.FindGameObjectWithTag("Player").GetComponent<Melee>().maskWearing.AccessLevel != MaskLevel.Level2)
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position, this.transform.position) > radius)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().radiation += Time.deltaTime * rateOfRadiation;
            }
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).position, this.transform.position) < radius)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().radiation -= Time.deltaTime * rateOfRadiation;
            }
        }

        if (radius > 0)
        {
            radius -= Time.deltaTime * rateOfReduction;
        }        
    }
}
