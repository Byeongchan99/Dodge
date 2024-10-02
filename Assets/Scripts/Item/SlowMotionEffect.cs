using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionEffect : ItemEffect
{
    public override void ApplyEffect()
    {
        Debug.Log("���� ��� ������ ȿ�� ����");
        GameManager.Instance.StartSlowEffect(_duration); // ���ο� ��� ȿ�� ����

        AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip); // ������ ȿ���� ���

        if (!GameManager.Instance.isItemSlowMotion) // �����ۿ� ���� ���ο� ��� ȿ���� ���� ���� �ƴ϶��
        {
            PlayerStat.Instance.currentMoveSpeed *= 250; // ���ο� ȿ���� �°� �ӵ� ����
            GameManager.Instance.isItemSlowMotion = true;
        }
        GameManager.Instance.slowMotionItemCount++;
    }

    public override void RemoveEffect()
    {
        Debug.Log("���� ��� ������ ȿ�� ����");

        // �����ۿ� ���� ���ο� ��� ȿ���� ���� ���� ��
        if (GameManager.Instance.isItemSlowMotion && GameManager.Instance.slowMotionItemCount == 1)
        {
            PlayerStat.Instance.currentMoveSpeed /= 250;  // ���� �ӵ��� ����
            GameManager.Instance.isItemSlowMotion = false;
        }
        GameManager.Instance.slowMotionItemCount--;
    }
}
