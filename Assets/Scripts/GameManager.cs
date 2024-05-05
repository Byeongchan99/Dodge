using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Coroutine slowMotionRoutine = null;
    private float _remainingSlowDuration = 0f;  // ���� ���ο� ��� ȿ�� �ð�
    private float _originalFixedDeltaTime;
    [SerializeField] private float _slowDownFactor = 0.05f; // �ð��� ������ �ϴ� ���

    public bool isAbilitySlowMotion; // Ư�� �ɷ����� ���� ���ο� ��� ����
    public bool isItemSlowMotion; // ���������� ���� ���ο� ��� ����
    public int slowMotionItemCount; // ����� ���� ��� ������ ����

    [SerializeField] Text timeText; // �ӽ� �ð� ǥ��
    [SerializeField] Text playerHealth; // �ӽ� �÷��̾� ü�� ǥ��

    private IEnumerator timerCoroutine;
    private float timer = 0f;
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ����
            Init();
        }
        else
        {
            Destroy(gameObject);  // �ߺ� �ν��Ͻ� ����
        }        
    }

    void Init()
    {
        // ���� �ʱ�ȭ
        _originalFixedDeltaTime = Time.fixedDeltaTime;  // �ʱ� FixedDeltaTime ����
        isAbilitySlowMotion = false;
        isItemSlowMotion = false;
        slowMotionItemCount = 0;
    }

    // �ӽ� �ð� ǥ��
    private void Update()
    {
        playerHealth.text = "Player HP: " + PlayerStat.Instance.currentHealth.ToString();
    }

    public void StartSlowEffect(float duration)
    {
        if (slowMotionRoutine != null && _remainingSlowDuration > duration)
        {
            Debug.Log("���� ���ο� ȿ���� �����ִ� �ð��� �� ��Ƿ� �� ��û ����");
            return;  // ���� �����ִ� ���ο� ����� �� ��� �� ��û ����
        }

        if (slowMotionRoutine != null)
        {
            StopCoroutine(slowMotionRoutine);  // ���� ���ο� ��� �ڷ�ƾ ����
        }

        slowMotionRoutine = StartCoroutine(ApplySlowMotion(duration));
    }

    private IEnumerator ApplySlowMotion(float duration)
    {
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = _originalFixedDeltaTime * Time.timeScale;

        _remainingSlowDuration = duration;  // ���ο� ��� ���� �ð� ������Ʈ
        while (_remainingSlowDuration > 0)
        {
            yield return null;
            _remainingSlowDuration -= Time.unscaledDeltaTime;  // ���� �ð� ���ҷ� ������Ʈ
        }

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = _originalFixedDeltaTime;  // ������� ����

        slowMotionRoutine = null;
        _remainingSlowDuration = 0f;  // ���� �ð� �ʱ�ȭ
    }

    public void StartTimer()
    {
        timerCoroutine = RunTimer();
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator RunTimer()
    {
        while (true)
        {
            if (!isPaused)
            {
                timer += Time.unscaledDeltaTime;
                timeText.text = "Time: " + timer.ToString();

            }
            yield return null;  // ���� �����ӱ��� ��ٸ�
        }
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
