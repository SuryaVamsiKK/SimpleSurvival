using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resources")]
public class Resources : ScriptableObject
{
    public Resource typeOfResource;
    public float weight;
    public int perStack;
    public Sprite icon;

}

public enum Resource
{
    Wood, Stone, Charcole, Cotton, Plastic, MetalOre, Metal, Rubber
}
