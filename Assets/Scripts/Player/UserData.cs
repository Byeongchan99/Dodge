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

    // JSON으로 변환
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    // JSON으로부터 객체를 복원
    public static UserData FromJson(string json)
    {
        return JsonUtility.FromJson<UserData>(json);
    }
}
