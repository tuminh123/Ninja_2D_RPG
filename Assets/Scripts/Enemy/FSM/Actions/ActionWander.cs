using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWander : FSMAction
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private float wanderTime;
    [SerializeField] private Vector2 moveRange;

    private Vector3 movePosition;
    private float timer;
    private Animator ani;

    private void Awake() {
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        this.GetNewDestination();
    }

    public override void Action()
    {
        timer -= Time.deltaTime;
        Vector3 moveDir = (movePosition - transform.position).normalized;
        Vector3 movement = moveDir * (speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePosition) >= 0.5f)
        {
            transform.Translate(movement);
        }
        if (timer <= 0)
        {
            this.GetNewDestination();
            timer = wanderTime;
        }
        
        ani.SetFloat(moveX, moveDir.x);
        ani.SetFloat(moveY, moveDir.y);
    }

    private void GetNewDestination()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float RandomY = Random.Range(-moveRange.y, moveRange.y);
        movePosition = transform.position + new Vector3(randomX, RandomY);
    }

    void OnDrawGizmos()
    {
        if (moveRange != Vector2.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, moveRange * 2f);
            Gizmos.DrawLine(transform.position, movePosition);
        }
    }
}
