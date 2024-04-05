using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IPlayerAbility
{
    public GameObject playerClonePrefab; // �н� ������
    private GameObject playerClone; // ���ӿ� ������ �н� ��ü
    public float blinkDistance = 5.0f; // �÷��̾ ������ �� �ִ� �Ÿ�
    public float slowDownFactor = 0.05f; // �ð��� ������ �ϴ� ���
    private bool isBlinking = false;

    public void Execute(PlayerStat stat)
    {
        if (!isBlinking)
        {
            // �ð��� ������ �Ѵ�
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            // �ð�ȭ ���� (�ӽ�: ���� ������ ǥ���ϴ� ���� ������ �׸���)
            ShowBlinkRange(stat.currentPosition.position);

            // �÷��̾ �Է��� ��ġ�� �޾Ƽ� �����Ѵ�
            StartCoroutine(BlinkRoutine(stat));
        }
    }

    // �ڷ�ƾ���� �ɷ� ���� �ð����� �÷��̾� �Է��� �޴´�
    // �ڷ�ƾ�� ����Ǹ� �÷��̾ �Է��� ��ġ�� ��ȯ�Ѵ�
    private IEnumerator BlinkRoutine(PlayerStat stat)
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
            playerClone = Instantiate(playerClonePrefab, stat.currentPosition.position, Quaternion.identity);
        }
        playerClone.SetActive(true);

        // �ð��� ������ �Ѵ�
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        Vector3 startPosition = stat.currentPosition.position;
        Vector3 nextPosition = startPosition;
        float elapsedTime = 0f;
        float maxDuration = 0.5f; // Ư�� �ɷ� �ִ� ���� �ð� (����)

        // Ư�� �ɷ� ���� �ð����� �н� ����
        while (elapsedTime < maxDuration)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                nextPosition += Vector3.up * blinkDistance * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                nextPosition += Vector3.down * blinkDistance * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                nextPosition += Vector3.left * blinkDistance * Time.unscaledDeltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                nextPosition += Vector3.right * blinkDistance * Time.unscaledDeltaTime;
            }

            // �н� ��ġ ������Ʈ
            playerClone.transform.position = nextPosition;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ư�� �ɷ� ���� �� ��ü ��ġ�� �н� ��ġ�� ������Ʈ
        stat.currentPosition.position = playerClone.transform.position;

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

        // ��ũ ���� 
        EndBlink();
    }

    // ��ũ ����
    private void EndBlink()
    {
        // �ð��� �������� �ǵ�����
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        // ���� ���� ǥ�ø� �����Ѵ�
        HideBlinkRange();

        // ���� ���¸� �����Ѵ�
        isBlinking = false;
    }

    // ��ũ ���� ǥ��
    private void ShowBlinkRange(Vector3 position)
    {
        // ���� ������ �ð������� ǥ���ϴ� �ڵ�
    }

    // ��ũ ���� ǥ�� ����
    private void HideBlinkRange()
    {
        // �ð������� ǥ�õ� ���� ������ �����ϴ� �ڵ�
    }
}
