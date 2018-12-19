using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV2 : MonoBehaviour
{

    public float maxWeight = 100;
    public float currentWeight = 0;

    public List<inventoryItems> Bag = new List<inventoryItems>();

    #region Singlton
    public static InventoryV2 instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Mulit instances are there for theinventory");
        }
        instance = this;
    }
    #endregion

    public bool Add(Resources item)
    {
        inventoryItems resurce = new inventoryItems();
        resurce.item = item;
        resurce.amount++;

        if (Bag.Count <= 0)
        {
            Bag.Add(resurce);
            currentWeight += resurce.item.weight;
            return true;
        }

        if (Bag.Count > 0)
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].item.typeOfResource == resurce.item.typeOfResource)
                {
                    Bag[i].amount++;
                    currentWeight += resurce.item.weight;
                    return true;
                }
                if (i == Bag.Count - 1 && Bag[i].item.typeOfResource != resurce.item.typeOfResource)
                {
                    Bag.Add(resurce);
                    currentWeight += resurce.item.weight;
                    return true;
                }
            }
        }

        return false;
    }
}

[System.Serializable]
public class inventoryItems
{
    public Resources item = new Resources();
    public int amount;
}
