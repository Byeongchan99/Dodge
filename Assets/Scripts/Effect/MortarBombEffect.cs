using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBombEffect : BaseEffect
{
    PolygonCollider2D effectCollider;
    float delayTime;

    private void Awake()
    {
        effectCollider = GetComponent<PolygonCollider2D>();
        effectCollider.enabled = false;
        delayTime = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
    }

    /// <summary> �ʱ�ȭ /// </summary>
    private void OnEnable()
    {
        StartCoroutine(ActiveMortarBombEffect());
    }

    // MortarBomb�� ���� �� �ݶ��̴� Ȱ��ȭ �� �� ����Ʈ ��Ȱ��ȭ
    IEnumerator ActiveMortarBombEffect()
    {
        yield return new WaitForSeconds(delayTime);  // ��ź�� ���� ������ ���
        effectCollider.enabled = true;
        yield return new WaitForSeconds(0.1f); // �ݶ��̴� Ȱ��ȭ �� 0.1�� ���
        effectCollider.enabled = false;
        DestroyEffect();
    }

    /// <summary> �浹 �˻� </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹���� ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹");
            PlayerStat.Instance.TakeDamage();
        }
    }
}
