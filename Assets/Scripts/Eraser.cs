using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public void EraseAll()
    {
        // 모든 터렛 제거 (풀 반환)
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            BaseTurret baseTurret = turret.GetComponent<BaseTurret>();
            if (baseTurret != null)
            {
                baseTurret.DisableTurret(); // 터렛 비활성화 및 풀 반환 로직
            }
            else
            {
                Destroy(turret);
            }
        }

        // 모든 발사체 제거 (풀 반환)
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles)
        {
            BaseProjectile baseProjectile = projectile.GetComponent<BaseProjectile>();
            if (baseProjectile != null)
            {
                baseProjectile.DestroyProjectile(); // 투사체 비활성화 및 풀 반환 로직
            }
            else
            {
                Destroy(projectile);
            }
        }

        /*
        // 플레이어 비활성화
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }
        */

        // 맵 제거
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        if (map != null)
        {
            Destroy(map);
        }

        // 모든 위험 범위 이펙트 제거 (풀 반환)
        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject effect in effects)
        {
            BaseEffect baseEffect = effect.GetComponent<BaseEffect>();
            if (baseEffect != null)
            {
                baseEffect.DestroyEffect(); // 위험 범위 이펙트 비활성화 및 풀 반환 로직
            }
            else
            {
                Destroy(effect);
            }
        }
    }
}
