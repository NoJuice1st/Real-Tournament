using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMod : MonoBehaviour
{
    public Animator animator;
    public Weapon weapon;

    private void Start()
    {
        weapon = GetComponent<Weapon>();

        weapon.onShoot.AddListener(RecoilAnim);
        weapon.onReload.AddListener(ReloadAnim);

        animator.SetFloat("ReloadTime", 1 / weapon.reloadTime);
        animator.SetFloat("FireRate", 1 / weapon.shootInterval);
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
