using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatData : ScriptableObject
{
    [System.Serializable]
    public class TurretSpawnerData
    {
        public int spawnLevel; // ��ȯ ����
        public float spawnChance; // ��ȯ Ȯ��
        public float spawnCooldown; // ��ȯ ��Ÿ��
        public float spawnCooldownPercent; // ���ҽ�ų ��ȯ ��Ÿ�� �ۼ�Ʈ
    }

    [System.Serializable]
    public class TurretData
    {
        public int projectileCount; // ����ü �߻� ����
        public int projectileIndex; // ���� ����ü �ε���
        public float turretLifeTime; // �ͷ� ���� �ð�
    }

    [System.Serializable]
    public class ProjectileData
    {
        public float projectileSpeed; // ����ü �ӵ�
        public float projectileLifeTime; // ����ü ���� �ð�
    }

    public List<TurretSpawnerData> turretSpawnerDatas;
    public List<TurretData> turretDatas;
    public List<ProjectileData> projectileDatas;
}
