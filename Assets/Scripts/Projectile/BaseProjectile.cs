using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    /****************************************************************************
                              private Fields
    ****************************************************************************/
    protected Rigidbody2D rb;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _lifeTime;
    protected Vector2 moveDirection;
    protected bool isDestroyed = false;

    /****************************************************************************
                                   Unity Callbacks
    ****************************************************************************/
    /// <summary> 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        isDestroyed = false;
        // 투사체 스탯 가져오고 _lifeTime 동안 유지
        // _speed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed;
        // _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileLifeTime;
        // StartCoroutine(LifecycleCoroutine());
    }

    protected void Update()
    {
        CheckOutOfBounds(); // 맵을 벗어났는지 검사
    }

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    /// <summary> 맵 범위 검사 </summary>
    protected void CheckOutOfBounds()
    {
        // 원점으로부터 20 이상 떨어지면 삭제
        if (transform.position.magnitude > 30.0f)
        {
            Debug.Log("맵 범위 벗어남");
            DestroyProjectile();
        }
    }

    /// <summary> 생명 주기 관리 코루틴 </summary>
    protected virtual IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        DestroyProjectile();  // 지정된 시간(_lifetime)이 지나면 발사체를 파괴
    }

    /****************************************************************************
                                abstract and virtual Methods
    ****************************************************************************/
    /// <summary> 이동 </summary>
    protected virtual void Move()
    {
        rb.velocity = moveDirection.normalized * _speed;
    }

    /// <summary> 충돌 검사 </summary>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어의 서있는 영역과 충돌했을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌 - test");
            DestroyProjectile();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyed) return;  // 이미 파괴된 상태면 무시

        // 플레이어와 충돌했을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌 - test");
            DestroyProjectile();
        }

        // EMP와 충돌했을 때
        if (collision.gameObject.CompareTag("EMP"))
        {
            Debug.Log("EMP와 충돌 - test");
            DestroyProjectile();
        }
    }

    /****************************************************************************
                              public Methods
    ****************************************************************************/
    /// <summary> 방향 설정 </summary>
    // 박격포 터렛의 경우에는 폭발 위치
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
        Move();
    }

    /// <summary> 발사체 파괴 </summary>
    public void DestroyProjectile()
    {
        if (isDestroyed) return;  // 이미 파괴된 상태면 무시
        isDestroyed = true;
        Debug.Log("투사체 풀에 반환 - test");
        ProjectilePoolManager.Instance.Return(this.GetType().Name, this);
        StopAllCoroutines();
    }
}
