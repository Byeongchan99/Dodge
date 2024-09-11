using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Unity.Services.Authentication;

public class UserDataManager : MonoBehaviour
{
    public UserData userData;

    private void Awake()
    {

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
        userData.playerID = AuthenticationService.Instance.PlayerId;

#if UNITY_EDITOR
        // ������ ��忡���� ���� �����͸� ���
#endif
    }
}
