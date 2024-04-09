using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // 분신 프리팹
    private GameObject playerClone; // 게임에 생성된 분신 객체

    [SerializeField] private float _blinkMoveSpeed = 5.0f; // 블링크 지속 시간 동안 이동 속도
    [SerializeField] private float _slowDownFactor = 0.05f; // 시간을 느리게 하는 요소
    [SerializeField] private float _blinkDuration = 0.5f; // 점멸 지속 시간
    [SerializeField] private float _cooldownTime = 5f; // 쿨타임 5초
    private float _nextAbilityTime = 0f; // 다음 능력 사용 가능 시간

    private bool isBlinking = false;

    public void Execute()
    {
        if (!isBlinking && Time.time >= _nextAbilityTime)
        {
            StartCoroutine(BlinkRoutine());
            _nextAbilityTime = Time.time + _cooldownTime; // 다음 사용 가능 시간 업데이트
        }
    }

    // 능력 유지 시간동안 분신 조종
    private IEnumerator BlinkRoutine()
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
            playerClone = Instantiate(playerClonePrefab, PlayerStat.Instance.currentPosition.position, Quaternion.identity);
        }
        playerClone.SetActive(true);

        // 시간을 느리게 한다
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // 위치 설정
        Vector3 startPosition = PlayerStat.Instance.currentPosition.position;
        Vector3 nextPosition = startPosition;

        float elapsedTime = 0f;
        // 특수 능력 지속 시간동안 분신 조종
        while (elapsedTime < _blinkDuration)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                nextPosition += Vector3.up * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                nextPosition += Vector3.down * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                nextPosition += Vector3.left * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                nextPosition += Vector3.right * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }

            // 분신 위치 업데이트
            playerClone.transform.position = nextPosition;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // 특수 능력 종료 후 본체 위치를 분신 위치로 업데이트
        PlayerStat.Instance.currentPosition.position = playerClone.transform.position;

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
    }
}
