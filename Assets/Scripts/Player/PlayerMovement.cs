using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    private Vector2 moveDirection;
    public Vector2 MoveDirection => moveDirection;

    //Component
    private Player player;
    private PlayerAnimation playerAnimation;
    private Rigidbody2D rb2D;

    private PlayerActions actions;

    #region  Unity Function
    private void Awake()
    {
        player = GetComponent<Player>();
        actions = new PlayerActions();
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    private void Update() {
        this.ReadMovement();
    }
    private void FixedUpdate() {
        this.Moveing();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
    #endregion

    #region  Movement
    private void Moveing()
    {
        if (player.Stats.currentHealth <= 0) return;
        rb2D.MovePosition(rb2D.position + moveDirection*(speed*Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
        if (moveDirection == Vector2.zero)
        {
            playerAnimation.SetMoveBoolTransition(false);
            return;
        }

        playerAnimation.SetMoveBoolTransition(true);

        playerAnimation.SetMoveAnimation(moveDirection);
    }
    #endregion
}
