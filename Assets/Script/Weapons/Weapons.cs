using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    // Attributs de base de l'arme
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected int weaponLevel = 0;
    [SerializeField] protected float baseReloadTime;
    [SerializeField] protected bool isExplosive = false;
    [SerializeField] protected bool isPoison = false;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected float baseReach;
    [SerializeField] protected int bounce;
    [SerializeField] protected int pierce;

    // Attributs de gestion
    protected float reloadTime;
    protected WeaponManager weaponManager;
    
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    // Gestion du temps de recharge
    void Update()
    {
        reloadTime -= Time.deltaTime;
        if (reloadTime <= 0)
        {
            weaponManager.NewAttack(this);
        }
    }

    // Fait attaquer l'arme
    public abstract void Attack();

    // Assgine le temps de recharge
    public virtual void SetReloadTime(float atkSpeed)
    {
        reloadTime = baseReloadTime * atkSpeed;
    }

    // Assigne la cible la plus proche au projectile
    protected virtual Collider GetNearestTarget(List<Collider> enemiesToIgnore = null)
    {
        List<Collider> enemies = Physics.OverlapSphere(transform.position, baseReach, 1 << 3).ToList();

        // Supprime certains ennemis de la liste des cibles
        if (enemiesToIgnore != null)
        {
            foreach (Collider col in enemiesToIgnore)
            {
                if (enemies.Contains(col))
                {
                    enemies.Remove(col);
                }
            }
        }

        if (enemies.Count > 0)
        {
            // Initialise l'ennemi le plus proche
            float nearestDistance = Vector3.Distance(transform.position, enemies[0].transform.position);
            Collider nearestEnemy = enemies[0];

            // Verifie s'il n'y pas un enemi plus proche
            for (int i = 1; i < enemies.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (distance <= nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemies[i];
                }
            }

            return nearestEnemy;
        }
        
        // Si aucun ennemi ne peut etre cible, retourne null
        return null;
    }
}
