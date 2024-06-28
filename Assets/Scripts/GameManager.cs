using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool _isPlayingStage; // �������� �÷��� �� ����
    private Coroutine slowMotionRoutine = null;
    private float _remainingSlowDuration = 0f;  // ���� ���ο� ��� ȿ�� �ð�
    private float _originalFixedDeltaTime;
    [SerializeField] private float _slowDownFactor = 0.05f; // �ð��� ������ �ϴ� ���

    public bool isAbilitySlowMotion; // Ư�� �ɷ����� ���� ���ο� ��� ����
    public bool isItemSlowMotion; // ���������� ���� ���ο� ��� ����
    public int slowMotionItemCount; // ����� ���� ��� ������ ����

    public bool isPlayingStage
    {
        get { return _isPlayingStage; }
        set { _isPlayingStage = value; }
    }

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
}
