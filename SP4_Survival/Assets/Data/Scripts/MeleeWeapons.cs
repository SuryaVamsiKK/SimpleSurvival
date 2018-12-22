﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee")]
public class MeleeWeapons : ScriptableObject
{
    public float weight;
    public float damage;
    public float meleeRange;
    public GameObject Wepon;
}

public enum WeoponLevel
{
    Level1, Level2, Level3
}
