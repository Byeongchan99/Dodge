using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseProtocol : MonoBehaviour, IPlayerAbility
{
    public AudioClip defenseProtocolSound; // 방어 프로토콜 효과음

    [SerializeField] private float _defenseProtocolDuration = 2f; // 방어 프로토콜 지속 시간
    [SerializeField] private float _cooldownTime = 5f; // 쿨타임 5초                                               
    public float CooldownTime // 쿨타임 프로퍼티
    {
        get { return _cooldownTime; }
    }
    private float _nextAbilityTime = 0f; // 다음 능력 사용 가능 시간
    private bool _isDefense = false;

    PlayerMovement playerMovement;
    Animator animator;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void Execute()
    {
        if (!_isDefense && Time.unscaledTime >= _nextAbilityTime)
        {
            StartCoroutine(DefenseProtocolRoutine());
            _nextAbilityTime = Time.unscaledTime + _cooldownTime + _defenseProtocolDuration; // 다음 사용 가능 시간 업데이트
        }
    }

    private IEnumerator DefenseProtocolRoutine()
    {
        _isDefense = true;

        // 사운드 재생
        AudioManager.instance.sfxAudioSource.PlayOneShot(defenseProtocolSound);

        // 움직임 멈춤
        if (playerMovement != null)
        {
            playerMovement.ChangeVelocity(Vector2.zero); // 플레이어 멈추기
            // X와 Y축의 위치 고정
            playerMovement.FreezePosition();
            playerMovement.enabled = false;
        }

        // 무적 효과 활성화
        PlayerStat.Instance.StartInvincibility(_defenseProtocolDuration + 0.1f);

        // 방어 프로토콜 애니메이션 실행
        animator.SetBool("IsAbility", true);

        yield return new WaitForSecondsRealtime(_defenseProtocolDuration);

        // 방어 프로토콜 비활성화
        // 방어 프로토콜 애니메이션 중지
        animator.SetBool("IsAbility", false);

        // 움직임 재개
        // PlayerMovement 스크립트 활성화
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            playerMovement.UnFreezePosition();
        }

        _isDefense = false;
        PlayerStat.Instance.abilityCooldownUI.StartCooldown(); // 쿨타임 적용

        yield return null;
    }
}