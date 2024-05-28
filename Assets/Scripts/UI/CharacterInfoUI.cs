using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    public Text characterNameText;
    public Text characterTypeText;
    public Text characterHPText;
    public Text characterAbilityText;
    public Text characterAbilityDescriptionText;
    public Image characterImage;

    [SerializeField]
    private CharacterData initialCharacterData;

    void Start()
    {
        // 캐릭터 정보 초기화
        UpdateCharacterInfo(initialCharacterData);
    }

    public void UpdateCharacterInfo(CharacterData characterData)
    {
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
