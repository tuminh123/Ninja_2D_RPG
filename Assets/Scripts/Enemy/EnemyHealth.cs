using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event System.Action OnEnemyDeadEvent;
    [Header("Config")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public float CurrentHealth => currentHealth;
    
    //Component
    private EnemyBrain enemyBrain;
    private EnemySeletor enemySelector;
    private EnemyLoot enemyLoot;
    private Animator animator;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySeletor>();
        enemyLoot = GetComponent<EnemyLoot>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            DisableEnemy();
            //gameObject.SetActive(false);
            QuestManager.Instance.AddProgress("Kill2Enemy", 1); // Update quest progress if applicable
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }

    private void DisableEnemy()
    {
        currentHealth = 0;
        animator.SetTrigger("dead");
        enemyBrain.enabled = false;
        enemySelector.NoSelectedCallBack();
        rb2D.bodyType = RigidbodyType2D.Static; // Disable physics interactions
        OnEnemyDeadEvent?.Invoke();

        GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);
    }
}
