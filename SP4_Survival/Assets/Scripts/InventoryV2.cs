using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryV2 : MonoBehaviour
{
    inventoryReaderV2 reader;
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

    //public delegate void OnItemChange();
    //public OnItemChange OnItemChangeCallback;

    private void Update()
    {
        Remove();
    }

    public bool Add(Resources item, int num)
    {
        reader = GetComponent<inventoryReaderV2>();

        inventoryItems resurce = new inventoryItems();
        resurce.item = item;
        resurce.amount += num;

        if (Bag.Count <= 0)
        {
            Bag.Add(resurce);
            reader.currentWeight += resurce.item.weight;
            return true;
        }

        if (Bag.Count > 0)
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].item.typeOfResource == resurce.item.typeOfResource)
                {
                    Bag[i].amount += num;
                    reader.currentWeight += resurce.item.weight;
                    return true;
                }
                if (i == Bag.Count - 1 && Bag[i].item.typeOfResource != resurce.item.typeOfResource)
                {
                    Bag.Add(resurce);
                    reader.currentWeight += resurce.item.weight;
                    return true;
                }
            }
        }
        return false;
    }

    public void Remove()
    {
        for (int i = 0; i < Bag.Count; i++)
        {
            if (Bag[i].amount <= 0)
            {
                Bag.RemoveAt(i);
            }
        }
    }
}

[System.Serializable]
public class inventoryItems
{
    public Resources item = new Resources();
    public int amount;
}
