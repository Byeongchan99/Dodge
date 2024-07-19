using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class UserDataManager : MonoBehaviour
{
    public UserData userData;

    private void Awake()
    {
        LoadUserData();
    }

    public void SaveUserData()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 ScriptableObject 데이터를 저장
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    public void LoadUserData()
    {
#if UNITY_EDITOR
        // 에디터 모드에서는 로컬 데이터를 사용
#else
        // Firestore에서 데이터를 불러오기
        LoadData("userData");
#endif
    }

    // Firestore에서 데이터를 불러왔을 때 호출되는 메서드
    public void OnDataLoaded(string jsonData)
    {
        userData = UserData.FromJson(jsonData);
        Debug.Log("Data loaded: " + jsonData);
    }
}
