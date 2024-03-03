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
    bool isReloading;

    public float reloadTime = 2f; // not in actual thing
    public float fireInterval;
    
    public float shootCooldown;

    public AudioClip shot;
    public AudioClip reload;

    public UnityEvent onRightClick;
    public UnityEvent onShoot;
    public UnityEvent onReloadStart;
    public UnityEvent onReload;
    
    private void Start()
    {
        if(clipAmmo <= 0) clipAmmo = clipSize;
        if (ammo <= 0) ammo = maxAmmo;
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
        AudioSystem.Play(shot);
        onShoot.Invoke();
        shootCooldown = fireInterval;
    }

    async public void Reload()
    {
        if (isReloading) return;

        isReloading = true;
        onReloadStart.Invoke();
        AudioSystem.Play(reload);
        await new WaitForSeconds(reloadTime);
        
        var ammoToReload = Mathf.Min(ammo, clipSize - clipAmmo);
        //print(ammo +" "+ clipSize);
        ammo -= ammoToReload;
        clipAmmo += ammoToReload;
        
        isReloading = false;
        onReload.Invoke();

    }
}
