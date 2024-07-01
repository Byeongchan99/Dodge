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

    // 데이터를 저장하는 메서드
    public void SaveUserData()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 ScriptableObject 데이터를 저장
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#else
        // Firestore에 데이터를 저장
        SaveDataToFirestore();
#endif
    }

    // Firestore에 데이터를 저장하는 메서드
    private void SaveDataToFirestore()
    {
        string json = userData.ToJson();
        FirestoreManager.Instance.SaveData("userData", json);
    }

    // 데이터를 불러오는 메서드
    public void LoadUserData()
    {
#if UNITY_EDITOR
        // 에디터 모드에서는 로컬 데이터를 사용
#else
        // Firestore에서 데이터를 불러오기
        LoadDataFromFirestore();
#endif
    }

    // Firestore에서 데이터를 불러오는 메서드
    private void LoadDataFromFirestore()
    {
        FirestoreManager.Instance.LoadData("userData");
    }

    // Firestore에서 데이터를 불러왔을 때 호출되는 메서드
    public void OnDataLoaded(string jsonData)
    {
        userData = UserData.FromJson(jsonData);
        Debug.Log("Data loaded: " + jsonData);
    }
}
