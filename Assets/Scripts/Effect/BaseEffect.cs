using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    /// <summary> ����Ʈ �ı� </summary>
    public void DestroyEffect()
    {
        //Debug.Log("����Ʈ Ǯ�� ��ȯ");
        EffectPoolManager.Instance.Return(this.GetType().Name, this);
        StopAllCoroutines();
    }
}
