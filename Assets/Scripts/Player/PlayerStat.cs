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

    public IPlayerAbility playerAbility; // �÷��̾� Ư�� �ɷ�
    [SerializeField] private Blink blink; // �÷��̾� ���� �ɷ�
    [SerializeField] private EMP emp; // �÷��̾� EMP �ɷ�

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
        // �ǰ� ó��
        currentHealth--;
        // ���߿� �ǰ� ���� ����
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // �÷��̾� �ı�
        }
    }

    public void ApplyBuff()
    {
        // ���� ���� ó��
    }

    public void RemoveBuff()
    {
        // ���� ���� ó��
    }
}
