using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected float baseReloadTime;
    protected float reloadTime;
    [SerializeField] protected float damage;
    [SerializeField] protected float reach;
    protected WeaponManager weaponManager;
    
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        reloadTime -= Time.deltaTime;
        if (reloadTime <= 0)
        {
            weaponManager.NewAttack(this);
        }
    }

    public abstract void Attack();

    public virtual void SetReloadTime(float atkSpeed)
    {
        reloadTime = baseReloadTime * atkSpeed;
    }
}
