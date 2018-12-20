using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryReaderV2 : MonoBehaviour
{
    public float inventorySpace = 16;
    public float maxWeight = 100;
    public float currentWeight = 0;

    InventoryV2 inventoryObject;
    public int inventorycount;
    public bool inventoryOpen = true;

    List<inventoryItems> Bag = new List<inventoryItems>();
    public InventoryTypes[] BackPack;

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryV2>();
        //inventoryObject.OnItemChangeCallback += DisplayUI;
    }


    void Update()
    {
        //DisplayUI();
        SlotCheck();
    }

    public void DisplayUI()
    {
        Bag = inventoryObject.Bag;
        List<inventoryItems> obj = Bag;

        BackPack = new InventoryTypes[obj.Count];
        for (int i = 0; i < obj.Count; i++)
        {
            BackPack[i] = new InventoryTypes();
            BackPack[i].name = obj[i].item.typeOfResource;
            BackPack[i].icon = obj[i].item.icon;
        }

        for (int i = 0; i < obj.Count; i++)
        {
            if (obj[i].amount <= obj[i].item.perStack)
            {
                BackPack[i].pack = new int[1];
                BackPack[i].UI = new Button[1];
                BackPack[i].pack[0] = obj[i].amount;
            }
            
            if (obj[i].amount > obj[i].item.perStack)
            {
                if (obj[i].amount % obj[i].item.perStack == 0)
                {
                    BackPack[i].pack = new int[(obj[i].amount / obj[i].item.perStack)];
                    BackPack[i].UI = new Button[(obj[i].amount / obj[i].item.perStack)];
                    for (int j = 0; j < BackPack[i].pack.Length; j++)
                    {
                        BackPack[i].pack[j] = obj[i].item.perStack;
                    }
                }

                if (obj[i].amount % obj[i].item.perStack != 0)
                {
                    BackPack[i].pack = new int[(obj[i].amount / obj[i].item.perStack) + 1];
                    for (int j = 0; j < BackPack[i].pack.Length; j++)
                    {
                        if (j < BackPack[i].pack.Length - 1)
                        {
                            BackPack[i].pack[j] = obj[i].item.perStack;
                        }
                        if (j == BackPack[i].pack.Length - 1)
                        {
                            BackPack[i].pack[j] = obj[i].amount - closestNumber(obj[i].amount, obj[i].item.perStack);
                        }
                    }
                }
            }
        }
    }

    static int closestNumber(int n, int m)
    {
        int q = n / m;        
        int n1 = m * q;
        return n1;
    }

    void SlotCheck()
    {
        inventorycount = 0;
        for (int i = 0; i < BackPack.Length; i++)
        {
            for (int j = 0; j < BackPack[i].pack.Length; j++)
            {
                inventorycount++;
            }
        }

        if(currentWeight >= maxWeight)
        {
            inventoryOpen = false;
        }

        if (BackPack.Length > 0)
        {
            if(BackPack[BackPack.Length - 1].pack.Length > 1)
            {
                if (inventorycount == inventorySpace)
                {
                    if (BackPack[BackPack.Length - 1].pack[BackPack[BackPack.Length - 1].pack.Length - 1] == BackPack[BackPack.Length - 1].pack[BackPack[BackPack.Length - 1].pack.Length - 2])
                    {
                        inventoryOpen = false;
                    }
                }
            }
        }       
    }
}

[System.Serializable]
public class InventoryTypes
{
    public int[] pack;
    public Resource name;
    public Sprite icon;
    public Button[] UI;
}
