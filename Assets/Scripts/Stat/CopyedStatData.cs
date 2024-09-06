using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyedStatData
{
    public List<StatData.TurretSpawnerData> turretSpawnerDatas; // 터렛 스포너 데이터
    public List<StatData.TurretData> turretDatas; // 터렛 데이터
    public List<StatData.ProjectileData> projectileDatas; // 발사체 데이터
    public List<StatData.ItemData> itemDatas; // 아이템 데이터

    /// <summary> 복사 생성자 </summary>
    public CopyedStatData(StatData data)
    {
        // TurretSpawnerData 리스트 복사
        turretSpawnerDatas = data.turretSpawnerDatas.ConvertAll(item => new StatData.TurretSpawnerData(item));
        // TurretData 리스트 복사
        turretDatas = data.turretDatas.ConvertAll(item => new StatData.TurretData(item));
        // ProjectileData 리스트 복사
        projectileDatas = data.projectileDatas.ConvertAll(item => new StatData.ProjectileData(item));
        // ItemData 리스트 복사
        itemDatas = data.itemDatas.ConvertAll(item => new StatData.ItemData(item));
    }
}
