using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpEffect : ItemEffect
{
    private float _moveSpeedIncrease;
    private GhostEffect ghostEffect;  // �ܻ� ȿ��

    protected override void Awake()
    {
        base.Awake();
        // Target ������Ʈ���� GhostEffect ������Ʈ�� ������
        this.ghostEffect = target.GetComponent<GhostEffect>();
    }

    public override void ApplyEffect()
    {
        // �̵� �ӵ��� ������Ŵ
        Debug.Log("�̵� �ӵ� ���� ������ ȿ�� ����" + _moveSpeedIncrease);
        AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip); // ������ ȿ���� ���

        PlayerStat.Instance.currentMoveSpeed *= _moveSpeedIncrease; // �ӵ� ����
        ghostEffect.moveSpeedUpItemCount++;
        ghostEffect.isMakeGhost = true;  // �ܻ� ���� ����
    }

    public override void RemoveEffect()
    {
        // �̵� �ӵ� ����
        Debug.Log("�̵� �ӵ� ���� ������ ȿ�� ����" + _moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed /= _moveSpeedIncrease; // �ӵ� ����

        if (ghostEffect.moveSpeedUpItemCount == 1)// �ܻ� ȿ���� ���� ���̶��
        {
            ghostEffect.isMakeGhost = false;  // �ܻ� ���� �ߴ�
        }
        ghostEffect.moveSpeedUpItemCount--;
    }
}
