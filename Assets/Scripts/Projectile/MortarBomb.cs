using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBomb : BaseProjectile
{
    public Transform target;  // 목표 지점
    [SerializeField] private float firingAngle = 45.0f;  // 발사 각도, 기본값 45도

    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
    }

    /// <summary> 포물선 궤적 계산 및 설정 </summary>
    protected override void Move()
    {
        Vector3 targetDir = target.position - transform.position;  // 목표 지점 방향
        float x = targetDir.x;  // 수평 거리
        float y = targetDir.y;  // 수직 거리

        float gravity = Physics2D.gravity.magnitude;  // 중력가속도
        float radianAngle = firingAngle * Mathf.Deg2Rad;  // 발사 각도를 라디안으로 변환

        float projectileVelocity = Mathf.Sqrt((gravity * x * x) / (2 * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle) * (y - Mathf.Tan(radianAngle) * x)));  // 초기 속도 계산

        Vector2 velocity = new Vector2(projectileVelocity * Mathf.Cos(radianAngle), projectileVelocity * Mathf.Sin(radianAngle));  // X, Y 방향 초기 속도

        SetDirection(velocity);  // 계산된 속도로 방향 설정
    }
}
