using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenOnDamageMod : MonoBehaviour
{
    private Health health;
    public float darkenAmount = 0.1f;

    private void Start()
    {
        if (gameObject.TryGetComponent<Health>(out Health hp))
        {
            health = hp;
            health.onDamage.AddListener(Darken);
        }
    }

    public void Darken()
    {
        if (gameObject.TryGetComponent<Renderer>(out Renderer rend))
        {
            var oldColor = rend.material.color;
            rend.material.color = oldColor - new Color(darkenAmount, darkenAmount, darkenAmount);
        }
    }
}
