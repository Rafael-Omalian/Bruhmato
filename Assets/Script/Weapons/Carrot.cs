using System.Collections.Generic;
using UnityEngine;

public class Carrot : Weapons
{
    public override void Attack()
    {
        List<Collider> enemiesToIgnore = new List<Collider>();
        switch (weaponLevel)
        {
            case 2: // Niveau 2
                CreateProjectile(enemiesToIgnore);
                CreateProjectile(enemiesToIgnore);
                break;
            case 3: // Niveau 3
                CreateProjectile(enemiesToIgnore);
                CreateProjectile(enemiesToIgnore);
                CreateProjectile(enemiesToIgnore);
                CreateProjectile(enemiesToIgnore);
                break;
            default: // Niveau 1
                CreateProjectile(enemiesToIgnore);
                break;
        }
    }

    private void CreateProjectile(List<Collider> enemiesToIgnore)
    {
        ProjectileScript projScript = Instantiate(projectile, transform.position, transform.rotation).GetComponent<ProjectileScript>();
        projScript.SetStats(isExplosive, isPoison, bounceIsRandom, baseDamage + pStats.GetDamage(), baseReach + pStats.GetReach(), bounce, pierce);

        // Change la direction du projectile pour cibler l'ennemi le plus proche
        Collider target = GetNearestTarget(enemiesToIgnore);
        if (target != null)
        {
            enemiesToIgnore.Add(target);
            Transform nearestEnemy = target.transform;
            projScript.transform.LookAt(new Vector3(nearestEnemy.position.x, projScript.transform.position.y, nearestEnemy.position.z));
        } else {
            // Change la direction du projectile au hasard s'il n'y a pas de cible valide
            projScript.transform.rotation = transform.rotation * Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
    }
}
