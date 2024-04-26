using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseProjectile
{
    /// <summary> ������ ���� �������� </summary>
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSpeed;
        _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime;
        
        // _lifeTime ���� ����
        StartCoroutine(LifecycleCoroutine());
    }

    /// <summary> �浹 �˻� </summary>
    // baseProjectile�� OnTriggerEnter2D�� �������̵��Ͽ� EMP�� �ı����ϴ� ��� ���� �� �÷��̾�� �浹 �� �������� ������ ��� �߰�
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹���� ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹");
            PlayerStat.Instance.TakeDamage();
        }
    }
}
