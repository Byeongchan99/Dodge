using System.Numerics;
using UnityEngine.UIElements.Experimental;

[System.Serializable]
public class TurretUpgradeInfo
{
    public enum TurretType // 터렛 종류
    {
        Bullet, // 총알
        Laser, // 레이저
        Rocket, // 로켓
        Mortar // 박격포
    }
    public TurretType turretType;

    public enum EnhancementType // 터렛 강화 종류
    {
        CountChange, // 발사체 개수 변경
        SpeedChange, // 발사체 속도 변경
        IsProjectileSplit, // 발사체 분열
        RemainTimeChange, // 발사체 지속시간 증가(레이저 터렛, 로켓 터렛 적용)
        SizeChange, // 크기 변경
        Init, // 초기화  
    }
    public EnhancementType enhancementType;

    public float value; // 강화 값
    public Vector3 projectileSize; // 발사체 크기
}
