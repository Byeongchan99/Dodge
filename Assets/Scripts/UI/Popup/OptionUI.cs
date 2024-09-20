using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        // 슬라이더 초기값 설정
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // 초기 볼륨 설정
        SetBGMVolume(bgmSlider.value);
        SetSFXVolume(sfxSlider.value);

        // 슬라이더 이벤트에 메서드 연결
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetBGMVolume(float volume)
    {
        // Audio Mixer의 exposed parameter를 통해 볼륨 조절
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        // 볼륨 값 저장 (옵션)
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private IEnumerator FadeBGM(float targetVolume, float duration)
    {
        float currentVolume;
        audioMixer.GetFloat("BGMVolume", out currentVolume);
        float startVolume = currentVolume;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            audioMixer.SetFloat("BGMVolume", newVolume);
            yield return null;
        }
    }
}
