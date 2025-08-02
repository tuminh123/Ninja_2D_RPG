using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    magic,
    melee
}

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Config")]
    public Sprite icon;
    public WeaponType weaponType;
    public float damage;

    [Header("Projectile")]
    public Projectile projectile;
    public float requiredMana;
}
