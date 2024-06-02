using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoButton : MonoBehaviour
{
    public StageData stageData;
    public UserData userData; // �ӽ� ���� ������
    public StageInfoUI stageInfoUI;

    void Start()
    {
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
            stageInfoUI.UpdateStageInfo(stageData, userData);
        }
    }
}
