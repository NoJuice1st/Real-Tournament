using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TMP_Text waveText;
    public AudioClip clip;

    public async void AnnounceWave(int wave)
    {
        AudioSystem.Play(clip);
        waveText.text = $"Wave {wave + 1} started!";
        await new WaitForSeconds(2f);
        waveText.text = "";
    }
}
