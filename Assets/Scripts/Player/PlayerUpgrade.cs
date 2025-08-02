using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgradeEvent;

    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    [Header("Setting")]
    [SerializeField] private UpgradeSettings[] settings;

    private void OnEnable() {
        AttributeButton.OnAttributeSelectedEvent += AttributeCallBack;
    }
    private void OnDisable() {
        AttributeButton.OnAttributeSelectedEvent -= AttributeCallBack;
    }

    private void AttributeCallBack(AttributeType attributeType)
    {
        if (stats.attributePoint == 0 || stats == null) return;
        switch (attributeType)
        {
            case AttributeType.strength:
                UpgradePlayer(0);
                stats.strength++;
                break;
            case AttributeType.dexterity:
                UpgradePlayer(1);
                stats.dexterity++;
                break;
            case AttributeType.intelligence:
                UpgradePlayer(2);
                stats.intelligence++;
                break;
        }
        stats.attributePoint--;
        OnPlayerUpgradeEvent?.Invoke();
    }

    private void UpgradePlayer(int upgradeIndex)
    {
        stats.baseDamage += settings[upgradeIndex].damageUpgrade;
        stats.totalDamage += settings[upgradeIndex].damageUpgrade;
        stats.maxHealth += settings[upgradeIndex].healthUpgrade;
        stats.currentHealth = stats.maxHealth;
        stats.maxMana += settings[upgradeIndex].manaUpgrade;
        stats.currentMana = stats.maxMana;
        stats.criticalChance += settings[upgradeIndex].cChanceUpgrade;
        stats.criticalDamage += settings[upgradeIndex].cDamageUpgrade;
    }
}

[System.Serializable]
public class UpgradeSettings
{
    public string name;

    [Header("Values")]
    public float damageUpgrade;
    public float healthUpgrade;
    public float manaUpgrade;
    public float cChanceUpgrade;
    public float cDamageUpgrade;
}
