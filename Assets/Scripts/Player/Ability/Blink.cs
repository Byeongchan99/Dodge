using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // 분신 프리팹
    public AudioClip blinkSound; // 블링크 효과음
    private GameObject playerClone; // 게임에 생성된 분신 객체
    private PlayerMovement playerMovement; // 플레이어 이동 스크립트
    private SpriteRenderer cloneSpriteRenderer; // 분신의 스프라이트 렌더러
    [SerializeField] private LayerMask wallLayerMask; // 벽 레이어 마스크

    [SerializeField] private float _blinkMoveSpeed = 5.0f; // 블링크 지속 시간 동안 이동 속도
    [SerializeField] private float _blinkDuration = 0.5f; // 점멸 지속 시간
    [SerializeField] private float _cooldownTime = 3f; // 쿨타임
    public float CooldownTime // 쿨타임 프로퍼티
    {
        get { return _cooldownTime; }
    }
    private float _nextAbilityTime = 0f; // 다음 능력 사용 가능 시간
    private bool _isBlinking = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Execute()
    {
        if (!_isBlinking && Time.unscaledTime >= _nextAbilityTime)
        {
            Debug.Log("블링크 실행");
            StartCoroutine(BlinkRoutine());
            Debug.Log("블링크 종료");
            _nextAbilityTime = Time.unscaledTime + _cooldownTime + _blinkDuration; // 다음 사용 가능 시간 업데이트
        }
    }

    // 능력 유지 시간동안 분신 조종하고 능력 종료 후 본체 위치를 분신 위치로 업데이트
    private IEnumerator BlinkRoutine()
    {
        _isBlinking = true;

        // 사운드 재생
        AudioManager.instance.sfxAudioSource.PlayOneShot(blinkSound);

        // 본체 비활성화
        // PlayerMovement 스크립트 비활성화
        if (playerMovement != null)
        {
            playerMovement.ChangeVelocity(Vector2.zero); // 플레이어 멈추기
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

        // 분신 시작 위치 설정
        Vector3 startPosition = PlayerStat.Instance.currentPosition.position;
        Vector3 nextPosition = startPosition;
        playerClone.transform.position = startPosition;

        float elapsedTime = 0f;
        // 특수 능력 지속 시간동안 분신 조종
        while (elapsedTime < _blinkDuration)
        {
            // 입력에 따른 위치 계산
            float horizontal = Input.GetAxisRaw("Horizontal") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            float vertical = Input.GetAxisRaw("Vertical") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            Vector3 moveDirection = new Vector3(horizontal, vertical, 0);

            if (cloneSpriteRenderer == null)
            {
                cloneSpriteRenderer = playerClone.GetComponent<SpriteRenderer>();
            }

            if (moveDirection.x != 0)
            {
                cloneSpriteRenderer.flipX = moveDirection.x < 0;
            }

            /*
            nextPosition += moveDirection;

            // 분신 위치 업데이트
            playerClone.transform.position = nextPosition;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
            */
            // 잠재적인 새로운 위치 계산
            Vector3 potentialPosition = nextPosition + moveDirection;

            // 잠재적인 위치에서의 충돌 검사
            if (!IsCollidingWithWall(nextPosition, moveDirection))
            {
                // 충돌이 없으면 위치 업데이트
                nextPosition = potentialPosition;
                playerClone.transform.position = nextPosition;
            }
            else
            {
                // 충돌이 있으면 해당 방향으로의 이동 중지
                moveDirection = Vector3.zero;
            }

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // 특수 능력 종료 후 본체 위치를 분신 위치로 업데이트 + 분신 위치도 업데이트
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
        _isBlinking = false;
        PlayerStat.Instance.abilityCooldownUI.StartCooldown(); // 쿨타임 적용
        GameManager.Instance.isAbilitySlowMotion = false;
        
        yield return null;
    }

    // 벽과의 충돌을 확인하는 메서드
    private bool IsCollidingWithWall(Vector3 currentPosition, Vector3 direction)
    {
        float distance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, distance, wallLayerMask);
        return hit.collider != null;
    }
}
