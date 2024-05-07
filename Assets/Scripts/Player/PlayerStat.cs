using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // �̱��� �ν��Ͻ�

    public Transform currentPosition; // �÷��̾� ���� ��ġ
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField] private int _maxHealth; // �÷��̾� �ִ� ü��
    public int currentHealth; // �÷��̾� ���� ü��
    [SerializeField] private float _initialMoveSpeed; // �÷��̾� �ʱ� �̵� �ӵ�
    public float currentMoveSpeed; // �÷��̾� ���� �̵� �ӵ�

    private Coroutine invincibilityRoutine = null; // ���� ���� �ڷ�ƾ
    private float _remainingInvincibilityDuration = 0f; // ���� ���� ���� ���� �ð�
    public bool isInvincibility; // ���� �������� ���� 

    public IPlayerAbility playerAbility; // �÷��̾� Ư�� �ɷ�
    [SerializeField] private Blink blink; // �÷��̾� ���� �ɷ�
    [SerializeField] private EMP emp; // �÷��̾� EMP �ɷ�
    [SerializeField] private DefenseProtocol defenseProtocol; // �÷��̾� ��� �������� �ɷ�

    private List<ItemEffect> activeItems = new List<ItemEffect>(); // ���� ���� ���� ������ ȿ�� ����Ʈ

    // ĳ���� Ÿ���� ���� enum
    public enum CharacterType
    {
        Light,
        Medium,
        Heavy
    }

    // ĳ���� ������
    [System.Serializable]
    public class CharacterData
    {
        public Sprite characterSprite;
        public RuntimeAnimatorController animatorController;  // �ִϸ����� ��Ʈ�ѷ� �߰�
        public int characterTypeIndex; // ĳ���� Ÿ�� �ε���
    }

    // ĳ���� ������ ����Ʈ
    public List<CharacterData> characterList;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
            Init();
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    void Update()
    {
        // �÷��̾ ������ ������ currentPosition�� ������Ʈ
        currentPosition.position = this.transform.position;
    }

    /// <summary> �ʱ�ȭ </summary>
    void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentHealth = _maxHealth;
        currentMoveSpeed = _initialMoveSpeed;
        currentPosition = transform;
        isInvincibility = false;

        // �⺻ ĳ���� ����
        SetCharacter(1);
    }

    // ĳ���� ���� �޼���
    public void SetCharacter(int typeIndex)
    {
        Debug.Log("ĳ���� ����: " + typeIndex);
        if (characterList[typeIndex] != null) 
        {
            CharacterData data = characterList[typeIndex];
            spriteRenderer.sprite = data.characterSprite;
            animator.runtimeAnimatorController = data.animatorController;  // �ִϸ����� ��Ʈ�ѷ� ����

            // �÷��̾� �����Ƽ ����
            if (typeIndex == 0)
            {
                playerAbility = blink;
            } 
            else if (typeIndex == 1)
            {
                playerAbility = emp;
            }
            else if (typeIndex == 2)
            {
                playerAbility = defenseProtocol;
            }
        }
    }

    /// <summary> ������ ó�� </summary>
    public void TakeDamage()
    {
        if (!isInvincibility) // ���� ���°� �ƴ� ��
        {
            // �ǰ� ó��
            currentHealth--;
            Debug.Log("�ǰ�! ���� ü��: " + currentHealth);
            StartInvincibility(1.5f); // 1�� ���� ����
        }
        // ���߿� �ǰ� ���� ����
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // �÷��̾� �ı�
        }
    }

    public void StartInvincibility(float duration)
    {
        if (invincibilityRoutine != null && _remainingInvincibilityDuration > duration)
        {
            Debug.Log("���� ���� ȿ���� �����ִ� �ð��� �� ��Ƿ� �� ��û ����");
            return;  // ���� �����ִ� ���ο� ����� �� ��� �� ��û ����
        }

        if (invincibilityRoutine != null)
        {
            StopCoroutine(invincibilityRoutine);  // ���� ���ο� ��� �ڷ�ƾ ����
        }

        invincibilityRoutine = StartCoroutine(ApplyInvincibility(duration));
    }

    /// <summary> ���� �ð� ���� Ÿ�̸� </summary>
    private IEnumerator ApplyInvincibility(float duration)
    {
        Debug.Log("���� ���� ����");
        isInvincibility = true;
        
        _remainingInvincibilityDuration = duration;  // ���� ���� �ð� ������Ʈ
        while (_remainingInvincibilityDuration > 0)
        {
            // �����̴� ����Ʈ
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);  // ������
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1);  // ���� ����
            yield return new WaitForSeconds(0.1f);
            _remainingInvincibilityDuration -= 0.2f;  // ���� �ð� ���ҷ� ������Ʈ
        }

        Debug.Log("���� ���� ����");
        invincibilityRoutine = null;
        isInvincibility = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    /// <summary> ������ ȿ�� ���� </summary>
    public void ApplyItemEffect(ItemEffect effect)
    {
        // ���� ���� ó��
        effect.ApplyEffect();
        activeItems.Add(effect);
        StartCoroutine(RemoveItemEffectEffectAfterDuration(effect));
    }

    /// <summary> ������ ȿ�� ���� Ÿ�̸� </summary>
    private IEnumerator RemoveItemEffectEffectAfterDuration(ItemEffect effect)
    {
        yield return new WaitForSecondsRealtime(effect.GetDuration());
        effect.RemoveEffect();
        activeItems.Remove(effect);
    }

    /// <summary> ������ ȿ�� ���� </summary>
    public void RemoveAllEffects()
    {
        foreach (ItemEffect effect in activeItems)
        {
            effect.RemoveEffect();
        }
        activeItems.Clear();
    }
}
