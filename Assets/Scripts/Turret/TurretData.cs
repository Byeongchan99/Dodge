using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretData", order = 1)]
public class TurretData : ScriptableObject
{
    [System.Serializable]
    public struct TurretDataInfo
    {
        public int spawnLevel; // ��ȯ ����
        public float spawnCooldownPercent; // ��ȯ ��Ÿ�� �ۼ�Ʈ
    }

    public List<TurretDataInfo> turretDatas; // ��Ȳ�� ���� �ͷ� ������ ������ ����Ʈ
}
