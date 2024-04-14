using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Coroutine slowMotionRoutine = null;
    private float remainingSlowDuration = 0f;  // ���� ���ο� ��� ȿ�� �ð�
    private float originalFixedDeltaTime;

    public bool isAbilitySlowMotion; // Ư�� �ɷ����� ���� ���ο� ��� ����
    public bool isItemSlowMotion; // ���������� ���� ���ο� ��� ����
    public int slowMotionItemCount; // ����� ���� ��� ������ ����

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
        originalFixedDeltaTime = Time.fixedDeltaTime;  // �ʱ� FixedDeltaTime ����
        isAbilitySlowMotion = false;
        isItemSlowMotion = false;
        slowMotionItemCount = 0;
    }

    public void StartSlowEffect(float duration)
    {
        if (slowMotionRoutine != null && remainingSlowDuration > duration)
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
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;

        remainingSlowDuration = duration;  // ���ο� ��� ���� �ð� ������Ʈ
        while (remainingSlowDuration > 0)
        {
            yield return null;
            remainingSlowDuration -= Time.unscaledDeltaTime;  // ���� �ð� ���ҷ� ������Ʈ
        }

        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = originalFixedDeltaTime;  // ������� ����

        slowMotionRoutine = null;
        remainingSlowDuration = 0f;  // ���� �ð� �ʱ�ȭ
    }
}
