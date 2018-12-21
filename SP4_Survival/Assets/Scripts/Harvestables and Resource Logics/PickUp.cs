using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Resources typeOf;
    public int amount;

    void Update()
    {
        if(this.GetComponent<ScriptActiivation>().interacted)
        {
            InventoryV2.instance.Add(typeOf, amount);
            Destroy(this.gameObject);
            GetComponent<ScriptActiivation>().interacted = false;
        }
    }
}
