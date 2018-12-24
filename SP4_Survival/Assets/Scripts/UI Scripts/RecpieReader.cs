using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecpieReader : MonoBehaviour
{

    public GameObject Slot;
    public Recipies recpie; 

    void Start()
    {
        
    }
    
    void Update()
    {
        for (int i = 0; i < recpie.requiredResources.Length; i++)
        {
            if(transform.GetChild(0).GetChild(1).childCount < recpie.requiredResources.Length)
            {
                GameObject g = Instantiate(Slot, transform.GetChild(0).GetChild(1));
                g.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = recpie.requiredResources[i].requiredAmount.ToString();
                g.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = recpie.requiredResources[i].requiredResource.icon;
            }
        }
    }
}
