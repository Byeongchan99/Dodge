using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseProtocol : MonoBehaviour, IPlayerAbility
{
    public AudioClip defenseProtocolSound; // ��� �������� ȿ����

    [SerializeField] private float _defenseProtocolDuration = 2f; // ��� �������� ���� �ð�
    [SerializeField] private float _cooldownTime = 5f; // ��Ÿ�� 5��                                               
    public float CooldownTime // ��Ÿ�� ������Ƽ
    {
        get { return _cooldownTime; }
    }
    private float _nextAbilityTime = 0f; // ���� �ɷ� ��� ���� �ð�
    private bool _isDefense = false;

    PlayerMovement playerMovement;
    Animator animator;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void Execute()
    {
        if (!_isDefense && Time.unscaledTime >= _nextAbilityTime)
        {
            StartCoroutine(DefenseProtocolRoutine());
            _nextAbilityTime = Time.unscaledTime + _cooldownTime + _defenseProtocolDuration; // ���� ��� ���� �ð� ������Ʈ
        }
    }

    private IEnumerator DefenseProtocolRoutine()
    {
        _isDefense = true;

        // ���� ���
        AudioManager.instance.sfxAudioSource.PlayOneShot(defenseProtocolSound);

        // ������ ����
        if (playerMovement != null)
        {
            playerMovement.ChangeVelocity(Vector2.zero); // �÷��̾� ���߱�
            // X�� Y���� ��ġ ����
            playerMovement.FreezePosition();
            playerMovement.enabled = false;
        }

        // ���� ȿ�� Ȱ��ȭ
        PlayerStat.Instance.StartInvincibility(_defenseProtocolDuration + 0.1f);

        // ��� �������� �ִϸ��̼� ����
        animator.SetBool("IsAbility", true);

        yield return new WaitForSecondsRealtime(_defenseProtocolDuration);

        // ��� �������� ��Ȱ��ȭ
        // ��� �������� �ִϸ��̼� ����
        animator.SetBool("IsAbility", false);

        // ������ �簳
        // PlayerMovement ��ũ��Ʈ Ȱ��ȭ
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            playerMovement.UnFreezePosition();
        }

        _isDefense = false;
        PlayerStat.Instance.abilityCooldownUI.StartCooldown(); // ��Ÿ�� ����

        yield return null;
    }
}