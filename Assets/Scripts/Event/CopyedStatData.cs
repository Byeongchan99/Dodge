using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyedStatData
{
    public List<StatData.TurretSpawnerData> turretSpawnerDatas;
    public List<StatData.TurretData> turretDatas;
    public List<StatData.ProjectileData> projectileDatas;

    /// <summary> 복사 생성자 </summary>
    public CopyedStatData(StatData data)
    {
        // TurretSpawnerData 리스트 복사
        turretSpawnerDatas = data.turretSpawnerDatas.ConvertAll(item => new StatData.TurretSpawnerData(item));
        // TurretData 리스트 복사
        turretDatas = data.turretDatas.ConvertAll(item => new StatData.TurretData(item));
        // ProjectileData 리스트 복사
        projectileDatas = data.projectileDatas.ConvertAll(item => new StatData.ProjectileData(item));
    }
}
