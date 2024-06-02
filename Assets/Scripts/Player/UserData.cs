using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    [System.Serializable]
    public class StageInfo
    {
        public bool isCleared;
        public int score;
    }

    public StageInfo[] stageInfos = new StageInfo[5]; // 현재 5개의 스테이지
}
