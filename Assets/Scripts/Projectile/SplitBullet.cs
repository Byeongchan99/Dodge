using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBullet : Bullet
{
    [SerializeField] float _splitTime = 1f;
    [SerializeField] string _bulletPoolName;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Split());
    }

    IEnumerator Split()
    {
        yield return new WaitForSeconds(_splitTime);

        // ���� �Ѿ��� ������ �������� �п�
        Quaternion currentRotation = transform.rotation;
        Vector2 currentDirection = moveDirection;

        // ������Ʈ Ǯ���� �п� �Ѿ� ��������
        BaseProjectile bullet1 = ProjectilePoolManager.Instance.Get(_bulletPoolName);
        BaseProjectile bullet2 = ProjectilePoolManager.Instance.Get(_bulletPoolName);
        BaseProjectile bullet3 = ProjectilePoolManager.Instance.Get(_bulletPoolName);

        if (bullet1 != null && bullet2 != null && bullet3 != null)
        {
            bullet1.transform.position = transform.position;
            bullet1.transform.rotation = currentRotation * Quaternion.Euler(0, 0, 45);
            bullet1.SetDirection(Quaternion.Euler(0, 0, 45) * currentDirection); // ���� ����

            bullet2.transform.position = transform.position;
            bullet2.transform.rotation = currentRotation;
            bullet2.SetDirection(currentDirection); // ���� ����

            bullet3.transform.position = transform.position;
            bullet3.transform.rotation = currentRotation * Quaternion.Euler(0, 0, -45);
            bullet3.SetDirection(Quaternion.Euler(0, 0, -45) * currentDirection); // ���� ����

            bullet1.gameObject.SetActive(true);
            bullet2.gameObject.SetActive(true);
            bullet3.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get split bullets from pool.");
        }

        DestroyProjectile();
    }
}
