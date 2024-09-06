using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpEffect : ItemEffect
{
    private float _moveSpeedIncrease;
    private GhostEffect ghostEffect;  // �ܻ� ȿ��

    public MoveSpeedUpEffect(float duration, GameObject target, float moveSpeedIncrease) : base(duration, target)
    {
        this._moveSpeedIncrease = moveSpeedIncrease;
        // Target ������Ʈ���� GhostEffect ������Ʈ�� ������
        this.ghostEffect = target.GetComponent<GhostEffect>();
    }

    public override void ApplyEffect()
    {
        // �̵� �ӵ��� ������Ŵ
        Debug.Log("�̵� �ӵ� ���� ������ ȿ�� ����" + _moveSpeedIncrease);
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
