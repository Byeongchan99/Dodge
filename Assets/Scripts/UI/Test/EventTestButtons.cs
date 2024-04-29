using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTestButtons : MonoBehaviour
{
    public Button[] buttons; // 버튼 배열

    void Start()
    {
        // 모든 버튼에 클릭 이벤트 추가
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ToggleObjects(button));
        }
    }

    void ToggleObjects(Button selectedButton)
    {
        foreach (Button button in buttons)
        {
            // 선택된 버튼의 자식 오브젝트만 활성화
            Transform child = button.transform.GetChild(1); // 첫 번째 자식 오브젝트
            child.gameObject.SetActive(button == selectedButton);
        }
    }
}
