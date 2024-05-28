using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public string characterType;
    public int characterTypeIndex;
    public int characterHP;
    public string characterAbility;
    public string characterAbilityDescription;
    public Sprite characterSprite;
}
