using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public itemType typeOfItem;   
    public string itemName; 
    public int stackableility;
}

public enum itemType
{
    WOOD, STONE, DEFAULT
}
