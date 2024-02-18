using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int ammo;
    public int maxAmmo;

    public bool isAutoFire;

    public float reloadTime = 2f;
    public float shootInterval;
    
    float shootCooldown;
    bool isReloading;
    
    private void Start()
    {
        if (ammo <= 0) ammo = maxAmmo;
    }

    void Update()
    {
        //Manual Fire
        if(!isAutoFire && Input.GetKeyDown(KeyCode.Mouse0)) Shoot();

        //Auto Fire
        if (isAutoFire && Input.GetKey(KeyCode.Mouse0)) Shoot();

        if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo) Reload();

        if(shootCooldown >= 0) shootCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (isReloading) return;

        if (ammo <= 0) { 
            Reload();
            return;
        };

        if (shootCooldown > 0) return;

        Instantiate(bulletPrefab, transform.position, transform.rotation);
        ammo--;

        shootCooldown = shootInterval;
    }

    async public void Reload()
    {
        if (isReloading) return;

        isReloading = true;

        await new WaitForSeconds(reloadTime);

        isReloading = false;
        ammo = maxAmmo;
    }
}
