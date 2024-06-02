using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    public Text stageNameText;
    public Text stageInformationText;
    public GameObject isGameClearImage;

    public void UpdateStageInfo(StageData stageData, UserData userData)
    {
        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            stageInformationText.text = stageData.stageInformation;
        }

        // �ӽ� ���� ������ ���
        if (userData != null)
        {
            if (userData.stageInfos[stageData.stageID].isCleared)
            {
                isGameClearImage.SetActive(true);
            }
            else
            {
                isGameClearImage.SetActive(false);
            }
            OnStarChanged(userData.stageInfos[stageData.stageID].score);
        }
    }

    [SerializeField] private GameObject[] StarUnits; // �� ĭ�� ��Ÿ���� GameObject �迭

    public void OnStarChanged(float score)
    {
        // �� Ȱ��ȭ
        for (int i = 0; i < 3; i++)
        {
            if (i < score / 30 - 1)  
            {
                StarUnits[i].SetActive(true);
            }
            else
            {
                StarUnits[i].SetActive(false);
            }
        }
    }
}
