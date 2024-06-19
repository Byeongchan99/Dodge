using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private HUDManager HUDManager;

    private float score = 0f;
    private Coroutine timerCoroutine;
    private bool isPaused = false;

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

    public void AddScore(float value)
    {
        score += value;
    }

    public float GetCurrentScore()
    {
        return score;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }
}
