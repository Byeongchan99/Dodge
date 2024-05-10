using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    
        currentMoveSpeed = _initialMoveSpeed;
        currentPosition = transform;
        isInvincibility = false;

        // �⺻ ĳ���� ����
        SetCharacter();
    }

    private int currentCharacterType = 0; // �⺻ ĳ���� Ÿ��
    public void OnCharacterTypeClicked(int typeIndex)
    {
        currentCharacterType = typeIndex;
        Debug.Log("������ ĳ���� Ÿ��: " + currentCharacterType);
    }

    // ĳ���� ���� �޼���
    public void SetCharacter()
    {
        Debug.Log("ĳ���� ����: " + currentCharacterType);
        if (characterList[currentCharacterType] != null) 
        {
            CharacterData data = characterList[currentCharacterType];
            spriteRenderer.sprite = data.characterSprite;
            animator.runtimeAnimatorController = data.animatorController;  // �ִϸ����� ��Ʈ�ѷ� ����

            // �÷��̾� Ÿ�Կ� ���� ü�°� �����Ƽ ����
            if (currentCharacterType == 0)
            {
                playerAbility = blink;
                _maxHealth = 2;
            } 
            else if (currentCharacterType == 1)
            {
                playerAbility = emp;
                _maxHealth = 3;
            }
            else if (currentCharacterType == 2)
            {
                playerAbility = defenseProtocol;
                _maxHealth = 4;
            }
        }
        currentHealth = _maxHealth;
    }

    /// <summary> ������ ó�� </summary>
    public void TakeDamage()
    {
        if (!isInvincibility) // ���� ���°� �ƴ� ��
        {
            // �ǰ� ó��
            currentHealth--;
            Debug.Log("�ǰ�! ���� ü��: " + currentHealth);
            StartCoroutine(FlickerEffect(1.5f)); // 1.5�� ���� ������ ȿ��
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

    private IEnumerator FlickerEffect(float duration)
    {
        float timeLeft = duration;  // ���� �ð��� �����ϴ� ����
        while (timeLeft > 0)
        {
            // �����̴� ����Ʈ: ������
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);  // 0.1�� ���

            // �����̴� ����Ʈ: ���� ����
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);  // 0.1�� ���

            timeLeft -= 0.2f;  // ���� �ð�(0.1 + 0.1)��ŭ �ð� ����
        }
        // ���� ���� �� ���� ���� ����
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    /// <summary> ���� �ð� ���� Ÿ�̸� </summary>
    private IEnumerator ApplyInvincibility(float duration)
    {
        Debug.Log("���� ���� ����");
        isInvincibility = true;
        
        _remainingInvincibilityDuration = duration;  // ���� ���� �ð� ������Ʈ
        while (_remainingInvincibilityDuration > 0)
        {
            yield return new WaitForSeconds(0.1f);
            _remainingInvincibilityDuration -= 0.1f;  // ���� �ð� ���ҷ� ������Ʈ
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
