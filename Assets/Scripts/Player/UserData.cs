using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    [System.Serializable]
    public class StageInfo
    {
        public bool isCleared; // 클리어 여부
        public int score; // 획득 점수
    }  

    public string playerID; // 플레이어 ID
    public StageInfo[] stageInfos = new StageInfo[5]; // 현재 5개의 스테이지
}
