using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    /****************************************************************************
                                 private Fields
    ****************************************************************************/
    private List<IHealthObserver> observers = new List<IHealthObserver>(); // ü�� ������ ����Ʈ

    [SerializeField] private int _maxHealth; // �÷��̾� �ִ� ü��
    [SerializeField] private float _initialMoveSpeed; // �÷��̾� �ʱ� �̵� �ӵ�

    private Coroutine invincibilityRoutine = null; // ���� ���� �ڷ�ƾ
    private float _remainingInvincibilityDuration = 0f; // ���� ���� ���� ���� �ð�

    private int _currentCharacterType = 0; // �⺻ ĳ���� Ÿ��
    [SerializeField] private Blink blink; // �÷��̾� ���� �ɷ�
    [SerializeField] private EMP emp; // �÷��̾� EMP �ɷ�
    [SerializeField] private DefenseProtocol defenseProtocol; // �÷��̾� ��� �������� �ɷ�

    private List<ItemEffect> activeItems = new List<ItemEffect>(); // ���� ���� ���� ������ ȿ�� ����Ʈ

    /****************************************************************************
                             public Fields
    ****************************************************************************/
    public static PlayerStat Instance { get; private set; } // �̱��� �ν��Ͻ�

    public Transform currentPosition; // �÷��̾� ���� ��ġ
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public int currentHealth; // �÷��̾� ���� ü��
    public float currentMoveSpeed; // �÷��̾� ���� �̵� �ӵ�

    public bool isInvincibility; // ���� �������� ���� 

    public IPlayerAbility playerAbility; // �÷��̾� Ư�� �ɷ�

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

    /****************************************************************************
                               Unity Callbacks
    ****************************************************************************/
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

    /****************************************************************************
                                private Methods
    ****************************************************************************/
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

    /// <summary> ������ ȿ�� �ڷ�ƾ </summary>
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

    /// <summary> ������ ȿ�� ���� Ÿ�̸� </summary>
    private IEnumerator RemoveItemEffectEffectAfterDuration(ItemEffect effect)
    {
        yield return new WaitForSecondsRealtime(effect.GetDuration());
        effect.RemoveEffect();
        activeItems.Remove(effect);
    }

    /****************************************************************************
                                public Methods
    ****************************************************************************/
    /// <summary> ĳ���� ���� ��ư Ŭ�� </summary>
    public void OnCharacterTypeClicked(int typeIndex)
    {
        _currentCharacterType = typeIndex;
        Debug.Log("������ ĳ���� Ÿ��: " + _currentCharacterType);
    }

    /// <summary> ĳ���� ���� </summary>
    public void SetCharacter()
    {
        Debug.Log("ĳ���� ����: " + _currentCharacterType);
        if (characterList[_currentCharacterType] != null)
        {
            CharacterData data = characterList[_currentCharacterType];
            spriteRenderer.sprite = data.characterSprite;
            animator.runtimeAnimatorController = data.animatorController;  // �ִϸ����� ��Ʈ�ѷ� ����

            // �÷��̾� Ÿ�Կ� ���� ü�°� �����Ƽ ����
            if (_currentCharacterType == 0)
            {
                playerAbility = blink;
                _maxHealth = 2;
            }
            else if (_currentCharacterType == 1)
            {
                playerAbility = emp;
                _maxHealth = 3;
            }
            else if (_currentCharacterType == 2)
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
            NotifyObservers();
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

    /// <summary> ���� ȿ�� ���� </summary>
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


    /// <summary> ������ ȿ�� ���� </summary>
    public void ApplyItemEffect(ItemEffect effect)
    {
        // ���� ���� ó��
        effect.ApplyEffect();
        activeItems.Add(effect);
        StartCoroutine(RemoveItemEffectEffectAfterDuration(effect));
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

    /****************************************************************************
                               Observer Pattern
    ****************************************************************************/
    /// <summary> ������ ��� </summary>
    public void RegisterObserver(IHealthObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    /// <summary> ������ ���� </summary>
    public void UnregisterObserver(IHealthObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    /// <summary> ���������� �˸� </summary>
    private void NotifyObservers()
    {
        foreach (IHealthObserver observer in observers)
        {
            observer.OnHealthChanged(currentHealth);
        }
    }
}
