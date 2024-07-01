using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class FirestoreManager : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void SaveData(string key, string value);

    [DllImport("__Internal")]
    private static extern void LoadData(string key);

    void Start()
    {
        // 예제: 데이터 저장
        SaveData("userData", UserDataToJson());

        // 예제: 데이터 불러오기
        LoadData("userData");
    }

    private string UserDataToJson()
    {
        return JsonUtility.ToJson(userData);
    }

    private void LoadJsonToUserData(string json)
    {
        userData = JsonUtility.FromJson<UserData>(json);
    }

    // JavaScript에서 호출될 함수
    public void OnDataLoaded(string jsonData)
    {
        LoadJsonToUserData(jsonData);
        Debug.Log("Data loaded: " + jsonData);
    }
}
