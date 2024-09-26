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

    public Text bgmVolumeText;
    public Text sfxVolumeText;

    public Button bgmMuteButton;
    public Button sfxMuteButton;

    private float previousBGMVolume;
    private bool isBGMMuted = false;

    private float previousSFXVolume;
    private bool isSFXMuted = false;

    void Awake()
    {
        // 슬라이더 초기값 설정 (기본값을 1로 설정하여 최대 볼륨)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // 슬라이더 이벤트 등록 전에 기존 이벤트 제거
        bgmSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        // 슬라이더 이벤트에 메서드 연결
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // 초기 볼륨 설정
        SetBGMVolume(bgmSlider.value);
        SetSFXVolume(sfxSlider.value);

        // Mute 버튼 이벤트 등록
        bgmMuteButton.onClick.AddListener(ToggleBGMMute);
        sfxMuteButton.onClick.AddListener(ToggleSFXMute);

        // Mute 버튼 텍스트 초기화
        UpdateBGMMuteButton();
        UpdateSFXMuteButton();
    }

    public void SetBGMVolume(float sliderValue)
    {
        if (isBGMMuted)
        {
            isBGMMuted = false;
            UpdateBGMMuteButton();
        }

        float mixerVolume;

        if (sliderValue <= 0.1f)
        {
            // 슬라이더 값 0 ~ 0.1 매핑 (-80dB ~ -20dB)
            mixerVolume = (sliderValue / 0.1f) * 60f - 80f;
        }
        else
        {
            // 슬라이더 값 0.1 ~ 1.0 매핑 (-20dB ~ +20dB)
            mixerVolume = ((sliderValue - 0.1f) / 0.9f) * 40f - 20f;
        }

        audioMixer.SetFloat("BGMVolume", mixerVolume);
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);

        // 볼륨 텍스트 업데이트 (dB 값 표시)
        bgmVolumeText.text = $"{(int)(sliderValue * 10)}";
    }

    public void SetSFXVolume(float sliderValue)
    {
        if (isSFXMuted)
        {
            isSFXMuted = false;
            UpdateSFXMuteButton();
        }

        float mixerVolume;

        if (sliderValue <= 0.1f)
        {
            // 슬라이더 값 0 ~ 0.1 매핑 (-80dB ~ -20dB)
            mixerVolume = (sliderValue / 0.1f) * 60f - 80f;
        }
        else
        {
            // 슬라이더 값 0.1 ~ 1.0 매핑 (-20dB ~ +20dB)
            mixerVolume = ((sliderValue - 0.1f) / 0.9f) * 40f - 20f;
        }

        audioMixer.SetFloat("SFXVolume", mixerVolume);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);

        // 볼륨 텍스트 업데이트 (dB 값 표시)
        sfxVolumeText.text = $"{(int)(sliderValue * 10)}";
    }

    public void ToggleBGMMute()
    {
        if (isBGMMuted)
        {
            bgmSlider.value = previousBGMVolume;
            isBGMMuted = false;
        }
        else
        {
            previousBGMVolume = bgmSlider.value;
            bgmSlider.value = 0f;
            isBGMMuted = true;
        }

        UpdateBGMMuteButton();
    }

    public void ToggleSFXMute()
    {
        if (isSFXMuted)
        {
            sfxSlider.value = previousSFXVolume;
            isSFXMuted = false;
        }
        else
        {
            previousSFXVolume = sfxSlider.value;
            sfxSlider.value = 0f;
            isSFXMuted = true;
        }

        UpdateSFXMuteButton();
    }

    private void UpdateBGMMuteButton()
    {
        bgmMuteButton.GetComponentInChildren<Text>().text = isBGMMuted ? "Unmute" : "Mute";
    }

    private void UpdateSFXMuteButton()
    {
        sfxMuteButton.GetComponentInChildren<Text>().text = isSFXMuted ? "Unmute" : "Mute";
    }
}
