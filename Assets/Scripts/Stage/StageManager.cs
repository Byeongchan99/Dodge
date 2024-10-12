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
    [SerializeField] private PopupUIManager popupUIManager;
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private GameObject HUDContainer;
    [SerializeField] private StageResultUI stageResultUI;
    [SerializeField] private NotificationManager notificationManager;
    // ���� ������
    [SerializeField] private StatDataManager statDataManager;
    // �������� ������
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private StageData currentStageData; // ���� �������� ������
    // ���� ������
    [SerializeField] private UserDataManager userDataManager;
    // Ÿ�ϸ�
    [SerializeField] private GameObject[] tilemaps;

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

    public List<string> returnLeaderboardID()
    {
        List<string> stageLeaderboardIDList = new List<string>(); // �������� �������� ID ����Ʈ

        foreach (var stageData in stageDataList)
        {
            stageLeaderboardIDList.Add(stageData.leaderboardID);
        }

        return stageLeaderboardIDList;
    }

    // �÷��̾� ü���� ����� �� ȣ��
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
        // Ÿ�ϸ� Ȱ��ȭ
        if (currentStageData != null)
        {
            tilemaps[currentStageData.stageID].SetActive(true);
            tilemaps[5].SetActive(true);
        }
        else
        {
            Debug.LogError("Stage data or tilemap is not set.");
            return;
        }

        // �������� ����
        Time.timeScale = 1f;
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
        // ���� ���
        AudioManager.instance.PlayBGM(currentStageData.bgm);

        // Ÿ�̸� ����
        ScoreManager.Instance.StartTimer();
    }

    // �������� �����
    public void RestartStage()
    {
        // �������� ����
        eraser.EraseAll();
        statDataManager.SetOriginalStatData(currentStageData.statDataName); // ���� �ʱ�ȭ
        PlayerStat.Instance.DisablePlayer();
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        AudioManager.instance.StopBGM();

        // UI
        popupUIManager.ClosePausePopup(); // �Ͻ����� â �ݱ�
        HUDContainer.SetActive(false);
        notificationManager.ClearNotifications();

        // �������� �����
        StartStage();
    }

    // �������� ����
    public void ExitStage()
    {
        // ���� ������ ����
        if (userDataManager.userData != null && currentStageData.stageID >= 0 && currentStageData.stageID < userDataManager.userData.stageInfos.Length)
        {
            if (ScoreManager.Instance.GetCurrentScore() > 100f)
                userDataManager.userData.stageInfos[currentStageData.stageID].isCleared = true;

            // ���� ����
            if (userDataManager.userData.stageInfos[currentStageData.stageID].score < Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore()))
                userDataManager.userData.stageInfos[currentStageData.stageID].score = Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore());

            userDataManager.SaveUserData();
        }

        // ��� ������Ʈ �ʱ�ȭ
        eraser.EraseAll();
        // �� ��Ȱ��ȭ
        if (currentStageData != null)
        {
            tilemaps[currentStageData.stageID].SetActive(false);
            tilemaps[5].SetActive(true);
        }
        else
        {
            Debug.LogError("Stage data or tilemap is not set.");
            return;
        }
        // �������� ����
        GameManager.Instance.isPlayingStage = false;
        PlayerStat.Instance.DisablePlayer();
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        // HUD ��Ȱ��ȭ
        notificationManager.ClearNotifications();
        HUDContainer.SetActive(false);
        //HUDManager.DisableTimer();
        //HUDManager.DisableHealthBar();
        // �������� ���â Ȱ��ȭ
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayMainBGM();
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
