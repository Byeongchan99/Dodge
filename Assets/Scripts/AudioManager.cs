using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip mainAudioClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            PlayBGM(mainAudioClip);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMainBGM()
    {
        audioSource.clip = mainAudioClip;
        audioSource.Play();
    }

    public void PlayBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
