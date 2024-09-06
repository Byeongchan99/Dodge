using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Rocket : BaseProjectile
{
    [SerializeField] FadeEffect fadeEffect;

    public Transform target;  // ��ǥ (�÷��̾�)
    public float rotateSpeed = 10f;

    protected override void Awake()
    {
        base.Awake();
        fadeEffect = GetComponent<FadeEffect>();
    }

    /// <summary> ���� ���� �������� </summary>
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed;
        _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime;

        fadeEffect.StartFadeIn(0f, 0f);  // ���� ���� �� ���� �ʱ�ȭ

        // _lifeTime ���� ����
        StartCoroutine(LifecycleCoroutine());
    }

    /// <summary> ���� ������ ���� </summary>
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)(target.position - transform.position);
            direction.Normalize();

            // ���� ȸ�� ����
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            // -0.4f ~ 0.4f ������ ������ ����
            rotateAmount = Mathf.Clamp(rotateAmount, -0.4f, 0.4f);

            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * _speed;
        }
    }

    /// <summary> _lifeTime ���� ���� �� �ı� </summary>
    protected override IEnumerator LifecycleCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        StartCoroutine(StartDestroyProjectile(0.2f));
    }

    IEnumerator StartDestroyProjectile(float delay)
    {
        fadeEffect.StartFadeOut(0.2f, 0f);
        yield return new WaitForSeconds(delay);
        DestroyProjectile();
    }
}
