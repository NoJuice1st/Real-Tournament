using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public AudioClip clip;
    public float respawnTime = 0f;
    public List<GameObject> parts;
    private bool debounce = false;

    private void OnTriggerEnter(Collider other) {
        if(!debounce)
        {
            if(other.transform.CompareTag("Player") && other.gameObject.name == "Player" && other.gameObject.TryGetComponent<Player>(out Player plr))
            {
                if(plr.weapon)
                {
                    debounce = true;
                    AudioSystem.Play(clip);
                    if(plr.weapon.maxAmmo > plr.weapon.ammo)plr.weapon.ammo += plr.weapon.clipSize;
                    plr.UpdateUI();
                    if(respawnTime == 0f)Destroy(gameObject);
                    else {
                        foreach (var part in parts)
                        {
                            part.SetActive(false);
                        }
                        Invoke("ResetDebounce", 2f);
                    }
                }
            }
        }
    }

    void ResetDebounce()
    {
        debounce = false;
        foreach (var part in parts)
        {
            part.SetActive(true);
        }
    }


}
