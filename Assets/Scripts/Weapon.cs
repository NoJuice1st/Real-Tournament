using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int ammo;
    public int maxAmmo;

    bool isReloading;
    
    private void Start()
    {
        if (ammo <= 0) ammo = maxAmmo;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) Shoot();

        if(Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo) Reload();
    }

    public void Shoot()
    {
        if (isReloading) return;

        if (ammo <= 0) { 
            Reload();
            return;
        };

        Instantiate(bulletPrefab, transform.position, transform.rotation);
        ammo--;
    }

    async public void Reload()
    {
        if (isReloading) return;

        isReloading = true;

        await new WaitForSeconds(2f);

        isReloading = false;
        ammo = maxAmmo;
    }
}
