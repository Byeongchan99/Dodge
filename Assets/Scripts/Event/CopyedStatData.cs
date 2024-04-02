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
        turretSpawnerDatas = new List<StatData.TurretSpawnerData>(data.turretSpawnerDatas);

        // TurretData ����Ʈ ����
        turretDatas = new List<StatData.TurretData>(data.turretDatas);

        // ProjectileData ����Ʈ ����
        projectileDatas = new List<StatData.ProjectileData>(data.projectileDatas);
    }
}
