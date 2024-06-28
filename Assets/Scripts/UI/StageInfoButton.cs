using System.Collections;
using System.Collections.Generic;
using UIManage;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoButton : MonoBehaviour
{
    public StageData stageData;
    public UserData userData; // �ӽ� ���� ������
    public StageInfoUI stageInfoUI;

    // �ʿ��� �Ŵ�����
    public StatDataManager statDataManager;
    public TurretUpgradeHandler turretUpgradeHandler;
    public PopupUIManager popupUIManager;
    public StageManager stageManager;

    // �Ű�������
    public string statDataParameter;
    public int turretUpgradeParameter;
    public int setStageDataParameter;

    void Start()
    {
        // �Ŵ����� ���� ���� ����
        statDataManager = FindObjectOfType<StatDataManager>();
        turretUpgradeHandler = FindObjectOfType<TurretUpgradeHandler>();
        popupUIManager = FindObjectOfType<PopupUIManager>();
        stageManager = FindObjectOfType<StageManager>();

        // Button ������Ʈ�� �����ͼ� Ŭ�� �̺�Ʈ�� ������ �߰�
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // �������� ������ ������Ʈ
        if (stageInfoUI != null && stageData != null && userData != null)
        {
            stageInfoUI.UpdateUIInfo(stageData, userData);
        }

        // �� �Ŵ����� �޼��� ȣ��
        if (statDataManager != null)
        {
            statDataManager.SetOriginalStatData(statDataParameter);
        }

        if (turretUpgradeHandler != null)
        {
            turretUpgradeHandler.SetStageEvents(turretUpgradeParameter);
        }

        if (popupUIManager != null)
        {
            popupUIManager.OpenStageInformationPopup();
        }

        if (stageManager != null)
        {
            stageManager.SetStageData(setStageDataParameter);
        }
    }
}
