using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/ItemManaPoton")]
public class ItemManaPotion : InventoryItem
{
    [Header("Config")]
    public float manaValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerMana.CanRestoreMana())
        {
            GameManager.Instance.Player.PlayerMana.RestoreMana(manaValue);
            return true;
        }
        return false;
    }
}
