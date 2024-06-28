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
    // ���� ������
    [SerializeField] private UserDataManager userDataManager;

    public Eraser eraser; // ���� ���� �� �ʱ�ȭ

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
        GameManager.Instance.isPlayingStage = true;
        PlayerStat.Instance.SetCharacter();
        PlayerStat.Instance.player.SetActive(true);
        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        turretUpgradeHandler.StartRandomUpgrades(10);
        // UI
        HUDManager.EnableTimer();
        HUDManager.EnableHealthBar();
        fullscreenUIContainer.SetActive(false);

        // Ÿ�̸� ����
        ScoreManager.Instance.StartTimer();
    }

    public void OnHealthChanged(float health)
    {
        if (health <= 0)
        {
            eraser.EraseAll();
            ExitStage();
        }
    }

    public void ExitStage()
    {
        // ���� ������ ����
        if (userDataManager.userData != null && currentStageData.stageID >= 0 && currentStageData.stageID < userDataManager.userData.stageInfos.Length)
        {
            if (ScoreManager.Instance.GetCurrentScore() > 150f)
                userDataManager.userData.stageInfos[currentStageData.stageID].isCleared = true;

            // ���� ����
            if (userDataManager.userData.stageInfos[currentStageData.stageID].score < Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore()))
                userDataManager.userData.stageInfos[currentStageData.stageID].score = Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore());

            userDataManager.SaveUserData();
        }

        // �������� ����
        GameManager.Instance.isPlayingStage = false;
        PlayerStat.Instance.DisablePlayer();
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        // HUD ��Ȱ��ȭ
        HUDManager.DisableTimer();
        HUDManager.DisableHealthBar();
        // �������� ���â Ȱ��ȭ
        stageResultUI.UpdateStageResult(currentStageData, userDataManager.userData);
        fullscreenUIManager.OnPushFullscreenUI("Stage Result");
        fullscreenUIContainer.SetActive(true);
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
