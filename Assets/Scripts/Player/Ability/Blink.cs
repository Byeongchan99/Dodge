using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // 분신 프리팹
    private GameObject playerClone; // 게임에 생성된 분신 객체

    [SerializeField] private float _blinkMoveSpeed = 5.0f; // 블링크 지속 시간 동안 이동 속도
    [SerializeField] private float _blinkDuration = 0.5f; // 점멸 지속 시간
    [SerializeField] private float _cooldownTime = 3f; // 쿨타임 5초
    private float _nextAbilityTime = 0f; // 다음 능력 사용 가능 시간

    private bool isBlinking = false;

    public void Execute()
    {
        if (!isBlinking && Time.unscaledTime >= _nextAbilityTime)
        {
            Debug.Log("블링크 실행");
            StartCoroutine(BlinkRoutine());
            Debug.Log("블링크 종료");
            _nextAbilityTime = Time.unscaledTime + _cooldownTime; // 다음 사용 가능 시간 업데이트
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
        GameManager.Instance.StartSlowEffect(_blinkDuration);
        GameManager.Instance.isAbilitySlowMotion = true;

        // 위치 설정
        Vector3 startPosition = PlayerStat.Instance.currentPosition.position;
        Vector3 nextPosition = startPosition;

        float elapsedTime = 0f;
        // 특수 능력 지속 시간동안 분신 조종
        while (elapsedTime < _blinkDuration)
        {
            // 입력에 따른 위치 계산
            float horizontal = Input.GetAxisRaw("Horizontal") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            float vertical = Input.GetAxisRaw("Vertical") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            Vector3 moveDirection = new Vector3(horizontal, vertical, 0);
            nextPosition += moveDirection;

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

        // 점멸 상태 해제
        isBlinking = false;
        GameManager.Instance.isAbilitySlowMotion = false;
    }
}
