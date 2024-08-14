using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    /// <summary> �� ������ ȿ�� </summary>
    protected ItemEffect itemEffect;

    /// <summary> �������� �ʵ忡 �����ִ� �ð� </summary>
    [SerializeField] protected float itemRemainTime = 20f;

    /// <summary> Fade Effect ���� </summary>
    private FadeEffect fadeEffect;

    private void Awake()
    {
        fadeEffect = GetComponent<FadeEffect>();
    }

    private void OnEnable()
    {
        InitItem();
        fadeEffect.StartFadeIn(0.5f, 0.1f);
        StartCoroutine(ItemDisableAfterRemainTime(itemRemainTime)); // itemRemainTime ���� ������ ��Ȱ��ȭ
    }

    /// <summary> ������ �ʱ�ȭ </summary>
    protected virtual void InitItem()
    {
        // ������ ȿ�� �ʱ�ȭ    
    }

    /// <summary> �������� �ʵ忡 �����ִ� �ð� ���� ������ ��Ȱ��ȭ ���� </summary>
    private IEnumerator ItemDisableAfterRemainTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(StartDisableItem());
    }

    /// <summary> ������ ��Ȱ��ȭ ���� </summary>
    IEnumerator StartDisableItem()
    {
        fadeEffect.StartFadeOut(0.5f, 0.1f);
        yield return new WaitForSeconds(1.5f);
        DisableItem();
    }

    /// <summary> ������ ��Ȱ��ȭ </summary>
    protected void DisableItem()
    {     
        ItemPoolManager.Instance.Return(this.GetType().Name, this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ �浹 ����
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������ ȹ�� ó��
            Debug.Log("������ ȹ��");
            PlayerStat.Instance.ApplyItemEffect(itemEffect);
            StartCoroutine(StartDisableItem());
        }
    }
}
