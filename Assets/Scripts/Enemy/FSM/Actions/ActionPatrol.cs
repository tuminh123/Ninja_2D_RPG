using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : FSMAction
{
    [Header("Config")]
    [SerializeField] private float speedPatrol;

    private Waypoint waypoint;
    private int pointIndex;
    private Vector3 nextPosition;

    private Animator ani;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        ani = GetComponent<Animator>();
    }

    public override void Action()
    {
        this.FollowPath();
    }

    //Set path waypoint
    private void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetCurrentPosition(), speedPatrol * Time.deltaTime);
        if (Vector3.Distance(transform.position, GetCurrentPosition()) <= 0.1f)
        {
            UpdateNextPosition();
        }
        Vector3 moveDir = GetCurrentPosition() - transform.position;
        ani.SetFloat(moveX, moveDir.x);
        ani.SetFloat(moveY, moveDir.y);
    }
    private void UpdateNextPosition()
    {
        pointIndex++;
        if (pointIndex > waypoint.Points.Length - 1)
            pointIndex = 0;
    }
    private Vector3 GetCurrentPosition()
    {
        return waypoint.GetPosition(pointIndex);
    }

}
