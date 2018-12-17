using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int inventorySpace;

    #region Singlton
    public static Inventory instance;

    void Awake () {
        if (instance != null) {
            Debug.Log ("Mulit instances are there for theinventory");
        }
        instance = this;
    }
    #endregion

    // Start is called before the first frame update

    public List<Item> items = new List<Item> ();

    public bool Add (Item item) 
    {
        if(items.Count > inventorySpace)
        {
            Debug.Log("You Ran out of Inventory");
            return false;
        }

        items.Add (item);

        return true;
    }

    public void Remove (Item item) {
        items.Remove (item);
    }
}