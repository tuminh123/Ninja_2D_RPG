using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public float currentMana { get; private set; }

    private void Start()
    {
        ResetMana();
    }

    public void UseMana(float amount)
    {
        //if (stats.currentHealth >= amount)
        stats.currentMana = Mathf.Max(stats.currentMana -= amount, 0f);
        currentMana = stats.currentMana;
    }

    //add mana amount
    public void RestoreMana(float amount)
    {
        stats.currentMana += amount;
        stats.currentMana = Mathf.Min(stats.currentMana, stats.maxMana);

    }
    public bool CanRestoreMana()
    {
        return stats.currentMana > 0 && stats.currentMana < stats.maxMana;
    }

    public void ResetMana()
    {
        currentMana = stats.maxMana;
    }
}
