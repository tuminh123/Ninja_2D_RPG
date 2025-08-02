
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : FSMAction
{

    [Header("Config")]
    [SerializeField] private float chaseSpeed;

    private EnemyBrain enemyBrain;

    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override void Action()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (enemyBrain.Player == null) return;
        Vector3 dirToPlayer = enemyBrain.Player.position - transform.position;
        if (dirToPlayer.magnitude >= 1.3f)
        {
            transform.Translate(dirToPlayer.normalized * chaseSpeed * Time.deltaTime);
        }
        ani.SetFloat(moveX, dirToPlayer.x);
        ani.SetFloat(moveY, dirToPlayer.y);
    }
}
