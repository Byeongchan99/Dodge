using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
    [System.Serializable]
    public class StageInfo
    {
        public bool isCleared; // Ŭ���� ����
        public int score; // ȹ�� ����
    }

    [System.Serializable]
    public class AchievementInfo
    {
        public string name; // �������� �̸�
        public bool isAchieved; // Ŭ���� ����
    }

    public StageInfo[] stageInfos = new StageInfo[5]; // ���� 5���� ��������
    public AchievementInfo[] achievementInfos = new AchievementInfo[5]; // ���� 5���� ��������
}
