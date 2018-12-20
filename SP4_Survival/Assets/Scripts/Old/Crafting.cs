using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{

    public GameObject wall;
    public Vector3 pos;
    GameObject crafted;
    public InventoryV2 bag;


    bool place = false;
    Vector3 choosenLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (place == false && crafted != null)
        {
            pos = this.transform.GetChild(0).position + this.transform.GetChild(0).forward * 2;
            crafted.transform.forward = this.transform.GetChild(0).forward;
            crafted.transform.position = pos;
        }

        PlaceObject(pos);

    }

    void PlaceObject(Vector3 pos)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bag.Bag.Count > 0)
            {
                for (int i = 0; i < bag.Bag.Count; i++)
                {
                    if (i == bag.Bag.Count - 1)
                    {
                        if (bag.Bag[i].amount < 3)
                        {
                            Debug.Log("Not Enough resources to craft");
                            break;
                        }
                    }

                    if (bag.Bag[i].item.typeOfResource == Resource.Wood)
                    {
                        if (bag.Bag[i].amount > 3)
                        {
                            bag.Bag[i].amount -= 3;
                            GameObject g = Instantiate(wall);
                            crafted = g;
                            break;
                        }
                        else
                        {

                        }
                    }

                }
            }
            else
            {
                Debug.Log("YOur inventory is empty");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            crafted = null;
        }
    }

    void Craft()
    {
        int requiredResources = 3;
        
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            //for (int i = 0; i < woods.Count; i++)
            //{
            //    if (woods[i] > requiredResources)
            //    {

            //    }
            //    if (woods[i] < 0 && i != woods.Count - 1)
            //    {

            //    }
            //}+
        }

    }
}
