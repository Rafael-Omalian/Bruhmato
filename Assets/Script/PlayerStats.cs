using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float atkSpeed = 1.0f;
    private float vie;
    [SerializeField] private float vieMax = 15f;


    private void Start()
    {
        vie = vieMax;
    }
    public float GetAtkSpeed()
    {
        return atkSpeed;
    }

    public void PlayerTakesDamage(float damageAmount)
    {
        vie -= damageAmount;

        if (vie <= 0)
        {
            Debug.Log("Joueur mort");
        }
    }
}
