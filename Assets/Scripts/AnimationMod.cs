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
        weapon.onReloadStart.AddListener(ReloadAnim);

        animator.SetFloat("ReloadTime", 1 / weapon.reloadTime);
        animator.SetFloat("FireRate", 1 / weapon.fireInterval);
    }

    void RemoveWeapon()
    {
        weapon.onShoot.RemoveListener(RecoilAnim);
        weapon.onReloadStart.RemoveListener(ReloadAnim);
    }

    void RecoilAnim()
    { 
        if(!animator.GetNextAnimatorStateInfo(0).IsName("GunRecoilAnim"))
        {
            animator.Play("GunRecoilAnim");
        }
    }

    void ReloadAnim()
    {
        animator.Play("GunReloadAnim");
    }
}
