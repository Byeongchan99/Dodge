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
        // Button 컴포넌트를 가져와서 클릭 이벤트에 리스너 추가
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // 캐릭터 정보를 업데이트
        if (characterInfoUI != null && characterData != null)
        {
            characterInfoUI.UpdateCharacterInfo(characterData);
        }
    }
}
