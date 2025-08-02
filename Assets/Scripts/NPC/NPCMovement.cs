using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 3f;

    private readonly int moveX = Animator.StringToHash("movex");
    private readonly int moveY = Animator.StringToHash("movey");

    private Waypoint waypoint;
    private Animator animator;
    private Vector3 previousPos;
    private int currentPointIndex;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 nextPos = waypoint.GetPosition(currentPointIndex);
        
        UpdateMoveValue(nextPos);

        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPos) <= 0.2f)
        {
            previousPos = nextPos;
            currentPointIndex = (currentPointIndex + 1) % waypoint.Points.Length;
        }
    }

    private void UpdateMoveValue(Vector3 nextPos)
    {
        Vector2 dir = Vector2.zero;
        if(previousPos.x<nextPos.x)
        {
            dir = Vector2.right;
        }
        else if (previousPos.x > nextPos.x)
        {
            dir = Vector2.left;
        }
        else if (previousPos.y < nextPos.y)
        {
            dir = Vector2.up;
        }
        else if (previousPos.y > nextPos.y)
        {
            dir = Vector2.down;
        }
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }

}
