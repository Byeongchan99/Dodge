using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyedStatData
{
    public List<StatData.TurretSpawnerData> turretSpawnerDatas;
    public List<StatData.TurretData> turretDatas;
    public List<StatData.ProjectileData> projectileDatas;

    /// <summary> ���� ������ </summary>
    public CopyedStatData(StatData data)
    {
        // TurretSpawnerData ����Ʈ ����
        turretSpawnerDatas = data.turretSpawnerDatas.ConvertAll(item => new StatData.TurretSpawnerData(item));
        // TurretData ����Ʈ ����
        turretDatas = data.turretDatas.ConvertAll(item => new StatData.TurretData(item));
        // ProjectileData ����Ʈ ����
        projectileDatas = data.projectileDatas.ConvertAll(item => new StatData.ProjectileData(item));
    }
}
