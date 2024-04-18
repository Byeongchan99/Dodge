using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MortarBomb : BaseProjectile
{
    [SerializeField] private AnimationCurve heightCurve;  // ���� ��ȭ�� ���� �ִϸ��̼� Ŀ��
    [SerializeField] private float flightDuration = 3.0f;  // ��ü ���� �ð�
    [SerializeField] private float hoverHeight = 15f;    // �ִ� ����

    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
    }

    protected override void Move()
    {
        StartCoroutine(IEFlight());
    }

    private IEnumerator IEFlight()
    {
        float time = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + (Vector3)moveDirection;

        while (time < flightDuration)
        {
            time += Time.deltaTime;
            float linearT = time / flightDuration;
            float heightT = heightCurve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, hoverHeight, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height);

            yield return null;
        }
        DestroyProjectile();
    }
}
