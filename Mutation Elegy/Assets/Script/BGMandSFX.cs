using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGMandSFX : MonoBehaviour
{    
    [Header("²V­µ¾¹")]
    public AudioMixer audmixer;

    public void SetMasterVolume(Slider sl)
    {
        audmixer.SetFloat("MasterVolume", sl.value);
    }

    public void SetBGMVolume(Slider sl)
    {
        audmixer.SetFloat("BGMVolume", sl.value);
    }

    public void SetSFXVolume(Slider sl)
    {
        audmixer.SetFloat("SFXVolume", sl.value);
    }    

    public void BackToTitle()
    {
        gameObject.SetActive(false);
    }
}
