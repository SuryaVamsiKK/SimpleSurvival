using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBench : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        if (GetComponent<ScriptActiivation>().interacted)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().InventoryStatus = true;
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
            GetComponent<ScriptActiivation>().interacted = false;
        }
    }
}
