using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    // Attributs de base de l'arme
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected string weaponFileName;
    [SerializeField] protected int weaponLevel = 1;
    [SerializeField] protected float baseReloadTime;
    [SerializeField] protected bool isExplosive = false;
    [SerializeField] protected bool isPoison = false;
    [SerializeField] protected bool bounceIsRandom = false;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected float baseReach;
    [SerializeField] protected int bounce;
    [SerializeField] protected int pierce;

    // Attributs de gestion
    protected float reloadTime;
    protected WeaponManager weaponManager;
    protected PlayerStats pStats;
    
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        projectile = Resources.Load("Projectiles/" + weaponFileName) as GameObject;
    }

    // Assigne les statistiques de l'arme
    public void SetBaseStats(WeaponSO newWeapon)
    {
        weaponFileName = newWeapon.weaponFileName;
        baseReloadTime = newWeapon.baseReloadTime;
        isExplosive = newWeapon.isExplosive;
        isPoison = newWeapon.isPoison;
        bounceIsRandom = newWeapon.bounceIsRandom;
        baseDamage = newWeapon.baseDamage;
        baseReach = newWeapon.baseReach;
        bounce = newWeapon.bounce;
        pierce = newWeapon.pierce;
        
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
        reloadTime = baseReloadTime / atkSpeed;
    }

    // Assigne les statistiques du joueur
    public virtual void SetPlayerStats(PlayerStats pStats)
    {
        this.pStats = pStats;
    }

    // Augmente le niveau de l'arme
    public virtual void LevelUp()
    {
        weaponLevel ++;
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

    // Assigne une cible aleatoire au projectile
    protected virtual Collider GetRandomTarget(List<Collider> enemiesToIgnore = null)
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
            // Initialise l'ennemi au hasard
            Collider targetedEnemy = enemies[Random.Range(0, enemies.Count)];

            return targetedEnemy;
        }
        
        // Si aucun ennemi ne peut etre cible, retourne null
        return null;
    }

    // Assigne comme cible l'ennemi entoure par le plus d'autres ennemis
    protected virtual Collider GetBiggestPack(float range)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, baseReach, 1 << 3);

        if (enemies.Length > 0)
        {
            Collider targetedEnemy = enemies[0];
            int count = Physics.OverlapSphere(enemies[0].transform.position, range, 1 << 3).Length;

            for (int i = 1; i < enemies.Length; i++)
            {
                int tempCount = Physics.OverlapSphere(enemies[i].transform.position, range, 1 << 3).Length;

                if (tempCount > count)
                {
                    count = tempCount;
                    targetedEnemy = enemies[i];
                }                
            }

            return targetedEnemy;
        }
        
        // Si aucun ennemi ne peut etre cible, retourne null
        return null;
    }
}
