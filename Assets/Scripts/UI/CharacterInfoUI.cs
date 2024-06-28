using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour, IUpdateUI
{
    public Text characterNameText;
    public Text characterTypeText;
    public Text characterHPText;
    public Text characterAbilityText;
    public Text characterAbilityDescriptionText;
    public Image characterImage;

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
