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
                HUDManager.UpdateTimerUI(score); // HUDManager�� ���� UI ������Ʈ
            }
            yield return null;
        }
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> Ÿ�̸� ���� </summary>
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

    /// <summary> ���� �߰� </summary>
    public void AddScore(float value)
    {
        score += value;
    }

    /// <summary> ���� ���� ��ȯ </summary>
    public float GetCurrentScore()
    {
        return score;
    }

    /// <summary> Ÿ�̸� �Ͻ����� </summary>
    public void PauseTimer()
    {
        isPaused = true;
    }

    /// <summary> Ÿ�̸� �簳 </summary>
    public void ResumeTimer()
    {
        isPaused = false;
    }
}
