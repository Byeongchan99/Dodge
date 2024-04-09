using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // �н� ������
    private GameObject playerClone; // ���ӿ� ������ �н� ��ü

    [SerializeField] private float _blinkMoveSpeed = 5.0f; // ��ũ ���� �ð� ���� �̵� �ӵ�
    [SerializeField] private float _slowDownFactor = 0.05f; // �ð��� ������ �ϴ� ���
    [SerializeField] private float _blinkDuration = 0.5f; // ���� ���� �ð�
    [SerializeField] private float _cooldownTime = 5f; // ��Ÿ�� 5��
    private float _nextAbilityTime = 0f; // ���� �ɷ� ��� ���� �ð�

    private bool isBlinking = false;

    public void Execute()
    {
        if (!isBlinking && Time.time >= _nextAbilityTime)
        {
            StartCoroutine(BlinkRoutine());
            _nextAbilityTime = Time.time + _cooldownTime; // ���� ��� ���� �ð� ������Ʈ
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
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // ��ġ ����
        Vector3 startPosition = PlayerStat.Instance.currentPosition.position;
        Vector3 nextPosition = startPosition;

        float elapsedTime = 0f;
        // Ư�� �ɷ� ���� �ð����� �н� ����
        while (elapsedTime < _blinkDuration)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                nextPosition += Vector3.up * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                nextPosition += Vector3.down * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                nextPosition += Vector3.left * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                nextPosition += Vector3.right * _blinkMoveSpeed * Time.unscaledDeltaTime;
            }

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

        // �ð� ����ȭ
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        // ���� ���� ����
        isBlinking = false;
    }
}
