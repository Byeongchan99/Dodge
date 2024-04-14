using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Coroutine slowMotionRoutine = null;
    private float remainingSlowDuration = 0f;  // 남은 슬로우 모션 효과 시간
    private float originalFixedDeltaTime;

    public bool isAbilitySlowMotion; // 특수 능력으로 인한 슬로우 모션 여부
    public bool isItemSlowMotion; // 아이템으로 인한 슬로우 모션 여부
    public int slowMotionItemCount; // 사용한 슬로 모션 아이템 개수

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 설정
            Init();
        }
        else
        {
            Destroy(gameObject);  // 중복 인스턴스 제거
        }        
    }

    void Init()
    {
        // 게임 초기화
        originalFixedDeltaTime = Time.fixedDeltaTime;  // 초기 FixedDeltaTime 저장
        isAbilitySlowMotion = false;
        isItemSlowMotion = false;
        slowMotionItemCount = 0;
    }

    public void StartSlowEffect(float duration)
    {
        if (slowMotionRoutine != null && remainingSlowDuration > duration)
        {
            Debug.Log("현재 슬로우 효과가 남아있는 시간이 더 길므로 새 요청 무시");
            return;  // 현재 남아있는 슬로우 모션이 더 길면 새 요청 무시
        }

        if (slowMotionRoutine != null)
        {
            StopCoroutine(slowMotionRoutine);  // 이전 슬로우 모션 코루틴 중지
        }

        slowMotionRoutine = StartCoroutine(ApplySlowMotion(duration));
    }

    private IEnumerator ApplySlowMotion(float duration)
    {
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;

        remainingSlowDuration = duration;  // 슬로우 모션 지속 시간 업데이트
        while (remainingSlowDuration > 0)
        {
            yield return null;
            remainingSlowDuration -= Time.unscaledDeltaTime;  // 실제 시간 감소로 업데이트
        }

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = originalFixedDeltaTime;  // 원래대로 복구

        slowMotionRoutine = null;
        remainingSlowDuration = 0f;  // 남은 시간 초기화
    }
}
