using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // �н� ������
    public AudioClip blinkSound; // ��ũ ȿ����
    private GameObject playerClone; // ���ӿ� ������ �н� ��ü
    private PlayerMovement playerMovement; // �÷��̾� �̵� ��ũ��Ʈ
    private SpriteRenderer cloneSpriteRenderer; // �н��� ��������Ʈ ������
    [SerializeField] private LayerMask wallLayerMask; // �� ���̾� ����ũ

    [SerializeField] private float _blinkMoveSpeed = 5.0f; // ��ũ ���� �ð� ���� �̵� �ӵ�
    [SerializeField] private float _blinkDuration = 0.5f; // ���� ���� �ð�
    [SerializeField] private float _cooldownTime = 3f; // ��Ÿ��
    public float CooldownTime // ��Ÿ�� ������Ƽ
    {
        get { return _cooldownTime; }
    }
    private float _nextAbilityTime = 0f; // ���� �ɷ� ��� ���� �ð�
    private bool _isBlinking = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Execute()
    {
        if (!_isBlinking && Time.unscaledTime >= _nextAbilityTime)
        {
            Debug.Log("��ũ ����");
            StartCoroutine(BlinkRoutine());
            Debug.Log("��ũ ����");
            _nextAbilityTime = Time.unscaledTime + _cooldownTime + _blinkDuration; // ���� ��� ���� �ð� ������Ʈ
        }
    }

    // �ɷ� ���� �ð����� �н� �����ϰ� �ɷ� ���� �� ��ü ��ġ�� �н� ��ġ�� ������Ʈ
    private IEnumerator BlinkRoutine()
    {
        _isBlinking = true;

        // ���� ���
        AudioManager.instance.sfxAudioSource.PlayOneShot(blinkSound);

        // ��ü ��Ȱ��ȭ
        // PlayerMovement ��ũ��Ʈ ��Ȱ��ȭ
        if (playerMovement != null)
        {
            playerMovement.ChangeVelocity(Vector2.zero); // �÷��̾� ���߱�
            playerMovement.enabled = false;
        }

        // �н� Ȱ��ȭ
        if (playerClone == null)
        {
            playerClone = Instantiate(playerClonePrefab, PlayerStat.Instance.currentPosition.position, Quaternion.identity);
        }
        playerClone.SetActive(true);

        // �ð��� ������ �Ѵ�
        GameManager.Instance.StartSlowEffect(_blinkDuration);
        GameManager.Instance.isAbilitySlowMotion = true;

        // �н� ���� ��ġ ����
        Vector3 startPosition = PlayerStat.Instance.currentPosition.position;
        Vector3 nextPosition = startPosition;
        playerClone.transform.position = startPosition;

        float elapsedTime = 0f;
        // Ư�� �ɷ� ���� �ð����� �н� ����
        while (elapsedTime < _blinkDuration)
        {
            // �Է¿� ���� ��ġ ���
            float horizontal = Input.GetAxisRaw("Horizontal") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            float vertical = Input.GetAxisRaw("Vertical") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            Vector3 moveDirection = new Vector3(horizontal, vertical, 0);

            if (cloneSpriteRenderer == null)
            {
                cloneSpriteRenderer = playerClone.GetComponent<SpriteRenderer>();
            }

            if (moveDirection.x != 0)
            {
                cloneSpriteRenderer.flipX = moveDirection.x < 0;
            }

            /*
            nextPosition += moveDirection;

            // �н� ��ġ ������Ʈ
            playerClone.transform.position = nextPosition;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
            */
            // �������� ���ο� ��ġ ���
            Vector3 potentialPosition = nextPosition + moveDirection;

            // �������� ��ġ������ �浹 �˻�
            if (!IsCollidingWithWall(nextPosition, moveDirection))
            {
                // �浹�� ������ ��ġ ������Ʈ
                nextPosition = potentialPosition;
                playerClone.transform.position = nextPosition;
            }
            else
            {
                // �浹�� ������ �ش� ���������� �̵� ����
                moveDirection = Vector3.zero;
            }

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ư�� �ɷ� ���� �� ��ü ��ġ�� �н� ��ġ�� ������Ʈ + �н� ��ġ�� ������Ʈ
        PlayerStat.Instance.currentPosition.position = playerClone.transform.position;

        // �н� ��Ȱ��ȭ
        playerClone.SetActive(false);

        // ��ü Ȱ��ȭ
        // �н� Ȱ��ȭ�� ���� �� PlayerMovement ��ũ��Ʈ �ٽ� Ȱ��ȭ
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // ���� ���� ����
        _isBlinking = false;
        PlayerStat.Instance.abilityCooldownUI.StartCooldown(); // ��Ÿ�� ����
        GameManager.Instance.isAbilitySlowMotion = false;
        
        yield return null;
    }

    // ������ �浹�� Ȯ���ϴ� �޼���
    private bool IsCollidingWithWall(Vector3 currentPosition, Vector3 direction)
    {
        float distance = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, distance, wallLayerMask);
        return hit.collider != null;
    }
}
