using System.Diagnostics;
using TMPro;
using UnityEngine;

public interface IPlayerAbility
{
    void Execute(PlayerStat stat);
}

public class EMP : IPlayerAbility
{
    public void Execute(PlayerStat stat)
    {
        // EMP 구현
        // 근처의 투사체 무력화
    }
}

public class DefenseProtocol: IPlayerAbility
{
    public void Execute(PlayerStat stat)
    {
        // 방어 프로토콜 구현
        // 일정 시간 동안 무적 및 행동 불가
    }
}