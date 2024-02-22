using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenOnDamageMod : MonoBehaviour
{
    public Health health;
    public float darkenAmount = 0.1f;

    public void Darken()
    {
        if (health.gameObject.TryGetComponent<Renderer>(out Renderer rend))
        {
            var oldColor = rend.material.color;
            rend.material.color = oldColor - new Color(darkenAmount, darkenAmount, darkenAmount);
        }
    }
}
