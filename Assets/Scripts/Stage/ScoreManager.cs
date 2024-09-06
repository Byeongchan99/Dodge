using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    [SerializeField] private HUDManager HUDManager;

    private float score = 0f;
    private Coroutine timerCoroutine;
    private bool isPaused = false;

    /****************************************************************************
                                public Fields
    ****************************************************************************/
    public static ScoreManager Instance { get; private set; }

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /****************************************************************************
                                 private Methods
    ****************************************************************************/
    private IEnumerator RunTimer()
    {
        while (true)
        {
            if (!isPaused)
            {
                score += Time.unscaledDeltaTime;
                HUDManager.UpdateTimerUI(score); // HUDManager를 통해 UI 업데이트
            }
            yield return null;
        }
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> 타이머 시작 </summary>
    public void StartTimer()
    {
        score = 0f;
        isPaused = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(RunTimer());
    }

    /// <summary> 점수 추가 </summary>
    public void AddScore(float value)
    {
        score += value;
    }

    /// <summary> 현재 점수 반환 </summary>
    public float GetCurrentScore()
    {
        return score;
    }

    /// <summary> 타이머 일시정지 </summary>
    public void PauseTimer()
    {
        isPaused = true;
    }

    /// <summary> 타이머 재개 </summary>
    public void ResumeTimer()
    {
        isPaused = false;
    }
}
