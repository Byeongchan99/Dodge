using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public UserData userData;

    private void Awake()
    {
        LoadUserData();
    }

    // �����͸� �����ϴ� �޼���
    public void SaveUserData()
    {
#if UNITY_EDITOR
        // ������ ��忡�� ScriptableObject �����͸� ����
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#else
        // Firestore�� �����͸� ����
        SaveDataToFirestore();
#endif
    }

    // Firestore�� �����͸� �����ϴ� �޼���
    private void SaveDataToFirestore()
    {
        string json = userData.ToJson();
        FirestoreManager.Instance.SaveData("userData", json);
    }

    // �����͸� �ҷ����� �޼���
    public void LoadUserData()
    {
#if UNITY_EDITOR
        // ������ ��忡���� ���� �����͸� ���
#else
        // Firestore���� �����͸� �ҷ�����
        LoadDataFromFirestore();
#endif
    }

    // Firestore���� �����͸� �ҷ����� �޼���
    private void LoadDataFromFirestore()
    {
        FirestoreManager.Instance.LoadData("userData");
    }

    // Firestore���� �����͸� �ҷ����� �� ȣ��Ǵ� �޼���
    public void OnDataLoaded(string jsonData)
    {
        userData = UserData.FromJson(jsonData);
        Debug.Log("Data loaded: " + jsonData);
    }
}
