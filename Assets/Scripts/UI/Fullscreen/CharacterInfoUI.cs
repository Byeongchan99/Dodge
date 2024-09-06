using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour, IUpdateUI
{
    public Text characterNameText; // ĳ���� �̸�
    public Text characterTypeText; // ĳ���� Ÿ��
    public Text characterHPText; // ĳ���� ü��
    public Text characterAbilityText; // ĳ���� �����Ƽ
    public Text characterAbilityDescriptionText; // ĳ���� �����Ƽ ����
    public Image characterImage; // ĳ���� �̹���

    // ĳ���� ���� UI ������Ʈ
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
            Debug.Log("ĳ���� �����Ͱ� �����ϴ�.");
            characterNameText.text = "";
            characterTypeText.text = "";
            characterHPText.text = "";
            characterAbilityText.text = "";
            characterAbilityDescriptionText.text = "";
            characterImage.sprite = null;
        }
    }
}
