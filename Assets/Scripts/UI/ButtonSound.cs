using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button myButton;      // ��ư ������Ʈ
    public AudioClip AudioClip;   // ȿ����

    void Awake()
    {
        myButton = GetComponent<Button>();  // ��ư ������Ʈ �Ҵ�

        // ��ư�� Ŭ�� �̺�Ʈ�� �Լ��� ����
        if (myButton != null && AudioClip != null)
        {
            myButton.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound()
    {
        // ȿ���� ���
        AudioManager.instance.sfxAudioSource.PlayOneShot(AudioClip);
    }
}
