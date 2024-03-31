using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretData", order = 1)]
public class TurretData : ScriptableObject
{
    [System.Serializable]
    public struct TurretDataInfo
    {
        public int spawnLevel; // 소환 레벨
        public float spawnCooldownPercent; // 소환 쿨타임 퍼센트
    }

    public List<TurretDataInfo> turretDatas; // 상황에 따른 터렛 종류별 데이터 리스트
}
