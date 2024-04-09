using System.Diagnostics;
using TMPro;
using UnityEngine;

public interface IPlayerAbility
{
    void Execute();
}

public class DefenseProtocol: IPlayerAbility
{
    public void Execute()
    {
        // 방어 프로토콜 구현
        // 일정 시간 동안 무적 및 행동 불가
    }
}