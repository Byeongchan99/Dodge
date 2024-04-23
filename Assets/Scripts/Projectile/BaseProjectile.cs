using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _lifeTime;
    protected Vector2 moveDirection;

    /// <summary> �ʱ�ȭ </summary>
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

    /// <summary> ���� ���� </summary>
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
        Move();
    }

    /// <summary> �̵� </summary>
    protected virtual void Move()
    {
        rb.velocity = moveDirection.normalized * _speed;
    }

    /// <summary> �� ���� �˻� </summary>
    protected void CheckOutOfBounds()
    {
        // �������κ��� 20 �̻� �������� ����
        if (transform.position.magnitude > 30.0f)
        {
            DestroyProjectile();
        }
    }

    /// <summary> �߻�ü �ı� </summary>
    protected void DestroyProjectile()
    {
        Debug.Log("����ü Ǯ�� ��ȯ");
        ProjectilePoolManager.Instance.Return(this.GetType().Name, this);
        StopAllCoroutines();
    }

    /// <summary> �浹 �˻� </summary>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾�� �浹���� ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹");
            DestroyProjectile();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // EMP�� �浹���� ��
        if (collision.gameObject.CompareTag("EMP"))
        {
            Debug.Log("EMP�� �浹");
            DestroyProjectile();
        }
    }

    /// <summary> ���� �ֱ� ���� �ڷ�ƾ </summary>
    protected IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);  // ������ �ð�(_lifetime) ���� ���
        DestroyProjectile();  // �ð��� ������ �߻�ü�� �ı�
    }
}
