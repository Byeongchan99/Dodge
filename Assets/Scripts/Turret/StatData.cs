using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatData : ScriptableObject
{
    [System.Serializable]
    public struct TurretSpawnerData
    {
        public int spawnLevel; // ��ȯ ����
        public float spawnChance; // ��ȯ Ȯ��
        public float spawnCooldownPercent; // ��ȯ ��Ÿ�� �ۼ�Ʈ
    }

    [System.Serializable]
    public struct TurretData
    {
        public int projectileCount; // ����ü �߻� ����
        public float turretLifeTime; // �ͷ� ���� �ð�
    }

    [System.Serializable]
    public struct ProjectileData
    {
        public float projectileSpeed; // ����ü �ӵ�
        public float projectileLifeTime; // ����ü ���� �ð�
    }

    public List<TurretSpawnerData> turretSpawnerDatas;
    public List<TurretData> turretDatas;
    public List<ProjectileData> projectileDatas;
}
