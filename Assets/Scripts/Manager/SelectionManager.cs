using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static event Action<EnemyBrain> OnEnemySelectEvent;
    public static event Action OnSelectEvent;

    [Header("Config")]
    [SerializeField] private LayerMask enemyMask;
    
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    private void Update() {
        SelectEnemy();
    }

    private void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, enemyMask);

            if (hit.collider != null)
            {
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();
                if (enemy == null) return;

                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

                if (enemyHealth.CurrentHealth <= 0)
                {
                    EnemyLoot enemyLoot = enemy.GetComponent<EnemyLoot>();
                    LootManager.Instance.ShowLoot(enemyLoot);
                }
                else
                {
                    OnEnemySelectEvent?.Invoke(enemy);
                }
            }
            else
            {
                OnSelectEvent?.Invoke();
            }
        }
    }
}
