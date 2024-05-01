using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // �н� ������
    private GameObject playerClone; // ���ӿ� ������ �н� ��ü

    [SerializeField] private float _blinkMoveSpeed = 5.0f; // ��ũ ���� �ð� ���� �̵� �ӵ�
    [SerializeField] private float _blinkDuration = 0.5f; // ���� ���� �ð�
    [SerializeField] private float _cooldownTime = 3f; // ��Ÿ�� 5��
    private float _nextAbilityTime = 0f; // ���� �ɷ� ��� ���� �ð�

    private bool isBlinking = false;

    public void Execute()
    {
        if (!isBlinking && Time.unscaledTime >= _nextAbilityTime)
        {
            Debug.Log("��ũ ����");
            StartCoroutine(BlinkRoutine());
            Debug.Log("��ũ ����");
            _nextAbilityTime = Time.unscaledTime + _cooldownTime; // ���� ��� ���� �ð� ������Ʈ
        }
    }

    // �ɷ� ���� �ð����� �н� ����
    private IEnumerator BlinkRoutine()
    {
        isBlinking = true;

        // ��ü ��Ȱ��ȭ
        // PlayerMovement ��ũ��Ʈ ��Ȱ��ȭ
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.rb.velocity = Vector2.zero;
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

        // ��ġ ����
        Vector3 startPosition = PlayerStat.Instance.currentPosition.position;
        Vector3 nextPosition = startPosition;

        float elapsedTime = 0f;
        // Ư�� �ɷ� ���� �ð����� �н� ����
        while (elapsedTime < _blinkDuration)
        {
            // �Է¿� ���� ��ġ ���
            float horizontal = Input.GetAxisRaw("Horizontal") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            float vertical = Input.GetAxisRaw("Vertical") * _blinkMoveSpeed * Time.unscaledDeltaTime;
            Vector3 moveDirection = new Vector3(horizontal, vertical, 0);
            nextPosition += moveDirection;

            // �н� ��ġ ������Ʈ
            playerClone.transform.position = nextPosition;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ư�� �ɷ� ���� �� ��ü ��ġ�� �н� ��ġ�� ������Ʈ
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
        isBlinking = false;
        GameManager.Instance.isAbilitySlowMotion = false;
    }
}
