using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 2)]
public class StageData : ScriptableObject
{
    public int stageID;
    public string stageName;
    public string statDataName; // 적용하는 StatData
    public string stageInformation;
    public string leaderboardID; // 리더보드 ID
    public StatData statData;
    public GameObject mapPrefab; // 맵 프리팹을 저장하기 위해 GameObject 타입 사용
    public RuntimeAnimatorController turretAnimatorController; // 터렛 애니메이터 컨트롤러
}
