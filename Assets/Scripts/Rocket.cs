using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 100f;
    public int damage;
    public int bounceCount = 0;

    private Rigidbody rb;
    public GameObject explosionPrefab;
    public GameObject hitPrefab;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
        rb.AddForce(transform.forward * speed);
    }

    private void OnCollisionEnter(Collision other) {

        //Destroy(gameObject);

        var health = other.gameObject.GetComponent<Health>();

        if(health != null) health.Damage(damage);

        //wall hit effect
        var obj = Instantiate(hitPrefab, transform.position, Quaternion.identity);
        obj.transform.forward = other.GetContact(0).normal;
        obj.transform.SetParent(other.transform);


        if (bounceCount > 0)
        {
            bounceCount--;
            transform.forward = other.GetContact(0).normal;
            rb.AddForce(transform.forward * speed / 2);
        }
        else
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
