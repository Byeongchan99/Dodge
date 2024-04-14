using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseProtocol : MonoBehaviour, IPlayerAbility
{
    [SerializeField] private float _DefenseProtocolDuration = 2f; // 방어 프로토콜 지속 시간
    [SerializeField] private float cooldownTime = 5f; // 쿨타임 5초
    private float _nextAbilityTime = 0f; // 다음 능력 사용 가능 시간

    private bool isDefense = false;

    public void Execute()
    {
        if (!isDefense && Time.unscaledTime >= _nextAbilityTime)
        {
            StartCoroutine(DefenseProtocolRoutine());
            _nextAbilityTime = Time.unscaledTime + cooldownTime; // 다음 사용 가능 시간 업데이트
        }
    }

    private IEnumerator DefenseProtocolRoutine()
    {
        isDefense = true;

        // 움직임 멈춤
        // PlayerMovement 스크립트 비활성화 및 Rigidbody2D 참조
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        Rigidbody2D rb = playerMovement != null ? playerMovement.rb : null;

        if (playerMovement != null)
        {
            playerMovement.rb.velocity = Vector2.zero;
            // X와 Y축의 위치 고정
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            playerMovement.enabled = false;
        }

        // 방어 프로토콜 활성화
        PlayerStat.Instance.isInvincibility = true;

        // 플레이어의 스프라이트를 노란색으로 변경
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        if (playerSprite != null)
        {
            playerSprite.color = Color.yellow;
        }

        yield return new WaitForSecondsRealtime(_DefenseProtocolDuration);

        // 방어 프로토콜 비활성화
        // 플레이어의 스프라이트를 원래 색으로 변경
        if (playerSprite != null)
        {
            playerSprite.color = Color.white; // 원래 색상으로 되돌림
        }

        // 움직임 재개
        // PlayerMovement 스크립트 활성화
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        yield return new WaitForSecondsRealtime(0.1f); // 약간의 무적 시간

        PlayerStat.Instance.isInvincibility = false;
        isDefense = false;

        yield return null;
    }
}
