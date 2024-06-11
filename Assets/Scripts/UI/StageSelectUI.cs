using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    public UserDataManager userDataManager;
    public Button[] stageButtons;

    private void Start()
    {
        UpdateStageButtons();
    }

    public void UpdateStageButtons()
    {
        // ù ��° ���������� �׻� Ȱ��ȭ
        stageButtons[0].interactable = true;

        for (int i = 1; i < stageButtons.Length; i++)
        {
            if (userDataManager.userData.stageInfos[i - 1].isCleared)
            {
                stageButtons[i].interactable = true;
            }
            else
            {
                stageButtons[i].interactable = false;
            }
        }
    }
}
