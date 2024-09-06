using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour, IHealthObserver
{
    /****************************************************************************
                                protected Fields
    ****************************************************************************/
    // ������ �� �̺�Ʈ
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    // UI
    [SerializeField] private FullscreenUIManager fullscreenUIManager;
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private GameObject HUDContainer;
    [SerializeField] private StageResultUI stageResultUI;
    // �������� ������
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private StageData currentStageData; // ���� �������� ������
    // ���� ������
    [SerializeField] private UserDataManager userDataManager;

    /****************************************************************************
                                    public Fields
    ****************************************************************************/
    public Eraser eraser; // ���� ���� �� �ʱ�ȭ

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    void Start()
    {
        PlayerStat.Instance.RegisterObserver(this);
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    // ���� �������� ������ ��ȯ
    public StageData GetCurrentStageData()
    {
        return currentStageData;
    }

    // ���� �������� ������ ����
    public void SetStageData(int stageID)
    {
        currentStageData = stageDataList[stageID];
    }

    public void OnHealthChanged(float health)
    {
        if (health <= 0)
        {      
            ExitStage();
        }
    }

    // �ۻ�� �޼ҵ�: ��� �ʱ�ȭ �۾��� ����
    // �������� ����
    public void StartStage()
    {
        // ��� ������Ʈ �ʱ�ȭ
        eraser.EraseAll();
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
        HUDContainer.SetActive(true);
        //HUDManager.EnableTimer();
        //HUDManager.EnableHealthBar();
        fullscreenUIContainer.SetActive(false);

        // Ÿ�̸� ����
        ScoreManager.Instance.StartTimer();
    }

    // �������� �����
    public void RestartStage()
    {
        // �������� ����
        PlayerStat.Instance.DisablePlayer();
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();

        // �������� �����
        StartStage();
    }

    // �������� ����
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

        // ��� ������Ʈ �ʱ�ȭ
        eraser.EraseAll();
        // �������� ����
        GameManager.Instance.isPlayingStage = false;
        PlayerStat.Instance.DisablePlayer();
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        // HUD ��Ȱ��ȭ
        HUDContainer.SetActive(false);
        //HUDManager.DisableTimer();
        //HUDManager.DisableHealthBar();
        // �������� ���â Ȱ��ȭ
        stageResultUI.UpdateUIInfo(currentStageData, userDataManager.userData);
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
