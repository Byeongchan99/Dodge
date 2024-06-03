using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ������ �� �̺�Ʈ
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    // UI
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    // �������� ������
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private StageData currentStageData; // ���� �������� ������
    private GameObject currentMap;

    public UserData userData; // ���� ������(�ӽ�)

    private float timer = 0f;
    private bool isPaused = false;
    private Coroutine timerCoroutine;

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
            currentMap = Instantiate(currentStageData.mapPrefab);
        }
        else
        {
            Debug.LogError("Stage data or map prefab is not set.");
            return;
        }

        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        HUDManager.EnableTimer();
        HUDManager.EnableHealthBar();
        fullscreenUIContainer.SetActive(false);
        turretUpgradeHandler.StartRandomUpgrades(10); // �Ķ���Ͱ� �ִ� ��쵵 ���

        // Ÿ�̸� ����
        StartTimer();
    }

    public void ClearStage()
    {
        // ���� ������ ����
        if (userData != null && currentStageData.stageID >= 0 && currentStageData.stageID < userData.stageInfos.Length)
        {
            if (timer > 90f) 
                userData.stageInfos[currentStageData.stageID].isCleared = true;
            userData.stageInfos[currentStageData.stageID].score = Mathf.FloorToInt(timer);
            SaveUserData();
        }

        // �������� ����â���� �̵�
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        HUDManager.DisableTimer();
        HUDManager.DisableHealthBar();
        fullscreenUIContainer.SetActive(true);
        turretUpgradeHandler.StopRandomUpgrades();
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
