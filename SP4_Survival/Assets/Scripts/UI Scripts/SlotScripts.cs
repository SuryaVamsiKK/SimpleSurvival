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
                GameObject g = Instantiate(resource.PickUp);
                g.transform.forward = this.transform.root.GetChild(1).forward;
                g.transform.position = this.transform.root.GetChild(1).position + this.transform.root.GetChild(1).forward * GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().SpwanDistance;
                g.transform.forward = this.transform.root.GetChild(1).forward;
                g.GetComponent<PickUp>().typeOf = resource.resource;
                g.GetComponent<PickUp>().amount = resource.amt;
                break;
            }
        }
        resource.amt = 0;
       
        resource.itself.SetActive(false);
    }
    

    public void WeponCraft(MeleeWeapons Wepon)
    {
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<CraftingV2>().CraftNonPlaceable(Wepon);
    }

    public void CraftCall(Recipies recpie)
    {
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<CraftingV2>().Craft(recpie);
    }

    public void WearableCraft(Wearable recpie)
    {
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<CraftingV2>().CraftWearable(recpie);
    }
}
