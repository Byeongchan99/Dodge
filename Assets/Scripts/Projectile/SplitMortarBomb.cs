using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitMortarBomb : MortarBomb
{
    [SerializeField] string _mortarBombPoolName;

    // 분열하는 4가지 방향
    Vector3[] directions = {
        new Vector3(1, 0, 0).normalized,
        new Vector3(-1, 0, 0).normalized,
        new Vector3(0, 1, 0).normalized,
        new Vector3(0, -1, 0).normalized
    };

    /// <summary> 분열 박격포탄 움직임 코루틴 </summary>
    protected override IEnumerator Flight()
    {
        float time = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + (Vector3)moveDirection;

        // 애니메이션 커브를 이용해 움직임 구현
        while (time < flightDuration)
        {
            time += Time.deltaTime;
            float linearT = time / flightDuration;
            float heightT = heightCurve.Evaluate(linearT); // 애니메이션 커브 값 리턴

            float height = Mathf.Lerp(0.0f, hoverHeight, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height); // 위치 설정

            yield return null;
        }
     
        // 분열된 박격포탄의 경우 크기가 더 작음
        Vector3 mortarBombSize = transform.localScale / 2;
        Vector3 effectSize = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize / 2;

        Debug.Log("분열 시작");
        for (int i = 0; i < 4; i++)
        {
            SplitedMortarBomb splitedMortarBomb = ProjectilePoolManager.Instance.Get(_mortarBombPoolName) as SplitedMortarBomb;
            SplitedMortarBombEffect splitedBombEffect = EffectPoolManager.Instance.Get("SplitedMortarBombEffect") as SplitedMortarBombEffect;

            if (splitedMortarBomb != null)
            {
                splitedMortarBomb.transform.position = transform.position; // 현재 위치 설정        
                splitedMortarBomb.transform.localScale = mortarBombSize; // 크기 설정
                splitedMortarBomb.setMortarBomb(flightDuration / 2, hoverHeight / 4); // 박격포탄 스탯 설정
                splitedMortarBomb.SetBombEffect(splitedBombEffect); // 이펙트 참조 설정
                
                splitedMortarBomb.gameObject.SetActive(true); // 활성화
                splitedMortarBomb.SetDirection(directions[i]); // 분열 방향 설정(박격포탄의 경우 Move() 메서드가 코루틴이라 활성화 후 실행)
            }
            else
            {
                Debug.LogWarning("Failed to get mortar bomb from pool.");
            }
           
            if (splitedBombEffect != null)
            {
                splitedBombEffect.transform.position = transform.position + directions[i]; // 위치 설정 (적당한 거리 조절)
                splitedBombEffect.transform.localScale = effectSize; // 크기 설정
                splitedBombEffect.gameObject.SetActive(true); // 활성화
            }
            else
            {
                Debug.LogWarning("Failed to get bomb effect from pool.");
            }
        }

        DestroyProjectile();
    }
}
