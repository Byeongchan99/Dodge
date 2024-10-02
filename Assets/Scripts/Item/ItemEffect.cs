using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    [SerializeField] protected float _duration; // ȿ�� ���� �ð�
    [SerializeField] protected GameObject target;

    public AudioClip audioClip; // ������ ȿ����

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
