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

        // 현재 총알의 방향을 기준으로 분열
        Quaternion currentRotation = transform.rotation;
        Vector2 currentDirection = moveDirection;

        // 오브젝트 풀에서 분열 총알 가져오기
        Bullet bullet1 = ProjectilePoolManager.Instance.Get(_bulletPoolName) as Bullet;
        Bullet bullet2 = ProjectilePoolManager.Instance.Get(_bulletPoolName) as Bullet;
        Bullet bullet3 = ProjectilePoolManager.Instance.Get(_bulletPoolName) as Bullet;
        Vector3 bulletSize = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize;

        if (bullet1 != null && bullet2 != null && bullet3 != null)
        {
            // 총알 위치와 회전 설정
            bullet1.transform.position = transform.position;
            bullet1.transform.rotation = currentRotation * Quaternion.Euler(0, 0, 45);
            // 크기 변경
            bullet1.transform.localScale = bulletSize;
            // 방향 설정
            bullet1.SetDirection(Quaternion.Euler(0, 0, 45) * currentDirection);

            bullet2.transform.position = transform.position;
            bullet2.transform.rotation = currentRotation;
            bullet2.transform.localScale = bulletSize;
            bullet2.SetDirection(currentDirection); // 방향 설정

            bullet3.transform.position = transform.position;
            bullet3.transform.rotation = currentRotation * Quaternion.Euler(0, 0, -45);
            bullet3.transform.localScale = bulletSize;
            bullet3.SetDirection(Quaternion.Euler(0, 0, -45) * currentDirection); // 방향 설정

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
