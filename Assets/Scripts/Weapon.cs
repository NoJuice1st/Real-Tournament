using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int multibulletAmount;
    public bool useMultibulletAmmo;

    public int storedAmmo;
    public int ammo;
    public int maxAmmo;

    public float bulletSpread;

    public bool isAutoFire;

    public float reloadTime = 2f;
    public float shootInterval;
    
    float shootCooldown;
    bool isReloading;
    
    private void Start()
    {
        if(storedAmmo <= 0) storedAmmo = maxAmmo;
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
        }

        if (shootCooldown > 0) return;

        var spread = transform.position + new Vector3( Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));

        if(multibulletAmount > 1)
        {
            if (!useMultibulletAmmo) ammo--;

            for (int i = 0; i < multibulletAmount; i++)
            {
                spread = transform.position + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
                Instantiate(bulletPrefab, spread, transform.rotation);

                if (useMultibulletAmmo)
                {
                    ammo--;

                    if (ammo <= 0)
                    {
                        print("No amo");
                        Reload();
                        return;
                    }
                }
            }
        }
        else
        {
            Instantiate(bulletPrefab, spread, transform.rotation);
            ammo--;
        }

        shootCooldown = shootInterval;
    }

    async public void Reload()
    {
        if (storedAmmo <= 0) return;
        if (isReloading) return;

        isReloading = true;

        await new WaitForSeconds(reloadTime);
        
        isReloading = false;

        for (int i = 0; i < maxAmmo; i++)
        {
            if (storedAmmo <= 0) return;
            ammo += 1;
            storedAmmo -= 1;
        }

    }
}
