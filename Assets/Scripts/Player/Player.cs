using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    public PlayerStats Stats => stats;

    private PlayerMana playerMana;
    public PlayerMana PlayerMana => playerMana;

    private PlayerHealth playerHealth;
    public PlayerHealth PlayerHealth => playerHealth;

    private PlayerAttack playerAttack;
    public PlayerAttack PlayerAttack => playerAttack;

    private PlayerAnimation playerAnimation;

    [Header("Test")]
    public ItemHealthPoton itemHealthPoton;
    public ItemManaPotion itemManaPoton;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMana = GetComponent<PlayerMana>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        stats.ResetStatsPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (itemHealthPoton.UseItem())
            {
                Debug.Log("Use Health Potion");
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (itemManaPoton.UseItem())
            {
                Debug.Log("Use Health Potion");
            }
        }
    }

    public void ResetPlayer()
    {
        stats.ResetStatsPlayer();
        playerAnimation.ResetAnimationPlayer();
        playerMana.ResetMana();
    }
}
