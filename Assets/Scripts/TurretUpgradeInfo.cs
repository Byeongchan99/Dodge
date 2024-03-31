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
        CountIncrease, // 발사체 개수 증가
        SpeedIncrease, // 발사체 속도 증가
        ProjectileSplit, // 발사체 분열
        RemainTimeIncrease, // 발사체 지속시간 증가(레이저 터렛 전용)
        InductionUpgrade, // 유도 성능 증가(박격포 터렛 전용)
        RemoveSplit // 분열 제거
    }
    public EnhancementType enhancementType;
}
