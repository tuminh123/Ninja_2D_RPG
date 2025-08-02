using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeletor : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject selectorSprite;
    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        selectorSprite.SetActive(false);
    }
    private void OnEnable()
    {
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallBack;
        SelectionManager.OnSelectEvent += NoSelectedCallBack;
    }
    private void OnDisable()
    {
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallBack;
        SelectionManager.OnSelectEvent -= NoSelectedCallBack;
    }

    private void EnemySelectedCallBack(EnemyBrain enemySelected)
    {
        if (enemySelected == enemyBrain)
        {
            selectorSprite.SetActive(true);
        }
        else
        {
            selectorSprite.SetActive(false);
        }
    }

    public void NoSelectedCallBack()
    {
        selectorSprite.SetActive(false);
    }
}
