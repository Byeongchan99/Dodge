using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MortarBomb : BaseProjectile
{
    [SerializeField] private AnimationCurve heightCurve;  // ���� ��ȭ�� ���� �ִϸ��̼� Ŀ��
    [SerializeField] private float flightDuration;  // ��ü ���� �ð�
    [SerializeField] private float hoverHeight = 15f;    // �ִ� ����

    /// <summary> �ڰ���ź ���� �������� </summary>
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
        flightDuration = _speed; // ��ü ���� �ð� ����
    }

    /// <summary> �ڰ���ź ������ ���� </summary>
    protected override void Move()
    {
        StartCoroutine(IEFlight());
    }

    /// <summary> �ڰ���ź ������ �ڷ�ƾ </summary>
    private IEnumerator IEFlight()
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
