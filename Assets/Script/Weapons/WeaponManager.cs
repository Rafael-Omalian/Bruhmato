using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private PlayerStats pStats;
    private List<Weapons> allWeapons = new List<Weapons>();
    [SerializeField] GameObject weaponChoice;

    void Start()
    {
        pStats = GetComponent<PlayerStats>();
        allWeapons = GetComponents<Weapons>().ToList();
        foreach (Weapons weapon in allWeapons)
        {
            weapon.SetPlayerStats(pStats);
        }
    }

    public void NewAttack(Weapons weaponScript)
    {
        weaponScript.Attack();
        weaponScript.SetReloadTime(pStats.GetAtkSpeed());
    }

    public void allAttack()
    {
        foreach (Weapons weapon in allWeapons)
        {
            weapon.Attack();
        }
    }

    public void GetWeapon(WeaponSO newWeapon)
    {
        // Si le joueur n'a pas l'arme, l'ajoute
        if (!gameObject.GetComponent(Type.GetType(newWeapon.weaponFileName)))
        {
            gameObject.AddComponent(Type.GetType(newWeapon.weaponFileName));
            allWeapons.Add(gameObject.GetComponent(Type.GetType(newWeapon.weaponFileName)) as Weapons);
            allWeapons[allWeapons.Count - 1].SetBaseStats(newWeapon);
            allWeapons[allWeapons.Count - 1].SetPlayerStats(pStats);
            Debug.Log("Armes ajoutee");
        } else {
            // Si le joueur a deja l'arme, elle s'ameliore
            Weapons newWeaponScript = gameObject.GetComponent(Type.GetType(newWeapon.weaponFileName)) as Weapons;
            newWeaponScript.LevelUp();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Weapon"))
        {
            weaponChoice.SetActive(true);
        }
    }
}
