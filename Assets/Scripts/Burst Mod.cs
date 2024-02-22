using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstMod : MonoBehaviour
{
    public Weapon weapon;
    public bool isburstFire;

    private void Start()
    {
        weapon.onRightClick.AddListener(BurstFire);
    }

    public void BurstFire()
    {
        isburstFire = !isburstFire;
        
        if(isburstFire)
        {
            weapon.bulletsPerShot = 7;
            weapon.isAutoFire = false;
        }
        else
        {
            weapon.isAutoFire = true;
            weapon.bulletsPerShot = 1;
        }
    }
}
