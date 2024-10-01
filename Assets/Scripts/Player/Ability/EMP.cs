using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour, IPlayerAbility
{
    private BaseEffect EMPEffect; // EMP ȿ��
    public AudioClip EMP_Sound; // EMP ȿ�� ����

    [SerializeField] private float _slowDuration = 0.05f; // ���ο� ���� �ð�
    [SerializeField] private float _EMPDuration = 0.5f; // EMP ���� �ð�
    [SerializeField] private float _cooldownTime = 4f; // ��Ÿ�� 5��
    public float CooldownTime // ��Ÿ�� ������Ƽ
    {
        get { return _cooldownTime; }
    }
    private float _nextAbilityTime = 0f; // ���� �ɷ� ��� ���� �ð�
    private bool _isEMP = false; // ���� EMP ��� ������ ����

    public void Execute()
    {
        if (!_isEMP && Time.unscaledTime >= _nextAbilityTime)
        {
            StartCoroutine(EMPRoutine());
            _nextAbilityTime = Time.unscaledTime + _cooldownTime + _EMPDuration; // ���� ��� ���� �ð� ������Ʈ
        }
    }

    private IEnumerator EMPRoutine()
    {
        _isEMP = true;

        // ���� ���
        AudioManager.instance.sfxAudioSource.PlayOneShot(EMP_Sound);
        // ������ ����
        // PlayerMovement ��ũ��Ʈ ��Ȱ��ȭ
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ChangeVelocity(Vector2.zero); // �÷��̾� ���߱�
            playerMovement.enabled = false;
        }
        else
        {
            Debug.LogWarning("PlayerMovement ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }

        // EMP Ȱ��ȭ
        EMPEffect = EffectPoolManager.Instance.Get("EMPEffect");
        EMPEffect.transform.position = PlayerStat.Instance.currentPosition.position;
        EMPEffect.gameObject.SetActive(true);

        // �ð��� ������ �Ѵ�
        GameManager.Instance.StartSlowEffect(_slowDuration); // ���ο� ��� ȿ�� ����
        GameManager.Instance.isAbilitySlowMotion = true;

        // EMP ���ӽð� ���� ���
        yield return new WaitForSeconds(_EMPDuration - _slowDuration);

        // EMP ��Ȱ��ȭ
        if (EMPEffect != null)
        {
            EffectPoolManager.Instance.Return("EMPEffect", EMPEffect);  // ĳ�õ� ȿ�� �ν��Ͻ��� Ǯ�� ��ȯ
            EMPEffect = null;  // ���� ����
        }

        // PlayerMovement ��ũ��Ʈ Ȱ��ȭ
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        _isEMP = false;
        PlayerStat.Instance.abilityCooldownUI.StartCooldown(); // ��Ÿ�� ����

        yield return null;
    }
}
