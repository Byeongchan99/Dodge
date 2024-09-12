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

    public string playerID; // �÷��̾� ID
    public StageInfo[] stageInfos = new StageInfo[5]; // ���� 5���� ��������
}
