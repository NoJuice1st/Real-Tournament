using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int health = 100;

    public int maxHealth = 100;

    private void Start()
    {
        if(health == 0) health = maxHealth;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0) Die();

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
