using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPBombEffect : ItemEffect
{
    private BaseEffect EMPEffect;
    public EMPBombEffect(float duration, GameObject target) : base(duration, target) { }

    public override void ApplyEffect()
    {
        Debug.Log("EMP ������ ȿ�� ����");
        EMPEffect = EffectPoolManager.Instance.Get("EMPEffect");
        EMPEffect.transform.position = PlayerStat.Instance.currentPosition.position;
        EMPEffect.gameObject.SetActive(true);
    }

    public override void RemoveEffect()
    {
        Debug.Log("EMP ������ ȿ�� ����");
        if (EMPEffect != null)
        {
            EffectPoolManager.Instance.Return("EMPEffect", EMPEffect);  // ĳ�õ� ȿ�� �ν��Ͻ��� Ǯ�� ��ȯ
            EMPEffect = null;  // ���� ����
        }
    }
}
