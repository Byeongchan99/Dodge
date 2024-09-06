using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour, IUpdateUI
{
    public Text characterNameText; // 캐릭터 이름
    public Text characterTypeText; // 캐릭터 타입
    public Text characterHPText; // 캐릭터 체력
    public Text characterAbilityText; // 캐릭터 어빌리티
    public Text characterAbilityDescriptionText; // 캐릭터 어빌리티 설명
    public Image characterImage; // 캐릭터 이미지

    // 캐릭터 정보 UI 업데이트
    public void UpdateCharacterInfo()
    {
        UpdateUIInfo(PlayerStat.Instance.currentCharacterData);
    }

    public void UpdateUIInfo(params object[] datas)
    {
        CharacterData characterData = null;

        foreach (var data in datas)
        {
            if (data is CharacterData)
            {
                characterData = data as CharacterData;
            }        
        }

        if (characterData != null)
        {
            characterNameText.text = characterData.characterName;
            characterTypeText.text = characterData.characterType;
            characterHPText.text = "HP: " + characterData.characterHP.ToString();
            characterAbilityText.text = "Ability: " + characterData.characterAbility;
            characterAbilityDescriptionText.text = characterData.characterAbilityDescription;
            characterImage.sprite = characterData.characterSprite;
        }
        else
        {
            Debug.Log("캐릭터 데이터가 없습니다.");
            characterNameText.text = "";
            characterTypeText.text = "";
            characterHPText.text = "";
            characterAbilityText.text = "";
            characterAbilityDescriptionText.text = "";
            characterImage.sprite = null;
        }
    }
}
