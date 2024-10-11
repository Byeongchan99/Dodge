using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MortarBomb : BaseProjectile
{
    [SerializeField] protected AnimationCurve heightCurve; // ���� ��ȭ�� ���� �ִϸ��̼� Ŀ��
    [SerializeField] protected float _flightDuration; // ��ü ���� �ð�
    [SerializeField] protected float _hoverHeight = 5f; // �ִ� ����

    protected MortarBombEffect bombEffect;

    /// <summary> �ڰ���ź ���� �������� </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
        _flightDuration = _speed;
    }

    /// <summary> �ڰ���ź ������ ���� </summary>
    protected override void Move()
    {
        StartCoroutine(Flight());
    }

    public void SetBombEffect(MortarBombEffect effect)
    {
        bombEffect = effect;
    }

    /// <summary> �ڰ���ź ������ �ڷ�ƾ </summary>
    protected virtual IEnumerator Flight()
    {
        float time = 0.0f;
        Vector3 start = transform.position; // ���� ��ġ
        Vector3 end = transform.position + (Vector3)moveDirection; // ���� ��ġ

        // �ִϸ��̼� Ŀ�긦 �̿��� ������ ����
        while (time < _flightDuration)
        {
            time += Time.deltaTime;
            float linearT = time / _flightDuration;
            float heightT = heightCurve.Evaluate(linearT); // �ִϸ��̼� Ŀ�� �� ����

            float height = Mathf.Lerp(0.0f, _hoverHeight, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height); // ��ġ ����

            yield return null;
        }
        DestroyProjectile();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // EMP�� �浹���� ��
        if (collision.gameObject.CompareTag("EMP"))
        {
            Debug.Log("EMP�� �浹");
            DestroyProjectile();
            bombEffect.DestroyByEMP();
        }
    }
}
