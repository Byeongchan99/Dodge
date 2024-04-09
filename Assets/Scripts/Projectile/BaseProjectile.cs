using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float speed = 5.0f;
    public Vector2 moveDirection;

    /// <summary> �ʱ�ȭ </summary>
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

    /// <summary> ���� ���� </summary>
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
        Move();
    }

    /// <summary> �̵� </summary>
    protected virtual void Move()
    {
        rb.velocity = moveDirection.normalized * speed;
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
        ProjectilePoolManager.Instance.Return(this.GetType().Name, this);
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
        // ���߿� EMP�� �浹���� ��
        else if (collision.gameObject.CompareTag("EMP"))
        {
            Debug.Log("EMP�� �浹");
            DestroyProjectile();
        }
    }
}
