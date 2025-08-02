using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeType
{
    strength,
    dexterity,
    intelligence
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int level = 1;

    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 10;

    [Header("Mana")]
    public float currentMana;
    public float maxMana = 20;

    [Header("Exp")]
    public float currentExp;
    public float nextLevelExp;
    public float initNextLevelExp;
    [Range(1f, 100f)]
    public float expMultiplier;

    [Header("Attack")]
    public float baseDamage;
    public float criticalChance;
    public float criticalDamage;

    [HideInInspector] public float totalDamage;
    [HideInInspector] public float totalExp;

    [Header("Attributes")]
    public int strength;
    public int dexterity;
    public int intelligence;
    public int attributePoint;

    public void ResetStatsPlayer()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        level = 1;
        currentExp = 0f;
        nextLevelExp = initNextLevelExp;
        totalExp = 0f;
        baseDamage = 2;
        criticalChance = 10;
        criticalDamage = 50;
        strength = 0;
        dexterity = 0;
        intelligence = 0;
        attributePoint = 0;
    }
}

