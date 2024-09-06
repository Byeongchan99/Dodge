using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatData : ScriptableObject
{
    // ���̽� ������ ������
    [System.Serializable]
    public class BaseSpawnData
    {
        public string name; // �̸�
        public int spawnLevel; // ��ȯ ����
        public float spawnChance; // ��ȯ Ȯ��
        public float spawnCooldown; // ��ȯ ��Ÿ��
        public float spawnCooldownPercent; // ���ҽ�ų ��ȯ ��Ÿ�� �ۼ�Ʈ

        public BaseSpawnData(BaseSpawnData source)
        {
            this.name = source.name;
            this.spawnLevel = source.spawnLevel;
            this.spawnChance = source.spawnChance;
            this.spawnCooldown = source.spawnCooldown;
            this.spawnCooldownPercent = source.spawnCooldownPercent;
        }
    }

    // ������ ������ ������
    [System.Serializable]
    public class ItemData : BaseSpawnData
    {
        public ItemData(ItemData source) : base(source) { }
    }

    // �ͷ� ������ ������
    [System.Serializable]
    public class TurretSpawnerData : BaseSpawnData
    {
        public TurretSpawnerData(TurretSpawnerData source) : base(source) { }
    }

    // �ͷ� ������
    [System.Serializable]
    public class TurretData
    {
        public string turretName; // �ͷ� �̸�
        public int projectileCount; // ����ü �߻� ����
        public bool isMaxProjectileCount; // �ִ� ����ü �߻� ��������
        public int projectileIndex; // ���� ����ü �ε���
        public float turretLifeTime; // �ͷ� ���� �ð�

        // ���� ���縦 ���� ���� ������
        public TurretData(TurretData source)
        {
            this.turretName = source.turretName;
            this.projectileCount = source.projectileCount;
            this.isMaxProjectileCount = source.isMaxProjectileCount;
            this.projectileIndex = source.projectileIndex;
            this.turretLifeTime = source.turretLifeTime;
        }
    }

    // ����ü ������
    [System.Serializable]
    public class ProjectileData
    {
        public string projectileName; // ����ü �̸�
        public float projectileSpeed; // ����ü �ӵ�
        public bool isMaxProjectileSpeed; // �ִ� ����ü �ӵ�����
        public float projectileLifeTime; // ����ü ���� �ð� - �������� ���Ͽ����� ���
        public bool isMaxProjectileLifeTime; // �ִ� ����ü ���� �ð�����
        public Vector3 projectileSize; // ����ü ũ�� - �ڰ���ź�� ��� ����Ʈ�� ũ��
        public bool isMaxProjectileSize; // �ִ� ����ü ũ������

        // ���� ���縦 ���� ���� ������
        public ProjectileData(ProjectileData source)
        {
            this.projectileName = source.projectileName;
            this.projectileSpeed = source.projectileSpeed;
            this.isMaxProjectileSpeed = source.isMaxProjectileSpeed;
            this.projectileLifeTime = source.projectileLifeTime;
            this.isMaxProjectileLifeTime = source.isMaxProjectileLifeTime;
            this.projectileSize = source.projectileSize;
            this.isMaxProjectileSize = source.isMaxProjectileSize;
        }
    }

    public List<ItemData> itemDatas;
    public List<TurretSpawnerData> turretSpawnerDatas;
    public List<TurretData> turretDatas;
    public List<ProjectileData> projectileDatas;
}
