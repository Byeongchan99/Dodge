using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public void EraseAll()
    {
        // ��� �ͷ� ���� (Ǯ ��ȯ)
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            BaseTurret baseTurret = turret.GetComponent<BaseTurret>();
            if (baseTurret != null)
            {
                baseTurret.DisableTurret(); // �ͷ� ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else
            {
                Destroy(turret);
            }
        }

        // ��� �߻�ü ���� (Ǯ ��ȯ)
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles)
        {
            BaseProjectile baseProjectile = projectile.GetComponent<BaseProjectile>();
            if (baseProjectile != null)
            {
                baseProjectile.DestroyProjectile(); // ����ü ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else
            {
                Destroy(projectile);
            }
        }

        /*
        // �÷��̾� ��Ȱ��ȭ
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }
        */

        // �� ����
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        if (map != null)
        {
            Destroy(map);
        }

        // ��� ���� ���� ����Ʈ ���� (Ǯ ��ȯ)
        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject effect in effects)
        {
            BaseEffect baseEffect = effect.GetComponent<BaseEffect>();
            if (baseEffect != null)
            {
                baseEffect.DestroyEffect(); // ���� ���� ����Ʈ ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else
            {
                Destroy(effect);
            }
        }
    }
}
