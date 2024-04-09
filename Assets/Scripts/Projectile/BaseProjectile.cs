using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float speed = 5.0f;
    public Vector2 moveDirection;

    /// <summary> 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        speed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed;
    }

    protected void Update()
    {
        CheckOutOfBounds();
    }

    /// <summary> 방향 설정 </summary>
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
        Move();
    }

    /// <summary> 이동 </summary>
    protected virtual void Move()
    {
        rb.velocity = moveDirection.normalized * speed;
    }

    /// <summary> 맵 범위 검사 </summary>
    protected void CheckOutOfBounds()
    {
        // 원점으로부터 20 이상 떨어지면 삭제
        if (transform.position.magnitude > 30.0f)
        {
            DestroyProjectile();
        }
    }

    /// <summary> 발사체 파괴 </summary>
    protected void DestroyProjectile()
    {
        ProjectilePoolManager.Instance.Return(this.GetType().Name, this);
    }

    /// <summary> 충돌 검사 </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 때
        if (collision.CompareTag("Player"))
        {
            DestroyProjectile();
        }
        // 나중에 EMP와 충돌했을 때
        else if (collision.CompareTag("EMP"))
        {
            DestroyProjectile();
        }
    }
}
