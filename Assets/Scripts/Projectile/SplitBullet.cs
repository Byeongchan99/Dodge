using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBullet : Bullet
{
    [SerializeField] float _splitTime;
    [SerializeField] string _bulletPoolName;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Split());
    }

    IEnumerator Split()
    {
        // �п� Ÿ�̹��� �ӵ��� �ݺ���ϵ��� ����
        _splitTime = 4 / _speed;
        yield return new WaitForSeconds(_splitTime);

        // ���� �Ѿ��� ������ �������� �п�
        Quaternion currentRotation = transform.rotation;
        Vector2 currentDirection = moveDirection;

        // ������Ʈ Ǯ���� �⺻ �Ѿ� ��������
        Bullet bullet1 = ProjectilePoolManager.Instance.Get(_bulletPoolName) as Bullet;
        //Bullet bullet2 = ProjectilePoolManager.Instance.Get(_bulletPoolName) as Bullet;
        Bullet bullet3 = ProjectilePoolManager.Instance.Get(_bulletPoolName) as Bullet;
        Vector3 bulletSize = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize;

        if (bullet1 != null && bullet3 != null) // bullet1 != null && bullet2 != null && bullet3 != null
        {
            // �Ѿ� ��ġ�� ȸ�� ����
            bullet1.transform.position = transform.position;
            bullet1.transform.rotation = currentRotation * Quaternion.Euler(0, 0, 15);
            // ũ�� ����
            bullet1.transform.localScale = bulletSize;
            // ���� ����
            bullet1.SetDirection(Quaternion.Euler(0, 0, 15) * currentDirection);

            /*
            bullet2.transform.position = transform.position;
            bullet2.transform.rotation = currentRotation;
            bullet2.transform.localScale = bulletSize;
            bullet2.SetDirection(currentDirection); // ���� ����
            */

            bullet3.transform.position = transform.position;
            bullet3.transform.rotation = currentRotation * Quaternion.Euler(0, 0, -15);
            bullet3.transform.localScale = bulletSize;
            bullet3.SetDirection(Quaternion.Euler(0, 0, -15) * currentDirection); // ���� ����

            bullet1.gameObject.SetActive(true);
            //bullet2.gameObject.SetActive(true);
            bullet3.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get split bullets from pool.");
        }

        DestroyProjectile();
    }
}
