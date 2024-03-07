using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] GameObject grabText;

    [Header("Components")]
    public Health health;
    public Weapon weapon;
    public LayerMask weaponLayer;
    public Transform hand;

    [Header("SoundClips")]
    public AudioClip drop;
    public AudioClip equip;

    [Header("Events")]
    public UnityEvent onGrab;
    public UnityEvent onDrop;

    private void Start()
    {
        UpdateUI();
        health.onDamage.AddListener(UpdateUI);
        health.onDie.AddListener(Respawn);
    }

    private void Update()
    {
        var cam = Camera.main.transform;
        var collided = Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 2f, weaponLayer);
        grabText.SetActive(!weapon && collided);
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!weapon && collided) // !weapon || weapon == null
            {
                Grab(hit.collider.gameObject);
            }
            else if(weapon)
            {
                Drop();
            }
        }

        if (weapon == null) return;

        if (Input.GetKeyDown(KeyCode.Mouse1)) weapon.onRightClick.Invoke();

        //Manual Fire
        if (!weapon.isAutoFire && Input.GetKeyDown(KeyCode.Mouse0)) weapon.Shoot();

        //Auto Fire
        if (weapon.isAutoFire && Input.GetKey(KeyCode.Mouse0)) weapon.Shoot();

        if (Input.GetKeyDown(KeyCode.R) && weapon.ammo != weapon.maxAmmo) weapon.Reload();

        if (weapon.shootCooldown >= 0) weapon.shootCooldown -= Time.deltaTime;
    }

    public void Grab(GameObject gun)
    {
        if (weapon != null) return;

        AudioSystem.Play(equip);
        weapon = gun.GetComponent<Weapon>();
        weapon.GetComponent<Rigidbody>().isKinematic = true;
        weapon.transform.SetParent(hand);
        weapon.transform.position = hand.position;
        weapon.transform.rotation = hand.rotation;

        weapon.onShoot.AddListener(UpdateUI);
        weapon.onReload.AddListener(UpdateUI);

        onGrab.Invoke();
        UpdateUI();
    }

    public void Drop()
    {
        if (weapon == null) return;

        AudioSystem.Play(drop);
        weapon.onShoot.RemoveListener(UpdateUI);
        weapon.onReload.RemoveListener(UpdateUI);
        onDrop.Invoke();

        weapon.GetComponent<Rigidbody>().isKinematic = false;
        weapon.transform.SetParent(null);
        weapon = null;

        UpdateUI();
    }

    public void UpdateUI()
    {
        ammoText.gameObject.SetActive(weapon);
        if(weapon)ammoText.text = weapon.clipAmmo + "/" + weapon.ammo;
        healthText.text = "HP " + health.health.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.Damage(10);
        }
    }

    void Respawn()
    {
        health.health = health.maxHealth;
        transform.position = Vector3.zero;
        UpdateUI();
    }
}
