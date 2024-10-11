using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public Transform turretContainer;
    public Transform projectileContainer;
    public Transform itemContainer;
    public Transform effectContainer;
    public Transform ghostEffectContainer;

    public void EraseAll()
    {
        // 모든 터렛 반환
        foreach (Transform child in turretContainer)
        {
            BaseTurret baseTurret = child.GetComponent<BaseTurret>();
            if (baseTurret != null)
            {
                baseTurret.DisableTurret(); // 터렛 비활성화 및 풀 반환 로직
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // 모든 발사체 반환
        foreach (Transform child in projectileContainer)
        {
            BaseProjectile baseProjectile = child.GetComponent<BaseProjectile>();
            if (baseProjectile != null)
            {
                baseProjectile.DestroyProjectile(); // 발사체 비활성화 및 풀 반환 로직
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // 모든 아이템 반환
        foreach (Transform child in itemContainer)
        {
            BaseItem baseItem = child.GetComponent<BaseItem>();
            if (baseItem != null)
            {
                baseItem.DisableItem(); // 아이템 비활성화 및 풀 반환 로직
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // 모든 이펙트 반환
        foreach (Transform child in effectContainer)
        {
            BaseEffect baseEffect = child.GetComponent<BaseEffect>();
            if (baseEffect != null)
            {
                baseEffect.DestroyEffect(); // 이펙트 비활성화 및 풀 반환 로직
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // 잔상은 파괴
        foreach (Transform child in ghostEffectContainer)
        {
            Destroy(child.gameObject);
        }

        /*
        // 모든 터렛 제거(풀 반환)
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

        // 모든 발사체 제거(풀 반환) - 발사체의 경우 레이어로 검색(박격포탄의 경우 태그가 없음)
        GameObject[] projectiles = FindObjectsOfType<GameObject>();
        foreach (GameObject projectile in projectiles)
        {
            if (projectile.layer == LayerMask.NameToLayer("Projectile"))
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
        }      

        // 모든 아이템 제거(풀 반환)
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            BaseItem baseItem = item.GetComponent<BaseItem>();
            if (baseItem != null)
            {
                baseItem.DisableItem(); // 아이템 비활성화 및 풀 반환 로직
            }
            else
            {
                Destroy(item);
            }
        }

        // 플레이어 비활성화
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }

        // 맵 제거
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        if (map != null)
        {
            Destroy(map);
        }

        // 모든 위험 범위 이펙트 제거(풀 반환)
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
        */
    }
}
