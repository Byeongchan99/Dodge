using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "ScriptableObjects/StatData", order = 1)]
public class StatData : ScriptableObject
{
    [System.Serializable]
    public struct TurretSpawnerData
    {
        public int spawnLevel; // 소환 레벨
        public float spawnChance; // 소환 확률
        public float spawnCooldownPercent; // 소환 쿨타임 퍼센트
    }

    [System.Serializable]
    public struct TurretData
    {
        public int projectileCount; // 투사체 발사 개수
        public float turretLifeTime; // 터렛 유지 시간
    }

    [System.Serializable]
    public struct ProjectileData
    {
        public float projectileSpeed; // 투사체 속도
        public float projectileLifeTime; // 투사체 유지 시간
    }

    public List<TurretSpawnerData> turretSpawnerDatas;
    public List<TurretData> turretDatas;
    public List<ProjectileData> projectileDatas;
}
