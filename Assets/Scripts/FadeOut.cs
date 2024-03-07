using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeOut : MonoBehaviour
{

    public float fadeAmount = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fade", 2f, 1f);
    }

    public void Fade()
    {
        if(gameObject.TryGetComponent<DecalProjector>(out DecalProjector decalProj))
        {
            decalProj.fadeFactor -= fadeAmount;

            if(decalProj.fadeFactor <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
