using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTestButtons : MonoBehaviour
{
    public Button[] buttons; // ��ư �迭

    void Start()
    {
        // ��� ��ư�� Ŭ�� �̺�Ʈ �߰�
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ToggleObjects(button));
        }
    }

    void ToggleObjects(Button selectedButton)
    {
        foreach (Button button in buttons)
        {
            // ���õ� ��ư�� �ڽ� ������Ʈ�� Ȱ��ȭ
            Transform child = button.transform.GetChild(1); // ù ��° �ڽ� ������Ʈ
            child.gameObject.SetActive(button == selectedButton);
        }
    }
}
