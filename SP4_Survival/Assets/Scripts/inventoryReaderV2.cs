using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryReaderV2 : MonoBehaviour
{
    public InventoryV2 inventoryObject;

    public List<inventoryItems> Bag = new List<inventoryItems>();
    public InventoryTypes[] BackPack;

    void Start()
    {
        
    }


    void Update()
    {
        Bag = inventoryObject.Bag;
        DisplayUI(Bag);
    }

    void DisplayUI(List<inventoryItems> obj)
    {
        BackPack = new InventoryTypes[obj.Count];
        for (int i = 0; i < obj.Count; i++)
        {
            BackPack[i] = new InventoryTypes();
            BackPack[i].name = obj[i].item.typeOfResource.ToString();
        }

        for (int i = 0; i < obj.Count; i++)
        {
            if (obj[i].amount <= obj[i].item.perStack)
            {
                BackPack[i].pack = new int[1];
                BackPack[i].pack[0] = obj[i].amount;
            }
            
            if (obj[i].amount > obj[i].item.perStack)
            {
                if (obj[i].amount % obj[i].item.perStack == 0)
                {
                    BackPack[i].pack = new int[(obj[i].amount / obj[i].item.perStack)];
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
}

[System.Serializable]
public class InventoryTypes
{
    public int[] pack;
    public string name;
}
