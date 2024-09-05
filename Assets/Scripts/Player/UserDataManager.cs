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
        // ������ ��忡�� ScriptableObject �����͸� ����
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    public void LoadUserData()
    {
#if UNITY_EDITOR
        // ������ ��忡���� ���� �����͸� ���
#endif
    }
}
