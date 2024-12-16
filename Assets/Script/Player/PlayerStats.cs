using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float atkSpeed = 1.0f;
    private float vie;
    [SerializeField] private float vieMax = 15f;
    [SerializeField] private float damage = 0f;
    [SerializeField] private float reach = 0f;


    private void Start()
    {
        // Recuperation des statistiques du joueur
        vieMax = GameState.player.vieMax;
        atkSpeed = GameState.player.atkSpeed;
        damage = GameState.player.damage;
        reach = GameState.player.reach;

        switch (GameState.player.name)
        {
            case "Monsieur":
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case "Madame":
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                break;
        }

        vie = vieMax;
    }

// Ensemble des methodes Get
    public float GetAtkSpeed()
    {
        return atkSpeed;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetReach()
    {
        return reach;
    }

// Gestion de la prise de dommage du personnage
    public void PlayerTakesDamage(float damageAmount)
    {
        vie -= damageAmount;

        if (vie <= 0)
        {
            Debug.Log("Joueur mort");
        }
    }

// Augmentation des statistiques
    public void StatUpgrade(Upgrade upgrade)
    {
        atkSpeed += upgrade.atkSpeed;
        vie += upgrade.vie;
        vieMax += upgrade.vieMax;
        damage += upgrade.damage;
        reach += upgrade.reach;
    }
}
