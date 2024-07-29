using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseProtocol : MonoBehaviour, IPlayerAbility
{
    [SerializeField] private float _defenseProtocolDuration = 2f; // ��� �������� ���� �ð�
    [SerializeField] private float _cooldownTime = 5f; // ��Ÿ�� 5��                                              
    public float CooldownTime // ��Ÿ�� ������Ƽ
    {
        get { return _cooldownTime; }
    }
    private float _nextAbilityTime = 0f; // ���� �ɷ� ��� ���� �ð�

    private bool isDefense = false;

    public void Execute()
    {
        if (!isDefense && Time.unscaledTime >= _nextAbilityTime)
        {
            StartCoroutine(DefenseProtocolRoutine());
            _nextAbilityTime = Time.unscaledTime + _cooldownTime + _defenseProtocolDuration; // ���� ��� ���� �ð� ������Ʈ
        }
    }

    private IEnumerator DefenseProtocolRoutine()
    {
        isDefense = true;

        // ������ ����
        // PlayerMovement ��ũ��Ʈ ��Ȱ��ȭ �� Rigidbody2D ����
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        Rigidbody2D rb = playerMovement != null ? playerMovement.rb : null;

        if (playerMovement != null)
        {
            playerMovement.rb.velocity = Vector2.zero;
            // X�� Y���� ��ġ ����
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            playerMovement.enabled = false;
        }

        // ���� ȿ�� Ȱ��ȭ
        PlayerStat.Instance.StartInvincibility(_defenseProtocolDuration + 0.1f);

        // �÷��̾��� ��������Ʈ�� ��������� ����
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        if (playerSprite != null)
        {
            playerSprite.color = Color.yellow;
        }

        yield return new WaitForSecondsRealtime(_defenseProtocolDuration);

        // ��� �������� ��Ȱ��ȭ
        // �÷��̾��� ��������Ʈ�� ���� ������ ����
        if (playerSprite != null)
        {
            playerSprite.color = Color.white; // ���� �������� �ǵ���
        }

        // ������ �簳
        // PlayerMovement ��ũ��Ʈ Ȱ��ȭ
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        isDefense = false;
        PlayerStat.Instance.abilityCooldownUI.StartCooldown(); // ��Ÿ�� ����

        yield return null;
    }
}
