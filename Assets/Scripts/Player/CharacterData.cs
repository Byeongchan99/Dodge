using UnityEngine;
using UnityEngine.UI;

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
    public Sprite abilityIconSprite; // 어빌리티 아이콘
    public RuntimeAnimatorController animatorController;  // 애니메이터 컨트롤러
    public Vector2 colliderSize; // 콜라이더 크기
    public Vector2 colliderOffset; // 콜라이더 오프셋
    public Vector3 crownPosition; // 왕관 위치
}
