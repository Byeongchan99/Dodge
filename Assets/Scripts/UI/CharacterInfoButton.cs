using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoButton : MonoBehaviour
{
    public CharacterData characterData;
    public CharacterInfoUI characterInfoUI;

    void Start()
    {
        // Button ������Ʈ�� �����ͼ� Ŭ�� �̺�Ʈ�� ������ �߰�
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // ĳ���� ������ ������Ʈ
        if (characterInfoUI != null && characterData != null)
        {
            characterInfoUI.UpdateCharacterInfo(characterData);
        }
    }
}
