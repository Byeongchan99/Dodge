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

    [System.Serializable]
    public class AchievementInfo
    {
        public string name; // 도전과제 이름
        public bool isAchieved; // 클리어 여부
    }

    public StageInfo[] stageInfos = new StageInfo[5]; // 현재 5개의 스테이지
    public AchievementInfo[] achievementInfos = new AchievementInfo[5]; // 현재 5개의 도전과제
}
