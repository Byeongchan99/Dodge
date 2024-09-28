using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Text bgmVolumeText;
    public Text sfxVolumeText;

    public Button bgmMuteButton;
    public Button sfxMuteButton;

    private bool isBGMMuted = false;
    private bool isSFXMuted = false;

    void Awake()
    {
        Debug.Log("OptionUI Awake");

        // �����̴� �ʱⰪ ���� (PlayerPrefs���� �ҷ�����)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolumeSlider", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumeSlider", 1f);

        // Mute ���� �ʱ�ȭ
        isBGMMuted = (bgmSlider.value == 0f);
        isSFXMuted = (sfxSlider.value == 0f);

        // �����̴� �̺�Ʈ ��� ���� ���� �̺�Ʈ ����
        bgmSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        // �����̴� �̺�Ʈ�� �޼��� ����
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        // �ʱ� ���� ����
        UpdateBGMVolumeUI(bgmSlider.value);
        UpdateSFXVolumeUI(sfxSlider.value);

        // Mute ��ư �̺�Ʈ ���
        bgmMuteButton.onClick.AddListener(ToggleBGMMute);
        sfxMuteButton.onClick.AddListener(ToggleSFXMute);

        // Mute ��ư �ؽ�Ʈ �ʱ�ȭ
        UpdateBGMMuteButton();
        UpdateSFXMuteButton();

        // AudioManager�� �ʱ� ���� ����
        AudioManager.instance.SetBGMVolume(bgmSlider.value);
        AudioManager.instance.SetSFXVolume(sfxSlider.value);
    }

    private void OnBGMVolumeChanged(float sliderValue)
    {
        UpdateBGMVolumeUI(sliderValue);
        AudioManager.instance.SetBGMVolume(sliderValue);
        PlayerPrefs.SetFloat("BGMVolumeSlider", sliderValue);
    }

    private void OnSFXVolumeChanged(float sliderValue)
    {
        UpdateSFXVolumeUI(sliderValue);
        AudioManager.instance.SetSFXVolume(sliderValue);
        PlayerPrefs.SetFloat("SFXVolumeSlider", sliderValue);
    }

    private void UpdateBGMVolumeUI(float sliderValue)
    {
        // Mute ���� ������Ʈ
        isBGMMuted = (sliderValue == 0f);
        UpdateBGMMuteButton();

        // ���� �ؽ�Ʈ ������Ʈ
        bgmVolumeText.text = $"{(int)(sliderValue * 100)}";
    }

    private void UpdateSFXVolumeUI(float sliderValue)
    {
        // Mute ���� ������Ʈ
        isSFXMuted = (sliderValue == 0f);
        UpdateSFXMuteButton();

        // ���� �ؽ�Ʈ ������Ʈ
        sfxVolumeText.text = $"{(int)(sliderValue * 100)}";
    }

    public void ToggleBGMMute()
    {
        if (isBGMMuted)
        {
            bgmSlider.value = PlayerPrefs.GetFloat("PreviousBGMVolume", 0.5f);
            isBGMMuted = false;
        }
        else
        {
            PlayerPrefs.SetFloat("PreviousBGMVolume", bgmSlider.value);
            bgmSlider.value = 0f;
            isBGMMuted = true;
        }

        UpdateBGMMuteButton();
    }

    public void ToggleSFXMute()
    {
        if (isSFXMuted)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("PreviousSFXVolume", 0.5f);
            isSFXMuted = false;
        }
        else
        {
            PlayerPrefs.SetFloat("PreviousSFXVolume", sfxSlider.value);
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
