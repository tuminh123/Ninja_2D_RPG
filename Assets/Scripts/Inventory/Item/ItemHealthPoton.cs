using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/ItemHealthPoton")]
public class ItemHealthPoton : InventoryItem
{
    [Header("Config")]
    public float healthValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerHealth.CanRestoreHealth())
        {
            GameManager.Instance.Player.PlayerHealth.RestoreHealth(healthValue);
            return true;
        }
        return false;
    }
}
