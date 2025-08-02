using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Weapon initWeapon;
    [SerializeField] private Transform[] attackPosition;
    [SerializeField] private PlayerStats stats;

    [Header("Mele Config")]
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleAttack = 2f;
    private Weapon currentWeapon;
    public Weapon CurrentWeapon => currentWeapon;

    //Component

    private PlayerActions actions;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private EnemyBrain enemyTarget;
    private Coroutine attackCoroutine;
    private PlayerMana playerMana;

    private Transform currentAttackPosition;
    private float currentAttackRotation;


    private void Awake()
    {
        actions = new PlayerActions();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMana = GetComponent<PlayerMana>();
    }

    private void Start()
    {
        WeaponManager.Instance.EquipWeapon(initWeapon);
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetFirePosition();
    }

    //Set Up Emey selected
    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectEvent += EnemySelectedCallBack;
        SelectionManager.OnSelectEvent += NoSelectedCallBack;
        EnemyHealth.OnEnemyDeadEvent += NoEnemySelectedCallBack;
    }
    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectEvent -= EnemySelectedCallBack;
        SelectionManager.OnSelectEvent -= NoSelectedCallBack;
        EnemyHealth.OnEnemyDeadEvent -= NoEnemySelectedCallBack;
    }

    private void EnemySelectedCallBack(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }

    private void NoSelectedCallBack()
    {
        enemyTarget = null;
    }

    private void NoEnemySelectedCallBack()
    {
        enemyTarget = null;
    }
    //Damage setup
    private float GetAttackDamage()
    {
        float damage = stats.baseDamage;
        damage += currentWeapon.damage;
        float randomPerc = Random.Range(0f, 100f);
        if (randomPerc <= stats.criticalChance)
        {
            damage += damage * (stats.criticalDamage / 100f);
        }
        return damage;
    }

    //attack action setup
    private void Attack()
    {
        if (enemyTarget == null) return;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        attackCoroutine = StartCoroutine(IEAttack());
    }
    private IEnumerator IEAttack()
    {
        if (currentAttackPosition == null) yield break;
        if (currentWeapon.weaponType == WeaponType.magic)
        {
            MagicAttack();
        }
        else
        {
            MeleAttack();
        }

        playerAnimation.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimation.SetAttackAnimation(false);
    }

    //magic attack
    private void MagicAttack()
    {
        if (playerMana.currentMana < currentWeapon.requiredMana) return;
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectile projectile = Instantiate(currentWeapon.projectile, currentAttackPosition.position, rotation);
        projectile.SetDirection(Vector3.up);
        projectile.SetDamage(GetAttackDamage());
        playerMana.UseMana(currentWeapon.requiredMana);
    }

    //mele attack
    private void MeleAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();
        float currentDistanceToEnemy = Vector3.Distance(enemyTarget.transform.position, transform.position);
        if (currentDistanceToEnemy < minDistanceMeleAttack)
        {
            enemyTarget.GetComponent<IDamageable>().TakeDamage(GetAttackDamage());
        }
    }

    //fire projectile setup - magic attack
    private void GetFirePosition()
    {
        Vector2 moveDir = playerMovement.MoveDirection;
        switch (moveDir.x)
        {
            case > 0f:
                currentAttackPosition = attackPosition[1];
                currentAttackRotation = -90f;
                break;
            case < 0f:
                currentAttackPosition = attackPosition[3];
                currentAttackRotation = -270f;
                break;

        }
        switch (moveDir.y)
        {
            case > 0f:
                currentAttackPosition = attackPosition[0];
                currentAttackRotation = 0f;
                break;
            case < 0f:
                currentAttackPosition = attackPosition[2];
                currentAttackRotation = -180f;
                break;

        }
    }

    //Weapon Setup
    public void EquipWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        stats.totalDamage = stats.baseDamage + currentWeapon.damage;
    }
}
