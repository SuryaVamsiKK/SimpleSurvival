using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ProducableRecpie", menuName = "Recpie/Producable")]
public class Produceable : ScriptableObject
{
    public Resource producable;
    public ProducableRecpie[] requiredResources;
    public GameObject output;
}

[System.Serializable]
public class ProducableRecpie
{
    public Resources requiredResource;
    public int requiredAmount;
}