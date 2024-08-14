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
        fadeEffect.StartFadeIn(0.5f);
        Invoke("DestroyItem", itemRemainTime); // 10�� �� DestroyItem �޼ҵ� ȣ��
    }

    /// <summary> ������ �ʱ�ȭ </summary>
    protected virtual void InitItem()
    {
        // ������ ȿ�� �ʱ�ȭ    
    }

    /// <summary> ������ �ı� </summary>
    protected void DestroyItem()
    {
        CancelInvoke("DestroyItem"); // �ߺ� ȣ�� ����
        fadeEffect.StartFadeOut(0.5f);
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
            DestroyItem();
        }
    }
}
