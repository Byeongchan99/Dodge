using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    [SerializeField] protected float _duration; // 효과 적용 시간
    [SerializeField] protected GameObject target;

    public AudioClip audioClip; // 아이템 효과음

    protected void Awake()
    {
        target = PlayerStat.Instance.gameObject;
    }

    public float GetDuration()
    {
        return _duration;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}
