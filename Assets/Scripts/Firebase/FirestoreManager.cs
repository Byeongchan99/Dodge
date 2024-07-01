using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class FirestoreManager : MonoBehaviour
{
    public UserData userData;

    [DllImport("__Internal")]
    private static extern void SaveData(string key, string value);

    [DllImport("__Internal")]
    private static extern void LoadData(string key);

    void Start()
    {
        SaveUserData();
        LoadUserData();
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData);
        SaveData("userData", json);
        Debug.Log("Data saved: " + json);
    }

    public void LoadUserData()
    {
        LoadData("userData");
    }

    // JavaScript에서 호출될 함수
    public void OnDataLoaded(string jsonData)
    {
        userData = UserData.FromJson(jsonData);
        Debug.Log("Data loaded: " + jsonData);
    }
}
