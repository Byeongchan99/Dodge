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
        // �����̴� �ʱⰪ ���� (�⺻���� 1�� �����Ͽ� �ִ� ����)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // �����̴� �̺�Ʈ ��� ���� ���� �̺�Ʈ ����
        bgmSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        // �����̴� �̺�Ʈ�� �޼��� ����
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // �ʱ� ���� ����
        SetBGMVolume(bgmSlider.value);
        SetSFXVolume(sfxSlider.value);

        // Mute ��ư �̺�Ʈ ���
        bgmMuteButton.onClick.AddListener(ToggleBGMMute);
        sfxMuteButton.onClick.AddListener(ToggleSFXMute);

        // Mute ��ư �ؽ�Ʈ �ʱ�ȭ
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

        float mixerVolume = sliderValue * 100f - 80f;
        audioMixer.SetFloat("BGMVolume", mixerVolume);
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);

        int displayVolume = (int)(sliderValue * 100f);
        bgmVolumeText.text = $"{displayVolume}";
    }

    public void SetSFXVolume(float sliderValue)
    {
        if (isSFXMuted)
        {
            isSFXMuted = false;
            UpdateSFXMuteButton();
        }

        float mixerVolume = sliderValue * 100f - 80f;
        audioMixer.SetFloat("SFXVolume", mixerVolume);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);

        int displayVolume = (int)(sliderValue * 100f);
        sfxVolumeText.text = $"{displayVolume}";
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
