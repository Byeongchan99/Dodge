using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    float BGMVolume;
    float SFXVolume;

    public void SetBGMVolume(float volume)
    {
        BGMVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
    }

    public void ApplyVolume()
    {
        // BGMVolume, SFXVolume Àû¿ë
    }
}
