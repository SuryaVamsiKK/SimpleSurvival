using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resources")]
public class Resources : ScriptableObject
{
    public Resource typeOfResource;
    public float weight;
    public int perStack;

}

public enum Resource
{
    Wood, Stone
}
