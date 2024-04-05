using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // 분신 프리팹
    private GameObject playerClone; // 게임에 생성된 분신 객체
    public float blinkDistance = 5.0f; // 플레이어가 점멸할 수 있는 거리
    public float slowDownFactor = 0.05f; // 시간을 느리게 하는 요소
    private bool isBlinking = false;

    public void Execute(PlayerStat stat)
    {
        if (!isBlinking)
        {
            // 시간을 느리게 한다
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            // 시각화 로직 (임시: 점멸 범위를 표시하는 원을 기즈모로 그리기)
            ShowBlinkRange(stat.currentPosition.position);

            // 플레이어가 입력한 위치를 받아서 저장한다
            StartCoroutine(BlinkRoutine(stat));
        }
    }

    // 코루틴으로 능력 유지 시간동안 플레이어 입력을 받는다
    // 코루틴이 종료되면 플레이어가 입력한 위치를 반환한다
    private IEnumerator BlinkRoutine(PlayerStat stat)
    {
        isBlinking = true;

        // 본체 비활성화
        // PlayerMovement 스크립트 비활성화
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.rb.velocity = Vector2.zero;
            playerMovement.enabled = false;
        }

        // 분신 활성화
        if (playerClone == null)
        {
            playerClone = Instantiate(playerClonePrefab, stat.currentPosition.position, Quaternion.identity);
        }
        playerClone.SetActive(true);

        // 시간을 느리게 한다
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        Vector3 startPosition = stat.currentPosition.position;
        Vector3 nextPosition = startPosition;
        float elapsedTime = 0f;
        float maxDuration = 0.5f; // 특수 능력 최대 지속 시간 (예시)

        // 특수 능력 지속 시간동안 분신 조종
        while (elapsedTime < maxDuration)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                nextPosition += Vector3.up * blinkDistance * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                nextPosition += Vector3.down * blinkDistance * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                nextPosition += Vector3.left * blinkDistance * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                nextPosition += Vector3.right * blinkDistance * Time.unscaledDeltaTime;
            }

            // 분신 위치 업데이트
            playerClone.transform.position = nextPosition;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // 특수 능력 종료 후 본체 위치를 분신 위치로 업데이트
        stat.currentPosition.position = playerClone.transform.position;

        // 분신 비활성화
        playerClone.SetActive(false);

        // 본체 활성화
        // 분신 활성화가 끝난 후 PlayerMovement 스크립트 다시 활성화
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // 시간 정상화
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        // 점멸 상태 해제
        isBlinking = false;

        // 블링크 종료 
        EndBlink();
    }

    // 블링크 종료
    private void EndBlink()
    {
        // 시간을 정상으로 되돌린다
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        // 점멸 범위 표시를 제거한다
        HideBlinkRange();

        // 점멸 상태를 해제한다
        isBlinking = false;
    }

    // 블링크 범위 표시
    private void ShowBlinkRange(Vector3 position)
    {
        // 점멸 범위를 시각적으로 표시하는 코드
    }

    // 블링크 범위 표시 제거
    private void HideBlinkRange()
    {
        // 시각적으로 표시된 점멸 범위를 제거하는 코드
    }
}
