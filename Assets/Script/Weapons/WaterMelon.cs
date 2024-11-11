using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMelon : Weapons
{
    // Attributs propre a cette arme
    [SerializeField] private float baseRange = 2f;

    public override void Attack()
    {
        switch (weaponLevel)
        {
            case 2: // Niveau 2
                CreateProjectile(baseRange * 2);

                break;
            case 3: // Niveau 3
                CreateProjectile(baseRange * 3);
                break;
            default: // Niveau 1
                CreateProjectile(baseRange);
                break;
        }
    }

    private void CreateProjectile(float range)
    {
        ProjectileScript projScript = Instantiate(projectile, transform.position, transform.rotation).GetComponent<ProjectileScript>();
        projScript.SetStats(isExplosive, isPoison, bounceIsRandom, baseDamage, baseReach, bounce, pierce, range);

        // Change la direction du projectile pour cibler l'ennemi le plus proche
        Collider target = GetBiggestPack(range);
        if (target != null)
        {
            Transform nearestEnemy = target.transform;
            projScript.transform.LookAt(new Vector3(nearestEnemy.position.x, projScript.transform.position.y, nearestEnemy.position.z));
        }
    }
}
