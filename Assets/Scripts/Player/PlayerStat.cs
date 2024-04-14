using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // �̱��� �ν��Ͻ�

    public Transform currentPosition; // �÷��̾� ���� ��ġ

    [SerializeField] private int _maxHealth; // �÷��̾� �ִ� ü��
    public int currentHealth; // �÷��̾� ���� ü��
    [SerializeField] private float _initialMoveSpeed; // �÷��̾� �ʱ� �̵� �ӵ�
    public float currentMoveSpeed; // �÷��̾� ���� �̵� �ӵ�

    public bool isInvincibility; // ���� �������� ���� 

    public IPlayerAbility playerAbility; // �÷��̾� Ư�� �ɷ�
    [SerializeField] private Blink blink; // �÷��̾� ���� �ɷ�
    [SerializeField] private EMP emp; // �÷��̾� EMP �ɷ�
    [SerializeField] private DefenseProtocol defenseProtocol; // �÷��̾� ��� �������� �ɷ�

    private List<ItemEffect> activeItems = new List<ItemEffect>(); // ���� ���� ���� ������ ȿ�� ����Ʈ

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

    void Init()
    {
        currentHealth = _maxHealth;
        currentMoveSpeed = _initialMoveSpeed;
        currentPosition = transform;
        // Ư�� �ɷ� ���� ���� ���߿� �߰��ϱ�
        this.SetAbility(emp);
    }

    public void SetAbility(IPlayerAbility newAbility)
    {
        this.playerAbility = newAbility;
    }

    public void TakeDamage()
    {
        if (!isInvincibility) // ���� ���°� �ƴ� ��
        {
            // �ǰ� ó��
            currentHealth--;
        }
        // ���߿� �ǰ� ���� ����
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // �÷��̾� �ı�
        }
    }

    public void ApplyItemEffect(ItemEffect effect)
    {
        // ���� ���� ó��
        effect.ApplyEffect();
        activeItems.Add(effect);
        StartCoroutine(RemoveItemEffectEffectAfterDuration(effect));
    }

    private IEnumerator RemoveItemEffectEffectAfterDuration(ItemEffect effect)
    {
        yield return new WaitForSecondsRealtime(effect.GetDuration());
        effect.RemoveEffect();
        activeItems.Remove(effect);
    }

    public void RemoveAllEffects()
    {
        foreach (ItemEffect effect in activeItems)
        {
            effect.RemoveEffect();
        }
        activeItems.Clear();
    }
}
