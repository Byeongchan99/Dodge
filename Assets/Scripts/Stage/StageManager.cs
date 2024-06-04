using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageManager : MonoBehaviour, IHealthObserver
{
    // ������ �� �̺�Ʈ
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    // UI
    [SerializeField] private FullscreenUIManager fullscreenUIManager;
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private StageResultUI stageResultUI;
    // �������� ������
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private StageData currentStageData; // ���� �������� ������

    public UserData userData; // ���� ������(�ӽ�)

    private float timer = 0f;
    private bool isPaused = false;
    private Coroutine timerCoroutine;

    void Start()
    {
        PlayerStat.Instance.RegisterObserver(this);
    }

    public void SetStageData(int stageID)
    {
        currentStageData = stageDataList[stageID];
    }

    // �ۻ�� �޼ҵ�: ��� �ʱ�ȭ �۾��� ����
    public void StartStage()
    {
        // �� ������ �ν��Ͻ�ȭ
        if (currentStageData != null && currentStageData.mapPrefab != null)
        {
            Instantiate(currentStageData.mapPrefab);
        }
        else
        {
            Debug.LogError("Stage data or map prefab is not set.");
            return;
        }

        // �������� ����
        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        turretUpgradeHandler.StartRandomUpgrades(10);
        // UI
        HUDManager.EnableTimer();
        HUDManager.EnableHealthBar();
        fullscreenUIContainer.SetActive(false);

        // Ÿ�̸� ����
        StartTimer();
    }

    public void OnHealthChanged(float health)
    {
        if (health <= 0)
        {
            ExitStage();
        }
    }

    public void ExitStage()
    {
        // ���� ������ ����
        if (userData != null && currentStageData.stageID >= 0 && currentStageData.stageID < userData.stageInfos.Length)
        {
            if (timer > 90f) 
                userData.stageInfos[currentStageData.stageID].isCleared = true;

            // ���� ����
            if (userData.stageInfos[currentStageData.stageID].score < Mathf.FloorToInt(timer)) 
                userData.stageInfos[currentStageData.stageID].score = Mathf.FloorToInt(timer);

            SaveUserData();
        }

        // �������� ����
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        // HUD ��Ȱ��ȭ
        HUDManager.DisableTimer();
        HUDManager.DisableHealthBar();
        // �������� ���â Ȱ��ȭ
        stageResultUI.UpdateStageResult(currentStageData, userData);
        fullscreenUIManager.OnPushFullscreenUI("Stage Result");
        fullscreenUIContainer.SetActive(true);
    }

    private void SaveUserData()
    {
#if UNITY_EDITOR
        // ������ ��忡�� ScriptableObject �����͸� ����
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    private void StartTimer()
    {
        timer = 0f;
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
                timer += Time.unscaledDeltaTime;
                HUDManager.UpdateTimerUI(timer); // HUDManager�� ���� UI ������Ʈ
            }
            yield return null;
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

    /*
    public void ExitStage()
    {
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        HUDManager.PauseTimer();
    }
    */
}
