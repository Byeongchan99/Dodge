using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour, IPlayerAbility
{
    public GameObject EMPPrefab; // EMP 프리팹
    private GameObject EMPClone; // 게임에 생성된 EMP 객체

    [SerializeField] private float _slowDownFactor = 0.05f; // 시간을 느리게 하는 요소
    [SerializeField] private float _slowDuration = 0.05f; // 슬로우 지속 시간
    [SerializeField] private float _EMPDuration = 0.5f; // EMP 지속 시간
    [SerializeField] private float cooldownTime = 4f; // 쿨타임 5초
    private float _nextAbilityTime = 0f; // 다음 능력 사용 가능 시간

    private bool isEMP = false;

    public void Execute()
    {
        if (!isEMP && Time.time >= _nextAbilityTime)
        {
            StartCoroutine(EMPRoutine());
            _nextAbilityTime = Time.time + cooldownTime; // 다음 사용 가능 시간 업데이트
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

        // EMP 활성화
        if (EMPClone == null)
        {
            EMPClone = Instantiate(EMPPrefab, PlayerStat.Instance.currentPosition.position, Quaternion.identity);
        }
        else
        {
            EMPClone.transform.position = PlayerStat.Instance.currentPosition.position; // EMPClone이 이미 있으면 위치만 업데이트
        }
        EMPClone.SetActive(true);

        // 시간을 느리게 한다
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // _slowDuration 동안 대기
        yield return new WaitForSecondsRealtime(_slowDuration);

        // 시간 흐름을 원래대로 복구
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // EMP 지속시간 동안 대기
        yield return new WaitForSeconds(_EMPDuration - _slowDuration);

        // EMP 비활성화
        EMPClone.SetActive(false);

        // PlayerMovement 스크립트 활성화
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isEMP = false;

        yield return null;
    }
}
