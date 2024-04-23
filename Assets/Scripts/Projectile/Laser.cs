using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseProjectile
{
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed;
    }

    protected override void Move()
    {
        StartCoroutine(ActiveLaser());
    }

    /// <summary> _lifeTime ���� ������ ���� </summary>
    private IEnumerator ActiveLaser()
    {
        yield return new WaitForSeconds(_lifeTime);
        DestroyProjectile();
    }

    /// <summary> �浹 �˻� </summary>
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
