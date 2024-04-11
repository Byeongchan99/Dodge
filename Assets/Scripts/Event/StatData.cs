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

        // ���� ���縦 ���� ���� ������
        public TurretSpawnerData(TurretSpawnerData source)
        {
            this.spawnLevel = source.spawnLevel;
            this.spawnChance = source.spawnChance;
            this.spawnCooldown = source.spawnCooldown;
            this.spawnCooldownPercent = source.spawnCooldownPercent;
        }
    }

    [System.Serializable]
    public class TurretData
    {
        public int projectileCount; // ����ü �߻� ����
        public int projectileIndex; // ���� ����ü �ε���
        public float turretLifeTime; // �ͷ� ���� �ð�

        // ���� ���縦 ���� ���� ������
        public TurretData(TurretData source)
        {
            this.projectileCount = source.projectileCount;
            this.projectileIndex = source.projectileIndex;
            this.turretLifeTime = source.turretLifeTime;
        }
    }

    [System.Serializable]
    public class ProjectileData
    {
        public float projectileSpeed; // ����ü �ӵ�
        public float projectileLifeTime; // ����ü ���� �ð�

        // ���� ���縦 ���� ���� ������
        public ProjectileData(ProjectileData source)
        {
            this.projectileSpeed = source.projectileSpeed;
            this.projectileLifeTime = source.projectileLifeTime;
        }
    }

    [System.Serializable]
    public class ItemData
    {
        public int spawnLevel; // ��ȯ ����
        public float spawnChance; // ��ȯ Ȯ��
        public float spawnCooldown; // ��ȯ ��Ÿ��
        public float spawnCooldownPercent; // ���ҽ�ų ��ȯ ��Ÿ�� �ۼ�Ʈ

        // ���� ���縦 ���� ���� ������
        public ItemData(ItemData source)
        {
            this.spawnLevel = source.spawnLevel;
            this.spawnChance = source.spawnChance;
            this.spawnCooldown = source.spawnCooldown;
            this.spawnCooldownPercent = source.spawnCooldownPercent;
        }
    }

    public List<TurretSpawnerData> turretSpawnerDatas;
    public List<TurretData> turretDatas;
    public List<ProjectileData> projectileDatas;
    public List<ItemData> itemDatas;
}
