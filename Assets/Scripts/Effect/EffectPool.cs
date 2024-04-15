using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public GameObject EMPPrefab;
    // �ٸ� ����Ʈ �����յ� �߰�

    public Transform effectContainer;

    private void Start()
    {
        EffectPoolManager.Instance.CreatePool(EMPPrefab.GetComponent<EMPEffect>(), 10, effectContainer);
        // �ٸ� ����Ʈ Ǯ�� ����
    }
}
