using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text ammoText;

    [Header("Components")]
    public Health health;
    public Weapon weapon;

    private void Start()
    {
        UpdateUI();
        weapon.onShoot.AddListener(UpdateUI);
        weapon.onReload.AddListener(UpdateUI);
        health.onDamage.AddListener(UpdateUI);
        health.onDie.AddListener(Respawn);
    }

    void UpdateUI()
    {
        ammoText.text = weapon.clipAmmo + "/" + weapon.ammo;
        healthText.text = "HP " + health.health.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.Damage(10);
        }
    }

    void Respawn()
    {
        health.health = health.maxHealth;
        transform.position = Vector3.zero;
        UpdateUI();
    }
}
