using System.Diagnostics;
using TMPro;
using UnityEngine;

public interface IPlayerAbility
{
    void Execute(PlayerStat stat);
}

public class Blink : IPlayerAbility
{
    private float blinkDistance = 5.0f; // 플레이어가 점멸할 수 있는 거리
    private float slowDownFactor = 0.05f; // 시간을 느리게 하는 요소
    private bool isBlinking = false;

    public void Execute(PlayerStat stat)
    {
        if (!isBlinking)
        {
            // 시간을 느리게 한다
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            // 시각화 로직을 여기에 추가한다 (예: 점멸 범위를 표시하는 원을 그리기)
            ShowBlinkRange(stat.currentPoisition.position);

            // 플레이어 입력을 받는다 (Input.GetButtonDown 등을 사용)
            Vector3 direction = Vector3.zero;

            // 플레이어가 입력한 위치를 받아서 저장한다
            Vector3 targetPosition = GetBlinkTargetPosition(stat.currentPoisition.position, direction);

            // 점멸 실행
            DoBlink(stat, targetPosition);
        }
    }

    private Vector3 GetBlinkTargetPosition(Vector3 currentPosition, Vector3 direction)
    {
        // 입력을 기반으로 새 위치를 계산
        Vector3 targetPosition = currentPosition + direction.normalized * blinkDistance;


        // 범위를 확인하여 최대 점멸 거리를 초과하지 않는지 확인한다
        return Vector3.ClampMagnitude(targetPosition - currentPosition, blinkDistance) + currentPosition;
    }

    private void DoBlink(PlayerStat stat, Vector3 targetPosition)
    {
        // 시간을 정상으로 되돌린다
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        // 플레이어의 위치를 업데이트한다
        stat.currentPoisition.position = targetPosition;

        // 점멸 범위 표시를 제거한다
        HideBlinkRange();

        // 점멸 상태를 해제한다
        isBlinking = false;
    }

    private void ShowBlinkRange(Vector3 position)
    {
        // 점멸 범위를 시각적으로 표시하는 코드
    }

    private void HideBlinkRange()
    {
        // 시각적으로 표시된 점멸 범위를 제거하는 코드
    }
}

public class EMP : IPlayerAbility
{
    public void Execute(PlayerStat stat)
    {
        // EMP 구현
        // 근처의 투사체 무력화
    }
}

public class DefenseProtocol: IPlayerAbility
{
    public void Execute(PlayerStat stat)
    {
        // 방어 프로토콜 구현
        // 일정 시간 동안 무적 및 행동 불가
    }
}