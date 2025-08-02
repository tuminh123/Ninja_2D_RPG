using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private Vector3 direction;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamageable>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
