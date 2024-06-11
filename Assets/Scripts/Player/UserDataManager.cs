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

    // 임시로 UserData를 저장하는 메소드
    public void SaveUserData()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 ScriptableObject 데이터를 저장
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    // UserData를 로드하는 메소드
    public void LoadUserData()
    {

    }
}
