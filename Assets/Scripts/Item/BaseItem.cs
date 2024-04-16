using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    protected ItemEffect itemEffect;
    [SerializeField] protected float itemRemainTime = 10f;

    private void OnEnable()
    {
        InitItem();
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
