using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryReader : MonoBehaviour
{
    public GameObject inventoryBelt;
    public Inventory Inventory;

    public GameObject WOODUI;
    public GameObject STONEUI;

    void Update()
    {
        if (Inventory.items.Count > 0)
        {
            for (int i = 0; i < Inventory.items.Count; i++)
            {
                if (Inventory.items[i].item.typeOfItem == itemType.WOOD)
                {
                    if (inventoryBelt.transform.GetChild(i).childCount <= 0)
                    {
                        GameObject g = Instantiate(WOODUI, inventoryBelt.transform.GetChild(i));
                        g.transform.localScale = Vector3.one;
                        g.transform.localPosition = new Vector3(10, 0, 0);
                        g.GetComponent<TextMeshProUGUI>().text = Inventory.items[i].amount.ToString();
                    }
                    else
                    {
                        inventoryBelt.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Inventory.items[i].amount.ToString();
                    }
                }

                if (Inventory.items[i].item.typeOfItem == itemType.STONE)
                {
                    if (inventoryBelt.transform.GetChild(i).childCount <= 0)
                    {
                        GameObject g = Instantiate(STONEUI, inventoryBelt.transform.GetChild(i));
                        g.transform.localScale = Vector3.one;
                        g.transform.localPosition = new Vector3(10, 0, 0);
                        g.GetComponent<TextMeshProUGUI>().text = Inventory.items[i].amount.ToString();
                    }
                    else
                    {
                        inventoryBelt.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Inventory.items[i].amount.ToString();
                    }
                }
            }

            for (int i = 0; i < Inventory.inventorySpace; i++)
            {
                if (i == Inventory.items.Count)
                {
                    if (inventoryBelt.transform.GetChild(i).childCount > 0)
                    {
                        Destroy(inventoryBelt.transform.GetChild(i).GetChild(0).gameObject);
                    }
                }
            }
        }
    }
}
