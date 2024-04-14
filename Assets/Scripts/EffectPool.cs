using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public GameObject EMPPrefab;
    // 다른 이펙트 프리팹도 추가

    public Transform effectContainer;

    private void Start()
    {
        EffectPoolManager.Instance.CreatePool(EMPPrefab.GetComponent<EMPEffect>(), 10, effectContainer);
        // 다른 이펙트 풀도 생성
    }
}
