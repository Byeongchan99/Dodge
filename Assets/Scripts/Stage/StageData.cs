using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 2)]
public class StageData : ScriptableObject
{
    public int stageID;
    public string stageName;
    public string statDataName;
    public StatData statData;
    public GameObject mapPrefab; // 맵 프리팹을 저장하기 위해 GameObject 타입 사용
}
