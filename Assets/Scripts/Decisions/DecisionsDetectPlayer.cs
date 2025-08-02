using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionsDetectPlayer : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float range = 4;
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return DetectPlayer();
    }
    private bool DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(enemyBrain.transform.position, range, playerMask);
        if (playerCollider != null)
        {
            enemyBrain.SetPlayer(playerCollider.transform);
            return true;
        }

        enemyBrain.SetPlayer(null);
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
