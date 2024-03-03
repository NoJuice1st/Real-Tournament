using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMod : MonoBehaviour
{
    private Animator animator;
    private Weapon weapon;
    public Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player.onGrab.AddListener(AddWeapon);
        player.onDrop.AddListener(RemoveWeapon);
    }
    
    void AddWeapon()
    {
        weapon = player.weapon;

        weapon.onShoot.AddListener(RecoilAnim);

        animator.SetFloat("ReloadTime", 1 / weapon.reloadTime);
        animator.SetFloat("FireRate", 1 / weapon.fireInterval);
    }

    void RemoveWeapon()
    {
        weapon.onShoot.RemoveListener(RecoilAnim);
    }

    void RecoilAnim()
    { 
        animator.Play("GunRecoilAnim");
    }

    void ReloadAnim()
    {
        animator.Play("GunReloadAnim");
    }
}
