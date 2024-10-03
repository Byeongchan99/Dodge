using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button myButton;      // 버튼 컴포넌트
    public AudioClip AudioClip;   // 효과음

    void Awake()
    {
        myButton = GetComponent<Button>();  // 버튼 컴포넌트 할당

        // 버튼의 클릭 이벤트에 함수를 연결
        if (myButton != null && AudioClip != null)
        {
            myButton.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound()
    {
        // 효과음 재생
        AudioManager.instance.sfxAudioSource.PlayOneShot(AudioClip);
    }
}
