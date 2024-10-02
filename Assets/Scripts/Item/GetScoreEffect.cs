using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScoreEffect : ItemEffect
{
    public override void ApplyEffect()
    {
        Debug.Log("���� ȹ�� ������ ȿ�� ����");
        AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip); // ������ ȿ���� ���

        ScoreManager.Instance.AddScore(10f); // ���� ȹ��
    }

    public override void RemoveEffect()
    {
        Debug.Log("���� ȹ�� ������ ȿ�� ����"); // �� �ǹ̾���
    }
}
