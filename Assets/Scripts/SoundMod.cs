using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMod : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public void playSound()
    {
        source.PlayOneShot(clip);
    }
}
