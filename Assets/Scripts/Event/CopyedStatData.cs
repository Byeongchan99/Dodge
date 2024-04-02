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
        turretSpawnerDatas = new List<StatData.TurretSpawnerData>(data.turretSpawnerDatas);

        // TurretData 리스트 복사
        turretDatas = new List<StatData.TurretData>(data.turretDatas);

        // ProjectileData 리스트 복사
        projectileDatas = new List<StatData.ProjectileData>(data.projectileDatas);
    }
}
