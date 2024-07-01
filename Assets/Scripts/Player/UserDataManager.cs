using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class UserDataManager : MonoBehaviour
{
    public UserData userData;

    [DllImport("__Internal")]
    private static extern void SaveData(string key, string value);

    [DllImport("__Internal")]
    private static extern void LoadData(string key);

    private void Awake()
    {
        LoadUserData();
    }

    public void SaveUserData()
    {
#if UNITY_EDITOR
        // ������ ��忡�� ScriptableObject �����͸� ����
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
        /*
        // Firestore�� �����͸� ����
        string json = userData.ToJson();
        SaveData("userData", json);
        */
    }

    public void LoadUserData()
    {
#if UNITY_EDITOR
        // ������ ��忡���� ���� �����͸� ���
#else
        // Firestore���� �����͸� �ҷ�����
        LoadData("userData");
#endif
    }

    // Firestore���� �����͸� �ҷ����� �� ȣ��Ǵ� �޼���
    public void OnDataLoaded(string jsonData)
    {
        userData = UserData.FromJson(jsonData);
        Debug.Log("Data loaded: " + jsonData);
    }
}
