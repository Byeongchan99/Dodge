using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MortarBomb : BaseProjectile
{
    [SerializeField] protected AnimationCurve heightCurve;  // ���� ��ȭ�� ���� �ִϸ��̼� Ŀ��
    [SerializeField] protected float flightDuration;  // ��ü ���� �ð�
    [SerializeField] protected float hoverHeight = 5f;    // �ִ� ����

    /// <summary> �ڰ���ź ���� �������� </summary>
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
        flightDuration = _speed;
    }

    /// <summary> �ڰ���ź ������ ���� </summary>
    protected override void Move()
    {
        StartCoroutine(Flight());
    }

    /// <summary> �ڰ���ź ������ �ڷ�ƾ </summary>
    protected virtual IEnumerator Flight()
    {
        float time = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + (Vector3)moveDirection;

        // �ִϸ��̼� Ŀ�긦 �̿��� ������ ����
        while (time < flightDuration)
        {
            time += Time.deltaTime;
            float linearT = time / flightDuration;
            float heightT = heightCurve.Evaluate(linearT); // �ִϸ��̼� Ŀ�� �� ����

            float height = Mathf.Lerp(0.0f, hoverHeight, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height); // ��ġ ����

            yield return null;
        }
        DestroyProjectile();
    }
}
