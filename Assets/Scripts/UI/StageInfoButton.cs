using System.Collections;
using System.Collections.Generic;
using UIManage;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoButton : MonoBehaviour
{
    public StageData stageData;
    public UserData userData; // 임시 유저 데이터
    public StageInfoUI stageInfoUI;

    // 필요한 매니저들
    public StatDataManager statDataManager;
    public TurretUpgradeHandler turretUpgradeHandler;
    public PopupUIManager popupUIManager;
    public StageManager stageManager;

    // 매개변수들
    public string statDataParameter;
    public int turretUpgradeParameter;
    public int setStageDataParameter;

    void Start()
    {
        // 매니저들 동적 참조 설정
        statDataManager = FindObjectOfType<StatDataManager>();
        turretUpgradeHandler = FindObjectOfType<TurretUpgradeHandler>();
        popupUIManager = FindObjectOfType<PopupUIManager>();
        stageManager = FindObjectOfType<StageManager>();

        // Button 컴포넌트를 가져와서 클릭 이벤트에 리스너 추가
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // 스테이지 정보를 업데이트
        if (stageInfoUI != null && stageData != null && userData != null)
        {
            stageInfoUI.UpdateUIInfo(stageData, userData);
        }

        // 각 매니저의 메서드 호출
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
