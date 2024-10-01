using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioMixer audioMixer;
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    [SerializeField] private AudioClip mainAudioClip;

    void Awake()
    {
        Debug.Log("AudioManager Awake");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // AudioMixer 초기 볼륨 설정
        float bgmSliderValue = PlayerPrefs.GetFloat("BGMVolumeSlider", 1f);
        float sfxSliderValue = PlayerPrefs.GetFloat("SFXVolumeSlider", 1f);
        SetBGMVolume(bgmSliderValue);
        SetSFXVolume(sfxSliderValue);

        PlayMainBGM();
    }

    public void PlayMainBGM()
    {
        Debug.Log("PlayMainBGM");

        float bgmVolume;
        if (audioMixer.GetFloat("BGMVolume", out bgmVolume))
        {
            Debug.Log($"Current BGMVolume: {bgmVolume} dB");
        }
        else
        {
            Debug.LogError("Failed to get BGMVolume parameter from AudioMixer.");
        }

        bgmAudioSource.clip = mainAudioClip;
        bgmAudioSource.Play();
    }


    public void PlayBGM(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void SetBGMVolume(float sliderValue)
    {
        float mixerVolume = ConvertSliderValueToMixerVolume(sliderValue);
        audioMixer.SetFloat("BGMVolume", mixerVolume);
        float bgmVolume;
        if (audioMixer.GetFloat("BGMVolume", out bgmVolume))
        {
            Debug.Log($"Current BGMVolume: {bgmVolume} dB");
        }
        else
        {
            Debug.LogError("Failed to get BGMVolume parameter from AudioMixer.");
        }
        Debug.Log($"AudioManager SetBGMVolume: {sliderValue}, {mixerVolume}");
    }

    public void SetSFXVolume(float sliderValue)
    {
        float mixerVolume = ConvertSliderValueToMixerVolume(sliderValue);
        audioMixer.SetFloat("SFXVolume", mixerVolume);
        Debug.Log($"AudioManager SetSFXVolume: {sliderValue}, {mixerVolume}");
    }

    private float ConvertSliderValueToMixerVolume(float sliderValue)
    {
        if (sliderValue <= 0.1f)
        {
            // 슬라이더 값 0 ~ 0.1 매핑 (-80dB ~ -20dB)
            return (sliderValue / 0.1f) * 60f - 80f;
        }
        else
        {
            // 슬라이더 값 0.1 ~ 1.0 매핑 (-20dB ~ +20dB)
            return ((sliderValue - 0.1f) / 0.9f) * 40f - 20f;
        }
    }
}
