using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvetoryToUI : MonoBehaviour
{
    public GameObject InventoryPanel;
    public inventoryReaderV2 inventoryScript;

    void Start()
    {
        inventoryScript = GameObject.FindGameObjectWithTag("Inventory").GetComponent<inventoryReaderV2>();
    }

    //was originally as updateUI.
    public void Update()
    {
        inventoryScript.DisplayUI();

        for (int i = 0; i < InventoryPanel.transform.GetChild(1).childCount; i++)
        {
            if (i > inventoryScript.inventorycount-1)
            {
                InventoryPanel.transform.GetChild(1).GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            if (i < inventoryScript.inventorycount)
            {
                InventoryPanel.transform.GetChild(1).GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }

        if (inventoryScript.BackPack.Length > 0)
        {
            int counter = 0;
            for (int i = 0; i < inventoryScript.BackPack.Length; i++)
            {
                for (int j = 0; j < inventoryScript.BackPack[i].pack.Length; j++)
                {
                    InventoryPanel.transform.GetChild(1).GetChild(counter + j).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = inventoryScript.BackPack[i].pack[j].ToString();
                    InventoryPanel.transform.GetChild(1).GetChild(counter + j).GetChild(0).GetChild(0).GetComponent<ReourceHolder>().typeOf = inventoryScript.BackPack[i].name;
                    InventoryPanel.transform.GetChild(1).GetChild(counter + j).GetChild(0).GetChild(0).GetComponent<ReourceHolder>().amt = inventoryScript.BackPack[i].pack[j];
                    InventoryPanel.transform.GetChild(1).GetChild(counter + j).GetChild(0).GetChild(2).GetComponent<Image>().sprite = inventoryScript.BackPack[i].icon;
                    InventoryPanel.transform.GetChild(1).GetChild(counter + j).GetChild(0).GetChild(0).GetComponent<ReourceHolder>().resource = inventoryScript.BackPack[i].resource;
                    //Debug.Log(inventoryScript.BackPack[i].pack[j].ToString() + " :: " + inventoryScript.BackPack[i].name + " :: " + (sec + j));
                }
                counter += inventoryScript.BackPack[i].pack.Length;
            }
        }
    }
}
