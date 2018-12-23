﻿using System.Collections;
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
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetChild(0).GetChild(3).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetChild(0).GetChild(4).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().InventoryStatus = true;
            GetComponent<ScriptActiivation>().interacted = false;
        }
    }
}