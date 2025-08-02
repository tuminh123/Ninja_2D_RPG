using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionsAttackRange : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float range = 1.5f;
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return PlayerInAttackRange();
    }

    private bool PlayerInAttackRange()
    {
        if (enemyBrain.Player == null) return false;
        Collider2D playerCollider = Physics2D.OverlapCircle(enemyBrain.transform.position, range, playerMask);
        if (playerCollider != null)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
