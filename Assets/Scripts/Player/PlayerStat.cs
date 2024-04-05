using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // �̱��� �ν��Ͻ�

    public Transform currentPosition; // �÷��̾� ���� ��ġ

    public int maxHealth = 3; // �÷��̾� �ִ� ü��
    public int currentHealth; // �÷��̾� ���� ü��
    public float initialMoveSpeed = 150f; // �÷��̾� �ʱ� �̵� �ӵ�
    public float currentMoveSpeed; // �÷��̾� ���� �̵� �ӵ�

    public IPlayerAbility playerAbility; // �÷��̾� Ư�� �ɷ�
    public Blink blink; // �÷��̾� ���� �ɷ�

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
        currentHealth = maxHealth;
        currentMoveSpeed = initialMoveSpeed;
        currentPosition = transform;
        this.SetAbility(blink);
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
