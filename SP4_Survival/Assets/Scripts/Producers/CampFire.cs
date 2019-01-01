using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class CampFire : MonoBehaviour
{
    public Produceable Resource;
    public List<inventoryItems> Bag = new List<inventoryItems>();

    public float productionTime;
    public GameObject Produced;
    public GameObject Campfire;

    public int que = 0;

    public bool started = false;
    void Start()
    {
        Produced = null;
    }
    
    void Update()
    {
        Bag = InventoryV2.instance.Bag;
        
        if(GetComponent<ScriptActiivation>().interacted)
        {
            Craft(Resource);
            GetComponent<ScriptActiivation>().interacted = false;
        }
    }

    IEnumerator Spwan(float craftTime)
    {
        yield return new WaitForSeconds(craftTime);
        que--;
        if(que <= 0)
        {
            Campfire.SetActive(false);
        }
        if (Produced == null)
        {
            Produced = Instantiate(Resource.output);
            Produced.GetComponent<PickUp>().amount = 1;
            Produced.transform.position = this.transform.GetChild(1).transform.position;
            yield break;
        }

        if (Produced != null)
        {
            Produced.GetComponent<PickUp>().amount++;
            yield break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(this.transform.GetChild(1).position, 0.25f);
    }


    public void Craft(Produceable obj)
    {
        bool[] collectedReources;
        collectedReources = new bool[obj.requiredResources.Length];

        for (int i = 0; i < collectedReources.Length; i++)
        {
            collectedReources[i] = false;
        }

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
                                if(collectedReources[k] == false)
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
                    Campfire.SetActive(true);
                    que++;
                    StartCoroutine(Spwan(productionTime * que));
                }
            }         
        }
    }    
}
