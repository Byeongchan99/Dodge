using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 2)]
public class StageData : ScriptableObject
{
    public int stageID;
    public string stageName;
    public string statDataName; // �����ϴ� StatData
    public string stageInformation;
    public string leaderboardID; // �������� ID
    public StatData statData;
    public GameObject mapPrefab; // �� �������� �����ϱ� ���� GameObject Ÿ�� ���
    public RuntimeAnimatorController turretAnimatorController; // �ͷ� �ִϸ����� ��Ʈ�ѷ�
}
