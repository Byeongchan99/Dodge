using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour, IPlayerAbility
{
    public GameObject EMPPrefab; // EMP ������
    private GameObject EMPClone; // ���ӿ� ������ EMP ��ü

    [SerializeField] private float _slowDownFactor = 0.05f; // �ð��� ������ �ϴ� ���
    [SerializeField] private float _slowDuration = 0.05f; // ���ο� ���� �ð�
    [SerializeField] private float _EMPDuration = 0.5f; // EMP ���� �ð�
    [SerializeField] private float cooldownTime = 4f; // ��Ÿ�� 5��
    private float _nextAbilityTime = 0f; // ���� �ɷ� ��� ���� �ð�

    private bool isEMP = false;

    public void Execute()
    {
        if (!isEMP && Time.time >= _nextAbilityTime)
        {
            StartCoroutine(EMPRoutine());
            _nextAbilityTime = Time.time + cooldownTime; // ���� ��� ���� �ð� ������Ʈ
        }
    }

    private IEnumerator EMPRoutine()
    {
        isEMP = true;

        // ������ ����
        // PlayerMovement ��ũ��Ʈ ��Ȱ��ȭ
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.rb.velocity = Vector2.zero;
            playerMovement.enabled = false;
        }

        // EMP Ȱ��ȭ
        if (EMPClone == null)
        {
            EMPClone = Instantiate(EMPPrefab, PlayerStat.Instance.currentPosition.position, Quaternion.identity);
        }
        else
        {
            EMPClone.transform.position = PlayerStat.Instance.currentPosition.position; // EMPClone�� �̹� ������ ��ġ�� ������Ʈ
        }
        EMPClone.SetActive(true);

        // �ð��� ������ �Ѵ�
        Time.timeScale = _slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // _slowDuration ���� ���
        yield return new WaitForSecondsRealtime(_slowDuration);

        // �ð� �帧�� ������� ����
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // EMP ���ӽð� ���� ���
        yield return new WaitForSeconds(_EMPDuration - _slowDuration);

        // EMP ��Ȱ��ȭ
        EMPClone.SetActive(false);

        // PlayerMovement ��ũ��Ʈ Ȱ��ȭ
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        isEMP = false;

        yield return null;
    }
}
