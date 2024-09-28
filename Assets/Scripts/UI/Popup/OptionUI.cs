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

        // 슬라이더 초기값 설정 (PlayerPrefs에서 불러오기)
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolumeSlider", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumeSlider", 1f);

        // Mute 상태 초기화
        isBGMMuted = (bgmSlider.value == 0f);
        isSFXMuted = (sfxSlider.value == 0f);

        // 슬라이더 이벤트 등록 전에 기존 이벤트 제거
        bgmSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        // 슬라이더 이벤트에 메서드 연결
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        // 초기 볼륨 설정
        UpdateBGMVolumeUI(bgmSlider.value);
        UpdateSFXVolumeUI(sfxSlider.value);

        // Mute 버튼 이벤트 등록
        bgmMuteButton.onClick.AddListener(ToggleBGMMute);
        sfxMuteButton.onClick.AddListener(ToggleSFXMute);

        // Mute 버튼 텍스트 초기화
        UpdateBGMMuteButton();
        UpdateSFXMuteButton();

        // AudioManager에 초기 볼륨 전달
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
        // Mute 상태 업데이트
        isBGMMuted = (sliderValue == 0f);
        UpdateBGMMuteButton();

        // 볼륨 텍스트 업데이트
        bgmVolumeText.text = $"{(int)(sliderValue * 100)}";
    }

    private void UpdateSFXVolumeUI(float sliderValue)
    {
        // Mute 상태 업데이트
        isSFXMuted = (sliderValue == 0f);
        UpdateSFXMuteButton();

        // 볼륨 텍스트 업데이트
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
