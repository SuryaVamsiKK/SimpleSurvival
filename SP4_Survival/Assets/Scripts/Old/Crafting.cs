using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{

    public GameObject wall;
    public Vector3 pos;
    GameObject crafted;
    public Inventory bag;


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
            if (bag.items.Count > 0)
            {
                for (int i = 0; i < bag.items.Count; i++)
                {
                    if (i == bag.items.Count - 1)
                    {
                        if (bag.items[i].amount < 3)
                        {
                            Debug.Log("Not Enough resources to craft");
                            break;
                        }
                        else
                        {
                            if (bag.items[i].item.typeOfItem == itemType.WOOD)
                            {
                                bag.items[i].amount -= 3;
                                GameObject g = Instantiate(wall);
                                crafted = g;
                                break;
                            }
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
}
