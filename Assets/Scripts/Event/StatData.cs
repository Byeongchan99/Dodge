using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatData : ScriptableObject
{
    [System.Serializable]
    public class BaseSpawnData
    {
        public string name; // 이름
        public int spawnLevel; // 소환 레벨
        public float spawnChance; // 소환 확률
        public float spawnCooldown; // 소환 쿨타임
        public float spawnCooldownPercent; // 감소시킬 소환 쿨타임 퍼센트

        public BaseSpawnData(BaseSpawnData source)
        {
            this.name = source.name;
            this.spawnLevel = source.spawnLevel;
            this.spawnChance = source.spawnChance;
            this.spawnCooldown = source.spawnCooldown;
            this.spawnCooldownPercent = source.spawnCooldownPercent;
        }
    }

    [System.Serializable]
    public class ItemData : BaseSpawnData
    {
        public ItemData(ItemData source) : base(source) { }
    }

    [System.Serializable]
    public class TurretSpawnerData : BaseSpawnData
    {
        public TurretSpawnerData(TurretSpawnerData source) : base(source) { }
    }

    [System.Serializable]
    public class TurretData
    {
        public string turretName; // 터렛 이름
        public int projectileCount; // 투사체 발사 개수
        public int projectileIndex; // 현재 투사체 인덱스
        public float turretLifeTime; // 터렛 유지 시간

        // 깊은 복사를 위한 복사 생성자
        public TurretData(TurretData source)
        {
            this.turretName = source.turretName;
            this.projectileCount = source.projectileCount;
            this.projectileIndex = source.projectileIndex;
            this.turretLifeTime = source.turretLifeTime;
        }
    }

    [System.Serializable]
    public class ProjectileData
    {
        public string projectileName; // 투사체 이름
        public float projectileSpeed; // 투사체 속도
        public float projectileLifeTime; // 투사체 유지 시간
        public Vector3 projectileSize; // 투사체 크기

        // 깊은 복사를 위한 복사 생성자
        public ProjectileData(ProjectileData source)
        {
            this.projectileName = source.projectileName;
            this.projectileSpeed = source.projectileSpeed;
            this.projectileLifeTime = source.projectileLifeTime;
            this.projectileSize = source.projectileSize;
        }
    }

    public List<ItemData> itemDatas;
    public List<TurretSpawnerData> turretSpawnerDatas;
    public List<TurretData> turretDatas;
    public List<ProjectileData> projectileDatas;
}
