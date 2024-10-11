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
        // ��� �ͷ� ��ȯ
        foreach (Transform child in turretContainer)
        {
            BaseTurret baseTurret = child.GetComponent<BaseTurret>();
            if (baseTurret != null)
            {
                baseTurret.DisableTurret(); // �ͷ� ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // ��� �߻�ü ��ȯ
        foreach (Transform child in projectileContainer)
        {
            BaseProjectile baseProjectile = child.GetComponent<BaseProjectile>();
            if (baseProjectile != null)
            {
                baseProjectile.DestroyProjectile(); // �߻�ü ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // ��� ������ ��ȯ
        foreach (Transform child in itemContainer)
        {
            BaseItem baseItem = child.GetComponent<BaseItem>();
            if (baseItem != null)
            {
                baseItem.DisableItem(); // ������ ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // ��� ����Ʈ ��ȯ
        foreach (Transform child in effectContainer)
        {
            BaseEffect baseEffect = child.GetComponent<BaseEffect>();
            if (baseEffect != null)
            {
                baseEffect.DestroyEffect(); // ����Ʈ ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        // �ܻ��� �ı�
        foreach (Transform child in ghostEffectContainer)
        {
            Destroy(child.gameObject);
        }

        /*
        // ��� �ͷ� ����(Ǯ ��ȯ)
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

        // ��� �߻�ü ����(Ǯ ��ȯ) - �߻�ü�� ��� ���̾�� �˻�(�ڰ���ź�� ��� �±װ� ����)
        GameObject[] projectiles = FindObjectsOfType<GameObject>();
        foreach (GameObject projectile in projectiles)
        {
            if (projectile.layer == LayerMask.NameToLayer("Projectile"))
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
        }      

        // ��� ������ ����(Ǯ ��ȯ)
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            BaseItem baseItem = item.GetComponent<BaseItem>();
            if (baseItem != null)
            {
                baseItem.DisableItem(); // ������ ��Ȱ��ȭ �� Ǯ ��ȯ ����
            }
            else
            {
                Destroy(item);
            }
        }

        // �÷��̾� ��Ȱ��ȭ
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }

        // �� ����
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        if (map != null)
        {
            Destroy(map);
        }

        // ��� ���� ���� ����Ʈ ����(Ǯ ��ȯ)
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
        */
    }
}
