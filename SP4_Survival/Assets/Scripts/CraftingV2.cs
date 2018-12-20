using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingV2 : MonoBehaviour
{
    public List<inventoryItems> Bag = new List<inventoryItems>();
    public KeyCode placeCraftable;
    public Recipies obj;
    GameObject craftedObj;
    GameObject player;
    public float craftDistance;
    bool placed = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        Bag = InventoryV2.instance.Bag;

        if(Input.GetKeyDown(obj.access))
        {
            bool[] collectedReources;
            collectedReources = new bool[obj.requiredResources.Length];

            if (Bag.Count == 0)
            {
                Debug.Log("Your Inventory is empty");
            }

            else
            {
                for (int i = 0; i < Bag.Count; i++)
                {
                    for (int j = 0; j < obj.requiredResources.Length; j++)
                    {
                        if (Bag[i].item.typeOfResource == obj.requiredResources[j].requiredResource)
                        {
                            if (Bag[i].amount >= obj.requiredResources[j].requiredAmount)
                            {
                                collectedReources[j] = true;
                            }
                            else
                            {
                                collectedReources[j] = false;
                            }
                        }
                    }
                }


                for (int i = 0; i < Bag.Count; i++)
                {
                    for (int j = 0; j < obj.requiredResources.Length; j++)
                    {
                        if (Bag[i].item.typeOfResource == obj.requiredResources[j].requiredResource)
                        {
                            if (Bag[i].amount >= obj.requiredResources[j].requiredAmount)
                            {
                                for (int k = 0; k < collectedReources.Length; k++)
                                {
                                    if (k == collectedReources.Length - 1 && collectedReources[k] == true)
                                    {
                                        Bag[i].amount -= obj.requiredResources[j].requiredAmount;
                                    }
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < collectedReources.Length; i++)
                {
                    if (collectedReources[i] == false)
                    {
                        break;
                    }
                    if (i == collectedReources.Length - 1 && collectedReources[i] == true)
                    {
                        Craft(obj.craftable);
                    }
                }

                if (craftedObj == null)
                {
                    Debug.Log("Crafting : " + obj.craftable.name + " : failed.");
                }
            }
        }

        positionCraftable();
        PlaceCraftable();
    }

    void Craft(GameObject craftable)
    {
        craftedObj = Instantiate(craftable);
    }

    void positionCraftable()
    {
        if(craftedObj != null)
        {
            if (!placed)
            {
                craftedObj.transform.position = player.transform.GetChild(0).position + player.transform.GetChild(0).forward * 2;
                craftedObj.transform.forward = player.transform.GetChild(0).transform.forward * craftDistance;
            }
        }
    }

    void PlaceCraftable()
    {
        if(Input.GetKeyDown(placeCraftable))
        {
            placed = !placed;
            craftedObj = null;
        }
    }
}
