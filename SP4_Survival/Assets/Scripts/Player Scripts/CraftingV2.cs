 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingV2 : MonoBehaviour
{
    public List<inventoryItems> Bag = new List<inventoryItems>();
    public KeyCode placeCraftable;

    GameObject craftedObj;
    GameObject player;
    public float craftDistance;
    public bool placed = false;
    public LayerMask layers;

    bool placeable = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        Bag = InventoryV2.instance.Bag;
        
        if (craftedObj != null)
        {
            positionCraftable();
            PlaceCraftable();
        }
    }

    public void Craft(Recipies obj)
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
                    if (Bag[i].item.typeOfResource == obj.requiredResources[j].requiredResource.typeOfResource)
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
                    if (Bag[i].item.typeOfResource == obj.requiredResources[j].requiredResource.typeOfResource)
                    {
                        if (Bag[i].amount >= obj.requiredResources[j].requiredAmount)
                        {
                            for (int k = 0; k < collectedReources.Length; k++)
                            {
                                if (collectedReources[k] == false)
                                {
                                    break;
                                }
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
                    craftedObj = Instantiate(obj.craftable);
                }
            }

            if (craftedObj == null)
            {
                Debug.Log("Crafting : " + obj.craftable.name + " : failed.");
            }
        }

        player.transform.GetComponent<PlayerStats>().InventoryStatus = false;
    }

    public void CraftNonPlaceable(MeleeWeapons obj)
    {

        bool[] collectedReources;
        bool allowSubtract = false;
        collectedReources = new bool[obj.recpie.requiredResources.Length];

        if (Bag.Count == 0)
        {
            Debug.Log("Your Inventory is empty");
        }

        else
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                for (int j = 0; j < obj.recpie.requiredResources.Length; j++)
                {
                    if (Bag[i].item.typeOfResource == obj.recpie.requiredResources[j].requiredResource.typeOfResource)
                    {
                        if (Bag[i].amount >= obj.recpie.requiredResources[j].requiredAmount)
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
                for (int j = 0; j < obj.recpie.requiredResources.Length; j++)
                {
                    if (Bag[i].item.typeOfResource == obj.recpie.requiredResources[j].requiredResource.typeOfResource)
                    {
                        if (Bag[i].amount >= obj.recpie.requiredResources[j].requiredAmount)
                        {
                            for (int k = 0; k < collectedReources.Length; k++)
                            {
                                if(collectedReources[k] != true)
                                {
                                    break;
                                }
                                if (k == collectedReources.Length - 1 && collectedReources[k] == true)
                                {
                                    allowSubtract = true;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Bag.Count; i++)
            {
                for (int j = 0; j < obj.recpie.requiredResources.Length; j++)
                {
                    if (Bag[i].item.typeOfResource == obj.recpie.requiredResources[j].requiredResource.typeOfResource)
                    {
                        if (Bag[i].amount >= obj.recpie.requiredResources[j].requiredAmount)
                        {
                            for (int k = 0; k < collectedReources.Length; k++)
                            {
                                if (allowSubtract == true)
                                {
                                    Bag[i].amount -= obj.recpie.requiredResources[j].requiredAmount;
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
                    player.GetComponent<Melee>().weaponHolding = obj;
                }
            }
        }

        player.transform.GetComponent<PlayerStats>().InventoryStatus = false;
    }

    public void CraftWearable(Wearable obj)
    {

        bool[] collectedReources;
        bool allowSubtract = false;
        collectedReources = new bool[obj.recpie.requiredResources.Length];

        if (Bag.Count == 0)
        {
            Debug.Log("Your Inventory is empty");
        }

        else
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                for (int j = 0; j < obj.recpie.requiredResources.Length; j++)
                {
                    if (Bag[i].item.typeOfResource == obj.recpie.requiredResources[j].requiredResource.typeOfResource)
                    {
                        if (Bag[i].amount >= obj.recpie.requiredResources[j].requiredAmount)
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
                for (int j = 0; j < obj.recpie.requiredResources.Length; j++)
                {
                    if (Bag[i].item.typeOfResource == obj.recpie.requiredResources[j].requiredResource.typeOfResource)
                    {
                        if (Bag[i].amount >= obj.recpie.requiredResources[j].requiredAmount)
                        {
                            for (int k = 0; k < collectedReources.Length; k++)
                            {
                                if (collectedReources[k] != true)
                                {
                                    break;
                                }
                                if (k == collectedReources.Length - 1 && collectedReources[k] == true)
                                {
                                    allowSubtract = true;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Bag.Count; i++)
            {
                for (int j = 0; j < obj.recpie.requiredResources.Length; j++)
                {
                    if (Bag[i].item.typeOfResource == obj.recpie.requiredResources[j].requiredResource.typeOfResource)
                    {
                        if (Bag[i].amount >= obj.recpie.requiredResources[j].requiredAmount)
                        {
                            for (int k = 0; k < collectedReources.Length; k++)
                            {
                                if (allowSubtract == true)
                                {
                                    Bag[i].amount -= obj.recpie.requiredResources[j].requiredAmount;
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
                    player.GetComponent<Melee>().maskWearing = obj;
                }
            }
        }

        player.transform.GetComponent<PlayerStats>().InventoryStatus = false;
    }

    void positionCraftable()
    {
        if(craftedObj != null)
        {
            if (!placed)
            {
                RaycastHit hit;
                if(Physics.Raycast(player.transform.GetChild(1).position, player.transform.GetChild(1).forward,out hit, 5f, layers))
                {
                    if(hit.transform.gameObject.tag == "Floor")
                    {
                        //craftedObj.transform.position = player.transform.GetChild(0).position + player.transform.GetChild(0).forward * GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().SpwanDistance;
                        //craftedObj.transform.forward = player.transform.GetChild(0).transform.forward * craftDistance;
                        if (Vector3.Distance(hit.point, player.transform.GetChild(0).transform.position) > 2f)
                        {
                            craftedObj.transform.position = hit.point;
                            craftedObj.transform.up = hit.normal;
                            craftedObj.transform.GetChild(4).gameObject.SetActive(false);
                            craftedObj.transform.GetChild(3).gameObject.SetActive(true);
                            placeable = true;
                        }
                        else
                        {
                            placeable = false;
                            craftedObj.transform.position = player.transform.GetChild(0).position + player.transform.GetChild(0).forward * GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().SpwanDistance;
                            craftedObj.transform.forward = player.transform.GetChild(0).transform.forward * craftDistance;
                            craftedObj.transform.GetChild(4).gameObject.SetActive(true);
                            craftedObj.transform.GetChild(3).gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    placeable = false;
                    craftedObj.transform.position = player.transform.GetChild(0).position + player.transform.GetChild(0).forward * GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().SpwanDistance;
                    craftedObj.transform.forward = player.transform.GetChild(0).transform.forward * craftDistance;
                    craftedObj.transform.GetChild(4).gameObject.SetActive(true);
                    craftedObj.transform.GetChild(3).gameObject.SetActive(false);
                }


                //craftedObj.transform.position = player.transform.GetChild(0).position + player.transform.GetChild(0).forward * GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().SpwanDistance;
                //craftedObj.transform.forward = player.transform.GetChild(0).transform.forward * craftDistance;
            }
        }
    }

    void PlaceCraftable()
    {
        if (placeable == true)
        {
            if (Input.GetKeyDown(GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().PlaceObject))
            {
                craftedObj.transform.GetChild(4).gameObject.SetActive(false);
                craftedObj.transform.GetChild(3).gameObject.SetActive(false);
                craftedObj.transform.GetChild(0).gameObject.SetActive(true);
                craftedObj.GetComponent<BoxCollider>().enabled = true;
                placed = false;
                craftedObj.GetComponent<ScriptActiivation>().enabled = true;
                craftedObj = null;
            }
        }
    }
}
