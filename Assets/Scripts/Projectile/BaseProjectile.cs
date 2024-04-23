using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _lifeTime;
    protected Vector2 moveDirection;

    /// <summary> 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed;
        _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileLifeTime;
        StartCoroutine(LifecycleCoroutine());
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
        rb.velocity = moveDirection.normalized * _speed;
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
        Debug.Log("투사체 풀에 반환");
        ProjectilePoolManager.Instance.Return(this.GetType().Name, this);
        StopAllCoroutines();
    }

    /// <summary> 충돌 검사 </summary>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 충돌했을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");
            DestroyProjectile();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // EMP와 충돌했을 때
        if (collision.gameObject.CompareTag("EMP"))
        {
            Debug.Log("EMP와 충돌");
            DestroyProjectile();
        }
    }

    /// <summary> 생명 주기 관리 코루틴 </summary>
    protected IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);  // 지정된 시간(_lifetime) 동안 대기
        DestroyProjectile();  // 시간이 지나면 발사체를 파괴
    }
}
