using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 2)]
public class StageData : ScriptableObject
{
    public int stageID;
    public string stageName;
    public string statDataName;
    public StatData statData;
    public GameObject mapPrefab; // �� �������� �����ϱ� ���� GameObject Ÿ�� ���
}
