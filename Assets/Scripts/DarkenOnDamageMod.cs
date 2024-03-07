using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenOnDamageMod : MonoBehaviour
{
    private Health health;
    public float darkenAmount = 0.1f;
    public bool isMultipleMaterials = false;
    public List<Renderer> renderers;
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
        if (!isMultipleMaterials && gameObject.TryGetComponent<Renderer>(out Renderer rend))
        {
            var oldColor = rend.material.color;
            rend.material.color = oldColor - new Color(darkenAmount, darkenAmount, darkenAmount);
        }
        else if (isMultipleMaterials)
        {
            foreach(Renderer render in renderers)
            {
                var oldColor = render.material.color; 
                render.material.color = oldColor - new Color(darkenAmount, darkenAmount, darkenAmount);
            }
        }
    }
}
