using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 100f;
    public int damage;

    private Rigidbody rb;
    public ParticleSystem explosionParticles;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
        rb.AddForce(transform.forward * speed);
    }

    private void OnCollisionEnter(Collision other) {
        Instantiate(explosionParticles, transform.position, transform.rotation);

        Destroy(gameObject);

        var health = other.gameObject.GetComponent<Health>();

        if(health != null) health.Damage(damage);

    }
}
