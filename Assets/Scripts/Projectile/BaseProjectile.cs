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

    /****************************************************************************
                                   Unity Callbacks
    ****************************************************************************/
    /// <summary> �ʱ�ȭ </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        // ����ü ���� �������� _lifeTime ���� ����
        // _speed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed;
        // _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileLifeTime;
        // StartCoroutine(LifecycleCoroutine());
    }

    protected void Update()
    {
        CheckOutOfBounds();
    }

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    /// <summary> �� ���� �˻� </summary>
    protected void CheckOutOfBounds()
    {
        // �������κ��� 20 �̻� �������� ����
        if (transform.position.magnitude > 30.0f)
        {
            DestroyProjectile();
        }
    }

    /// <summary> ���� �ֱ� ���� �ڷ�ƾ </summary>
    protected virtual IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        DestroyProjectile();  // ������ �ð�(_lifetime)�� ������ �߻�ü�� �ı�
    }

    /****************************************************************************
                                abstract and virtual Methods
    ****************************************************************************/
    /// <summary> �̵� </summary>
    protected virtual void Move()
    {
        rb.velocity = moveDirection.normalized * _speed;
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

    /****************************************************************************
                              public Methods
    ****************************************************************************/
    /// <summary> ���� ���� </summary>
    // �ڰ��� �ͷ��� ��쿡�� ���� ��ġ
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
        Move();
    }

    /// <summary> �߻�ü �ı� </summary>
    public void DestroyProjectile()
    {
        //Debug.Log("����ü Ǯ�� ��ȯ");
        ProjectilePoolManager.Instance.Return(this.GetType().Name, this);
        StopAllCoroutines();
    }
}
