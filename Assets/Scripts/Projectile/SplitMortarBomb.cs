using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitMortarBomb : MortarBomb
{
    [SerializeField] string _mortarBombPoolName;

    // �п��ϴ� 4���� ����
    Vector3[] directions = {
        new Vector3(1, 0, 0).normalized,
        new Vector3(-1, 0, 0).normalized,
        new Vector3(0, 1, 0).normalized,
        new Vector3(0, -1, 0).normalized
    };

    /// <summary> �п� �ڰ���ź ������ �ڷ�ƾ </summary>
    protected override IEnumerator Flight()
    {
        float time = 0.0f;
        Vector3 start = transform.position;
        Vector3 end = transform.position + (Vector3)moveDirection;

        // �ִϸ��̼� Ŀ�긦 �̿��� ������ ����
        while (time < flightDuration)
        {
            time += Time.deltaTime;
            float linearT = time / flightDuration;
            float heightT = heightCurve.Evaluate(linearT); // �ִϸ��̼� Ŀ�� �� ����

            float height = Mathf.Lerp(0.0f, hoverHeight, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height); // ��ġ ����

            yield return null;
        }
     
        // �п��� �ڰ���ź�� ��� ũ�Ⱑ �� ����
        Vector3 mortarBombSize = transform.localScale / 2;
        Vector3 effectSize = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize / 2;

        Debug.Log("�п� ����");
        for (int i = 0; i < 4; i++)
        {
            SplitedMortarBomb splitedMortarBomb = ProjectilePoolManager.Instance.Get(_mortarBombPoolName) as SplitedMortarBomb;
            SplitedMortarBombEffect splitedBombEffect = EffectPoolManager.Instance.Get("SplitedMortarBombEffect") as SplitedMortarBombEffect;

            if (splitedMortarBomb != null)
            {
                splitedMortarBomb.transform.position = transform.position; // ���� ��ġ ����        
                splitedMortarBomb.transform.localScale = mortarBombSize; // ũ�� ����
                splitedMortarBomb.setMortarBomb(flightDuration / 2, hoverHeight / 4); // �ڰ���ź ���� ����
                splitedMortarBomb.SetBombEffect(splitedBombEffect); // ����Ʈ ���� ����
                
                splitedMortarBomb.gameObject.SetActive(true); // Ȱ��ȭ
                splitedMortarBomb.SetDirection(directions[i]); // �п� ���� ����(�ڰ���ź�� ��� Move() �޼��尡 �ڷ�ƾ�̶� Ȱ��ȭ �� ����)
            }
            else
            {
                Debug.LogWarning("Failed to get mortar bomb from pool.");
            }
           
            if (splitedBombEffect != null)
            {
                splitedBombEffect.transform.position = transform.position + directions[i]; // ��ġ ���� (������ �Ÿ� ����)
                splitedBombEffect.transform.localScale = effectSize; // ũ�� ����
                splitedBombEffect.gameObject.SetActive(true); // Ȱ��ȭ
            }
            else
            {
                Debug.LogWarning("Failed to get bomb effect from pool.");
            }
        }

        DestroyProjectile();
    }
}
