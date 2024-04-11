using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    protected ItemEffect itemEffect;

    private void OnEnable()
    {
        InitItem();
    }

    /// <summary> ������ �ʱ�ȭ </summary>
    protected virtual void InitItem()
    {
        // ������ ȿ�� �ʱ�ȭ
    }

    /// <summary> ������ �ı� </summary>
    protected void DestroyItem()
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
            DestroyItem();
        }
    }
}
