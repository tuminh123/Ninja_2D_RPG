using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private readonly int moveX = Animator.StringToHash("movex");
    private readonly int moveY = Animator.StringToHash("movey");
    private readonly int moving = Animator.StringToHash("moving");
    private readonly int dead = Animator.StringToHash("dead");
    private readonly int revive = Animator.StringToHash("revive");
    private readonly int attack = Animator.StringToHash("attack");

    //Component
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public void ShowDeadAnimation()
    {
        ani.SetTrigger(dead);
    }
    public void SetMoveBoolTransition(bool value)
    {
        ani.SetBool(moving, value);
    }
    public void SetAttackAnimation(bool value)
    {
        ani.SetBool(attack, value);
    }
    public void SetMoveAnimation(Vector2 value)
    {
        ani.SetFloat(moveX, value.x);
        ani.SetFloat(moveY, value.y);
    }
    public void ResetAnimationPlayer()
    {
        SetMoveAnimation(Vector2.down);
        ani.SetTrigger(revive);
    }
}
