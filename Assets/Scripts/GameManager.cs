using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    private bool _isPlayingStage; // �������� �÷��� �� ����
    private bool _isPaused; // �Ͻ� ���� ����
    private Coroutine slowMotionRoutine = null;
    [SerializeField] private float _remainingSlowDuration = 0f;  // ���� ���ο� ��� ȿ�� �ð�
    private float _originalFixedDeltaTime;
    [SerializeField] private float _slowDownFactor = 0.05f; // �ð��� ������ �ϴ� ���

    /****************************************************************************
                                   public Fields
    ****************************************************************************/
    public static GameManager Instance { get; private set; }

    public bool isAbilitySlowMotion; // Ư�� �ɷ����� ���� ���ο� ��� ����
    public bool isItemSlowMotion; // ���������� ���� ���ο� ��� ����
    public int slowMotionItemCount; // ����� ���� ��� ������ ����

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
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ����
            Init();
        }
        else
        {
            Destroy(gameObject);  // �ߺ� �ν��Ͻ� ����
        }        
    }

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    void Init()
    {
        // ���� �ʱ�ȭ
        _originalFixedDeltaTime = Time.fixedDeltaTime;  // �ʱ� FixedDeltaTime ����
        isAbilitySlowMotion = false;
        isItemSlowMotion = false;
        slowMotionItemCount = 0;
    }

    /// <summary> ���ο� ��� ���� �ڷ�ƾ </summary>
    private IEnumerator ApplySlowMotion(float duration)
    {
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = _originalFixedDeltaTime * Time.timeScale;

        _remainingSlowDuration = duration;  // ���ο� ��� ���� �ð� ������Ʈ
        while (_remainingSlowDuration > 0)
        {
            yield return null;
            if (!_isPaused) // �Ͻ� ���� ���°� �ƴ� ��
            {
                _remainingSlowDuration -= Time.unscaledDeltaTime;  // ���� �ð� ���ҷ� ������Ʈ
            }
        }
    
        StopSlowEffect();  // ���ο� ��� ȿ�� ����

        /*
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = _originalFixedDeltaTime;  // ������� ����

        slowMotionRoutine = null;
        _remainingSlowDuration = 0f;  // ���� �ð� �ʱ�ȭ
        */
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> ���ο� ��� ȿ�� ���� </summary>
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

    /// <summary> ���ο� ��� ȿ�� ���� </summary>
    public void StopSlowEffect()
    {
        Debug.Log("���ο� ��� ȿ�� ����");
        if (slowMotionRoutine != null)
        {
            StopCoroutine(slowMotionRoutine);
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = _originalFixedDeltaTime;  // ������� ����
            slowMotionRoutine = null;
            _remainingSlowDuration = 0f;  // ���� �ð� �ʱ�ȭ
        }
    }

    // ������ �����ϴ� �޼���
    public void ExitGame()
    {
#if UNITY_EDITOR
        // ����Ƽ ������ �󿡼��� �����͸� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� ���ø����̼ǿ����� ���ø����̼��� ����
        Application.Quit();
#endif
    }
}
