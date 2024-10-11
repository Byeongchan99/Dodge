using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MortarBomb : BaseProjectile
{
    [SerializeField] protected AnimationCurve heightCurve; // 높이 변화를 위한 애니메이션 커브
    [SerializeField] protected float _flightDuration; // 전체 비행 시간
    [SerializeField] protected float _hoverHeight = 5f; // 최대 높이

    protected MortarBombEffect bombEffect;

    /// <summary> 박격포탄 스탯 가져오기 </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
        _flightDuration = _speed;
    }

    /// <summary> 박격포탄 움직임 구현 </summary>
    protected override void Move()
    {
        StartCoroutine(Flight());
    }

    public void SetBombEffect(MortarBombEffect effect)
    {
        bombEffect = effect;
    }

    /// <summary> 박격포탄 움직임 코루틴 </summary>
    protected virtual IEnumerator Flight()
    {
        float time = 0.0f;
        Vector3 start = transform.position; // 시작 위치
        Vector3 end = transform.position + (Vector3)moveDirection; // 도착 위치

        // 애니메이션 커브를 이용해 움직임 구현
        while (time < _flightDuration)
        {
            time += Time.deltaTime;
            float linearT = time / _flightDuration;
            float heightT = heightCurve.Evaluate(linearT); // 애니메이션 커브 값 리턴

            float height = Mathf.Lerp(0.0f, _hoverHeight, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height); // 위치 설정

            yield return null;
        }
        DestroyProjectile();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // EMP와 충돌했을 때
        if (collision.gameObject.CompareTag("EMP"))
        {
            Debug.Log("EMP와 충돌");
            DestroyProjectile();
            bombEffect.DestroyByEMP();
        }
    }
}
