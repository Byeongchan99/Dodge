using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    private bool _isPlayingStage; // 스테이지 플레이 중 여부
    private bool _isPaused; // 일시 정지 여부
    private Coroutine slowMotionRoutine = null;
    [SerializeField] private float _remainingSlowDuration = 0f;  // 남은 슬로우 모션 효과 시간
    private float _originalFixedDeltaTime;
    [SerializeField] private float _slowDownFactor = 0.05f; // 시간을 느리게 하는 요소

    /****************************************************************************
                                   public Fields
    ****************************************************************************/
    public static GameManager Instance { get; private set; }

    public bool isAbilitySlowMotion; // 특수 능력으로 인한 슬로우 모션 여부
    public bool isItemSlowMotion; // 아이템으로 인한 슬로우 모션 여부
    public int slowMotionItemCount; // 사용한 슬로 모션 아이템 개수

    public bool isPlayingStage
    {
        get { return _isPlayingStage; }
        set { _isPlayingStage = value; }
    }

    public bool isPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
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

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    void Init()
    {
        // 게임 초기화
        _originalFixedDeltaTime = Time.fixedDeltaTime;  // 초기 FixedDeltaTime 저장
        isAbilitySlowMotion = false;
        isItemSlowMotion = false;
        slowMotionItemCount = 0;
    }

    /// <summary> 슬로우 모션 적용 코루틴 </summary>
    private IEnumerator ApplySlowMotion(float duration)
    {
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = _originalFixedDeltaTime * Time.timeScale;

        _remainingSlowDuration = duration;  // 슬로우 모션 지속 시간 업데이트
        while (_remainingSlowDuration > 0)
        {
            yield return null;
            if (!_isPaused) // 일시 정지 상태가 아닐 때
            {
                _remainingSlowDuration -= Time.unscaledDeltaTime;  // 실제 시간 감소로 업데이트
            }
        }
    
        StopSlowEffect();  // 슬로우 모션 효과 종료

        /*
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = _originalFixedDeltaTime;  // 원래대로 복구

        slowMotionRoutine = null;
        _remainingSlowDuration = 0f;  // 남은 시간 초기화
        */
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> 슬로우 모션 효과 시작 </summary>
    public void StartSlowEffect(float duration)
    {
        if (slowMotionRoutine != null && _remainingSlowDuration > duration)
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

    /// <summary> 슬로우 모션 효과 중지 </summary>
    public void StopSlowEffect()
    {
        Debug.Log("슬로우 모션 효과 종료");
        if (slowMotionRoutine != null)
        {
            StopCoroutine(slowMotionRoutine);
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = _originalFixedDeltaTime;  // 원래대로 복구
            slowMotionRoutine = null;
            _remainingSlowDuration = 0f;  // 남은 시간 초기화
        }
    }

    // 게임을 종료하는 메서드
    public void ExitGame()
    {
#if UNITY_EDITOR
        // 유니티 에디터 상에서는 에디터를 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서는 애플리케이션을 종료
        Application.Quit();
#endif
    }
}
