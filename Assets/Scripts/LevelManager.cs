using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class LevelManager : MonoBehaviour
{
    public TMP_Text waveText;
    public GameObject waveBanner;
    public AudioClip clip;

    public void AnnounceWave(int wave)
    {
        AudioSystem.Play(clip);
        //waveBanner.LeanScale(new Vector3(1,0,1), 0f);
        waveBanner.LeanMoveLocalX(0f, 2f).setEaseInOutExpo().setOnComplete(ShrinkWave);

        waveText.gameObject.LeanScale(new Vector3(1,1,1), 1f).setEaseInOutExpo().setOnComplete(ShrinkText);
        waveText.text = $"Wave {wave + 1} started!";
        
    }

    public void ShrinkWave()
    {
        waveBanner.LeanMoveLocalX(-800f, 2f).setEaseInOutExpo();

    }

    public async void ShrinkText()
    {
        await new WaitForSeconds(1.5f);
        waveText.gameObject.LeanScale(new Vector3(1,0,1), 1f).setEaseInOutExpo();
    }
}
