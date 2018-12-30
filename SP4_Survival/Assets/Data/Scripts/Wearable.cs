using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mask Weapon", menuName = "Equipable/Mask")]
public class Wearable : ScriptableObject
{
    public GameObject Msk;
    public Sprite Icon;
    public Recipies recpie;
    public MaskLevel AccessLevel;
}

public enum MaskLevel
{
    Level0, Level1, Level2
}
