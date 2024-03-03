using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    static AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public static void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}