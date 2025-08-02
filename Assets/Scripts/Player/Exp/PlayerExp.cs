using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddExp(100);            
        }
    }

    public void AddExp(float amount)
    {
        stats.totalExp += amount;
        stats.currentExp += amount;
        while (stats.currentExp >= stats.nextLevelExp)
        {
            stats.currentExp -= stats.nextLevelExp;
            UpdateExpToNextLevel();
        }
    }

    private void UpdateExpToNextLevel()
    {
        stats.level++;
        stats.attributePoint++;
        float currentExpRequired = stats.nextLevelExp;//100-vd
        float newNextLevelExp = Mathf.Round(currentExpRequired + stats.nextLevelExp * (stats.expMultiplier / 100f));//100+100*50/100 = 150
        stats.nextLevelExp = newNextLevelExp;
    }
}
