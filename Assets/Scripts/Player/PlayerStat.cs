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

    [SerializeField] private Blink blink; // �÷��̾� ���� �ɷ�
    [SerializeField] private EMP emp; // �÷��̾� EMP �ɷ�
    [SerializeField] private DefenseProtocol defenseProtocol; // �÷��̾� ��� �������� �ɷ�

    private List<ItemEffect> activeItems = new List<ItemEffect>(); // ���� ���� ���� ������ ȿ�� ����Ʈ

    /****************************************************************************
                             public Fields
    ****************************************************************************/
    public static PlayerStat Instance { get; private set; } // �̱��� �ν��Ͻ�

    public GameObject player; // �÷��̾� ���� ������Ʈ
    public Transform currentPosition; // �÷��̾� ���� ��ġ
    public SpriteRenderer spriteRenderer; // �÷��̾� ��������Ʈ ������
    public Animator animator;

    public int MaxHealth // �÷��̾� �ִ� ü�� ������Ƽ
    {
        get { return _maxHealth; }
        private set { _maxHealth = value; }
    }
    public int currentHealth; // �÷��̾� ���� ü��
    public float currentMoveSpeed; // �÷��̾� ���� �̵� �ӵ�

    public bool isInvincibility; // ���� �������� ���� 

    public IPlayerAbility playerAbility; // �÷��̾� Ư�� �ɷ�

    // ĳ���� ����
    public List<CharacterData> characterList; // ĳ���� ������ ����Ʈ
    public CharacterData currentCharacterData; // ���� ĳ���� ������
    public CharacterData selectedCharacterData; // ������ ĳ���� ������

    // UI
    public AbilityCooldownUI abilityCooldownUI; // �ɷ� ��Ÿ�� UI

    // ���� ������
    public UserDataManager userDataManager;
    public GameObject crown; // �հ�
    SpriteRenderer crownSpriteRenderer; // �հ� ��������Ʈ ������

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
        crownSpriteRenderer = crown.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentMoveSpeed = _initialMoveSpeed;
        currentPosition = transform;
        isInvincibility = false;

        if (userDataManager.userData.stageInfos[4].isCleared)
        {
            crown.SetActive(true);
        }
        else
        {
            crown.SetActive(false);
        }

        SetCharacter();
        player.SetActive(false);
    }

    // ���� ���� �ڷ�ƾ�� �ʱ�ȭ
    void InitCoroutines()
    {
        // ���� ���� �ڷ�ƾ ����
        if (invincibilityRoutine != null)
        {
            StopCoroutine(invincibilityRoutine);
            invincibilityRoutine = null;
            isInvincibility = false;
            spriteRenderer.color = new Color(1, 1, 1, 1);
            crownSpriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    /// <summary> ������ ȿ�� �ڷ�ƾ </summary>
    private IEnumerator FlickerEffect(float duration)
    {
        float timeLeft = duration;  // ���� �ð��� �����ϴ� ����
        while (timeLeft > 0)
        {
            // �����̴� ����Ʈ: ������
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            crownSpriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);  // 0.1�� ���

            // �����̴� ����Ʈ: ���� ����
            spriteRenderer.color = new Color(1, 1, 1, 1);
            crownSpriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);  // 0.1�� ���

            timeLeft -= 0.2f;  // ���� �ð�(0.1 + 0.1)��ŭ �ð� ����
        }
        // ���� ���� �� ���� ���� ����
        spriteRenderer.color = new Color(1, 1, 1, 1);
        crownSpriteRenderer.color = new Color(1, 1, 1, 1);
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
        crownSpriteRenderer.color = new Color(1, 1, 1, 1);
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
    /// <summary> ĳ���� ���� </summary>
    public void SelectCharacter(int characterTypeIndex)
    {
        selectedCharacterData = characterList[characterTypeIndex];
    }

    /// <summary> ĳ���� ���� </summary>
    public void SetCharacter()
    {
        currentCharacterData = selectedCharacterData;
        Debug.Log("ĳ���� ����: " + currentCharacterData.characterType);
        if (characterList[currentCharacterData.characterTypeIndex] != null)
        {
            spriteRenderer.sprite = currentCharacterData.characterSprite;
            animator.runtimeAnimatorController = currentCharacterData.animatorController;  // �ִϸ����� ��Ʈ�ѷ� ����

            // �÷��̾� Ÿ�Կ� ���� ü�°� �����Ƽ ����
            if (currentCharacterData.characterTypeIndex == 0)
            {
                playerAbility = blink;
                _maxHealth = 2;
                abilityCooldownUI.Init(blink.CooldownTime);
            }
            else if (currentCharacterData.characterTypeIndex == 1)
            {
                playerAbility = emp;
                _maxHealth = 3;
                abilityCooldownUI.Init(emp.CooldownTime);
            }
            else if (currentCharacterData.characterTypeIndex == 2)
            {
                playerAbility = defenseProtocol;
                _maxHealth = 4;
                abilityCooldownUI.Init(defenseProtocol.CooldownTime);
            }
        }
        currentHealth = _maxHealth;
        NotifyObservers();
    }

    /// <summary> �÷��̾� ��Ȱ��ȭ </summary>
    public void DisablePlayer()
    {
        Debug.Log("�÷��̾� ��Ȱ��ȭ");
        RemoveAllEffects(); // ��� ������ ȿ�� ����
        InitCoroutines(); // ��� �ڷ�ƾ �ʱ�ȭ
        player.SetActive(false);
    }

    /// <summary> ������ ó�� </summary>
    public void TakeDamage()
    {
        if (!isInvincibility && currentHealth > 0) // ���� ���°� �ƴ� ��
        {
            StartCoroutine(FlickerEffect(1.5f)); // 1.5�� ���� ������ ȿ��
            StartInvincibility(1.5f); // 1�� ���� ����
            // �ǰ� ó��
            currentHealth--;
            NotifyObservers();
            Debug.Log("�ǰ�! ���� ü��: " + currentHealth);    
        }

        // ���߿� �ǰ� ���� ����
        if (currentHealth <= 0)
        {
            DisablePlayer(); // �÷��̾� ��Ȱ��ȭ
            /*
            GameManager.Instance.testGameOver();
            //Destroy(gameObject); // �÷��̾� �ı�
            */
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
