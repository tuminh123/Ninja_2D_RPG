using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/ItemWeapon")]
public class ItemWeapon : InventoryItem
{
    [Header("Weapon")]
    public Weapon weapon;

    public override void EquipItem()
    {
        WeaponManager.Instance.EquipWeapon(weapon);
    }
}
