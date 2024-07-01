using UnityEngine;
using System.Runtime.InteropServices;

public class FirebaseInitializer : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void InitializeFirebase();

    void Start()
    {
        InitializeFirebase();
    }
}
