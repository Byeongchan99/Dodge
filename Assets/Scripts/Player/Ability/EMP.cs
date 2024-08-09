using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour, IPlayerAbility
{
    private BaseEffect EMPEffect; // EMP 효과

    [SerializeField] private float _slowDuration = 0.05f; // 슬로우 지속 시간
    [SerializeField] private float _EMPDuration = 0.5f; // EMP 지속 시간
    [SerializeField] private float _cooldownTime = 4f; // 쿨타임 5초
    public float CooldownTime // 쿨타임 프로퍼티
    {
        get { return _cooldownTime; }
    }
    private float _nextAbilityTime = 0f; // 다음 능력 사용 가능 시간

    private bool isEMP = false;

    public void Execute()
    {
        if (!isEMP && Time.unscaledTime >= _nextAbilityTime)
        {
            StartCoroutine(EMPRoutine());
            _nextAbilityTime = Time.unscaledTime + _cooldownTime + _EMPDuration; // 다음 사용 가능 시간 업데이트
        }
    }

    private IEnumerator EMPRoutine()
    {
        isEMP = true;

        // 움직임 멈춤
        // PlayerMovement 스크립트 비활성화
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.rb.velocity = Vector2.zero;
            playerMovement.enabled = false;
        }
        else
        {
            Debug.LogWarning("PlayerMovement 스크립트를 찾을 수 없습니다.");
        }

        // EMP 활성화
        EMPEffect = EffectPoolManager.Instance.Get("EMPEffect");
        EMPEffect.transform.position = PlayerStat.Instance.currentPosition.position;
        EMPEffect.gameObject.SetActive(true);

        // 시간을 느리게 한다
        GameManager.Instance.StartSlowEffect(_slowDuration); // 슬로우 모션 효과 적용
        GameManager.Instance.isAbilitySlowMotion = true;

        // EMP 지속시간 동안 대기
        yield return new WaitForSeconds(_EMPDuration - _slowDuration);

        // EMP 비활성화
        if (EMPEffect != null)
        {
            EffectPoolManager.Instance.Return("EMPEffect", EMPEffect);  // 캐시된 효과 인스턴스를 풀로 반환
            EMPEffect = null;  // 참조 해제
        }

        // PlayerMovement 스크립트 활성화
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isEMP = false;
        PlayerStat.Instance.abilityCooldownUI.StartCooldown(); // 쿨타임 적용

        yield return null;
    }
}
