using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] projectiles = new GameObject[4];
    private PlayerStats pStats;
    private List<Weapons> allWeapons = new List<Weapons>();

    void Start()
    {
        pStats = GetComponent<PlayerStats>();
        allWeapons = GetComponents<Weapons>().ToList();
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
}
