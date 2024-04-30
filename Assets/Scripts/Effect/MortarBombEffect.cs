using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBombEffect : BaseEffect
{
    PolygonCollider2D effectCollider;
    protected float _delayTime;

    private void Awake()
    {
        effectCollider = GetComponent<PolygonCollider2D>();
        effectCollider.enabled = false;
    }

    /// <summary> �ʱ�ȭ /// </summary>
    protected virtual void OnEnable()
    {
        _delayTime = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;

        StartCoroutine(ActiveMortarBombEffect());
    }

    // MortarBomb�� ���� �� �ݶ��̴� Ȱ��ȭ �� �� ����Ʈ ��Ȱ��ȭ
    protected IEnumerator ActiveMortarBombEffect()
    {
        Debug.Log("delayTime: " + _delayTime);
        yield return new WaitForSeconds(_delayTime);  // ��ź�� ���� ������ ���
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
