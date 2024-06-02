using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoButton : MonoBehaviour
{
    public StageData stageData;
    public UserData userData; // 임시 유저 데이터
    public StageInfoUI stageInfoUI;

    void Start()
    {
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
            stageInfoUI.UpdateStageInfo(stageData, userData);
        }
    }
}
