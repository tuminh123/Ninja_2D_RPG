using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : FSMAction
{
    [Header("Config")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private float timeBtwAttacks = 1f;
    private float timer;
    private EnemyBrain enemyBrain;

    private void Awake()
    {

        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override void Action()
    {
        Debug.Log("Attack");
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        if (enemyBrain.Player == null) return;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            IDamageable damagePlayer = enemyBrain.Player.GetComponent<IDamageable>();
            if (damagePlayer == null) return;
            damagePlayer.TakeDamage(damage);
            timer = timeBtwAttacks;
        }
    }
}
