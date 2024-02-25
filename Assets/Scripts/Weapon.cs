using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public int bulletsPerShot;

    public int ammo;
    public int maxAmmo;
    public int clipSize;
    public int clipAmmo;

    public float spreadAngle = 5f;

    public bool isAutoFire;

    public float reloadTime = 2f;
    public float shootInterval;
    
    float shootCooldown;
    bool isReloading;

    public UnityEvent onRightClick;
    public UnityEvent onShoot;
    public UnityEvent onReload;
    
    private void Start()
    {
        if(clipAmmo <= 0) clipAmmo = clipSize;
        if (ammo <= 0) ammo = maxAmmo;
    }

    void Update()
    {
        //Manual Fire
        if(!isAutoFire && Input.GetKeyDown(KeyCode.Mouse0)) Shoot();

        //Auto Fire
        if (isAutoFire && Input.GetKey(KeyCode.Mouse0)) Shoot();

        if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo) Reload();

        if (Input.GetKeyDown(KeyCode.Mouse1)) onRightClick.Invoke();


        if(shootCooldown >= 0) shootCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (isReloading) return;

        if (clipAmmo <= 0) { 
            Reload();
            return;
        }

        if (shootCooldown > 0) return;


        for (int i = 0; i < bulletsPerShot; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        
            var offsetX = Random.Range(-spreadAngle, spreadAngle);
            var offsetY = Random.Range(-spreadAngle, spreadAngle);

            bullet.transform.eulerAngles += new Vector3(offsetX, offsetY, 0);
        }

        clipAmmo--;

        shootCooldown = shootInterval;
        onShoot.Invoke();
    }

    async public void Reload()
    {
        if (isReloading) return;

        isReloading = true;

        onReload.Invoke();

        await new WaitForSeconds(reloadTime);
        
        
        var ammoToReload = Mathf.Min(ammo, clipSize);
        ammo -= ammoToReload;
        clipAmmo += ammoToReload;
        
        isReloading = false;
        
    }
}
