using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int inventorySpace;
    public List<inventoryItem> items = new List<inventoryItem> ();

    #region Singlton
    public static Inventory instance;

    void Awake ()
    {
        if (instance != null)
        {
            Debug.Log ("Mulit instances are there for theinventory");
        }
        instance = this;
    }
    #endregion

    // Start is called before the first frame update

    public bool Add (Item item) 
    {

        if (items.Count > inventorySpace) 
        {   
            Debug.Log ("You Ran out of Inventory");
            return false;
        }

        if (items.Count != 0) 
        {
            for (int i = 0; i < items.Count; i++) 
            {               
                if (items[i].item == item) 
                {
                    if (items[i].amount < items[i].item.stackableility) 
                    {
                        items[i].amount++;
                        break;
                    } 
                    if (items[i].amount >= items[i].item.stackableility && i == items.Count - 1 && i < inventorySpace) 
                    {
                        inventoryItem newItem = new inventoryItem ();
                        newItem.item = item;
                        newItem.amount = 1;
                        items.Add (newItem);
                        break;
                    }
                    if(i == inventorySpace)
                    {
                        Debug.Log("You Ran out of Inventory");
                        return false;
                    }
                }

                if(items[i].item != item && i == items.Count-1 && i < inventorySpace)
                {
                    inventoryItem newItem = new inventoryItem();
                    newItem.item = item;
                    newItem.amount = 1;
                    items.Add(newItem);
                    break;

                }

                if (i == inventorySpace)
                {
                    Debug.Log("You Ran out of Inventory");
                    return false;
                }
            }
        }

        else
        {
            inventoryItem newItem = new inventoryItem ();
            newItem.item = item;
            newItem.amount = 1;
            items.Add (newItem);
        }

        return true;
    }

    void Update ()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].amount <= 0)
            {
                items.RemoveAt(i);
            }
        }
    }

    [System.Serializable]
    public class inventoryItem
    {
        public Item item;
        public int amount;

        // public inventoryItem()
        // {
        //     item.typeOfItem = itemType.DEFAULT;
        //     item.stackableility = 0;
        //     item.name = "default";
        //     amount = 0;
        // }
    }
}