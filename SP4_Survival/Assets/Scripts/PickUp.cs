using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Resources typeOf;
    public int amount;

    void Awake()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(other.transform.parent.GetComponent<Controls>().PickUP))
            {
                InventoryV2.instance.Add(typeOf, amount);
            }
        }
    }
}
