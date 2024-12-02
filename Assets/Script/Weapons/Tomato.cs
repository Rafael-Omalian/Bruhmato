using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Tomato : Weapons
{
    public override void Attack()
    {
        switch (weaponLevel)
        {
            case 2: // Niveau 2
                CreateProjectile(transform.rotation);
                CreateProjectile(transform.rotation * Quaternion.Euler(0, 180, 0));
                break;
            case 3: // Niveau 3
                CreateProjectile(transform.rotation);
                CreateProjectile(transform.rotation * Quaternion.Euler(0, 90, 0));
                CreateProjectile(transform.rotation * Quaternion.Euler(0, 180, 0));
                CreateProjectile(transform.rotation * Quaternion.Euler(0, -90, 0));
                break;
            default: // Niveau 1
                CreateProjectile(transform.rotation);
                break;
        }
        
    }

    private void CreateProjectile(Quaternion projRotation)
    {
        ProjectileScript projScript = Instantiate(projectile, transform.position, projRotation).GetComponent<ProjectileScript>();
        projScript.SetStats(isExplosive, isPoison, bounceIsRandom, baseDamage + pStats.GetDamage(), baseReach + pStats.GetReach(), bounce, pierce);
    }
}
