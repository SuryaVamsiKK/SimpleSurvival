using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee")]
public class MeleeWeapons : ScriptableObject
{
    public int harvestMultiplier;
    public float damage;
    public float meleeRange;
    public GameObject Wepon;
    public LayerMask AccessLevel;
    public Sprite Icon;
    public Recipies recpie;
}

public enum WeoponLevel
{
    Level1, Level2, Level3
}
