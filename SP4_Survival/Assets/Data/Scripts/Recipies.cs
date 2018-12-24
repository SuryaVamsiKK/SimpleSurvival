using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Recpie", menuName = "Recpie")]
public class Recipies : ScriptableObject
{
    public GameObject craftable;
    public sRrecpie[] requiredResources;
}

[System.Serializable]
public class sRrecpie
{
    public Resources requiredResource;
    public int requiredAmount;
}
