using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    //deduct health amount
    public void TakeDamage(float amount)
    {
        stats.currentHealth -= amount;
        DamageManager.Instance.ShowDamageText(amount, transform);
        if (stats.currentHealth <= 0f)
        {
            stats.currentHealth = 0;
            PlayerDead();
        }
    }
    private void PlayerDead()
    {
        playerAnimation.ShowDeadAnimation();
    }

    //add health amount
    public void RestoreHealth(float amount)
    {
        stats.currentHealth += amount;
        if (stats.currentHealth > stats.maxHealth)
        {
            stats.currentHealth = stats.maxHealth;
        }

    }
    public bool CanRestoreHealth()
    {
        return stats.currentHealth > 0 && stats.currentHealth < stats.maxHealth;
    }
}
