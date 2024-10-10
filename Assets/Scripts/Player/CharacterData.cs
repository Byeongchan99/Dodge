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
    public Sprite abilityIconSprite; // �����Ƽ ������
    public RuntimeAnimatorController animatorController;  // �ִϸ����� ��Ʈ�ѷ�
    public Vector2 colliderOffset; // �ݶ��̴� ������
    public Vector2 colliderSize; // �ݶ��̴� ũ��
    public CapsuleDirection2D colliderDirection; // �ݶ��̴� ����
    public Vector2 standAreaColliderOffset; // ���ִ� ���� �ݶ��̴� ������
    public Vector2 standAreaColliderSize; // ���ִ� ���� �ݶ��̴� ũ��
    public Vector3 crownPosition; // �հ� ��ġ
}
