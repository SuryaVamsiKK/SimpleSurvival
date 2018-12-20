using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScripts : MonoBehaviour
{
    public void RemoveReource(ReourceHolder resource)
    {
        for (int i = 0; i < InventoryV2.instance.Bag.Count; i++)
        {
            if(InventoryV2.instance.Bag[i].item.typeOfResource == resource.typeOf)
            {
                InventoryV2.instance.Bag[i].amount -= resource.amt;
                break;
            }
        }
        resource.amt = 0;
       
        resource.itself.SetActive(false);
    }

    public void ShowRecpie(GameObject g)
    {
        g.SetActive(true);
    }

    public void CraftCall(Recipies recpie)
    {
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<CraftingV2>().Craft(recpie);
    }
}
