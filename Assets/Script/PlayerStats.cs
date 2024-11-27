using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float atkSpeed = 1.0f;
<<<<<<< Updated upstream
    [SerializeField] protected float vie;
    [SerializeField] protected float vieMax = 15f;


    private void Start()
    {
        vie = vieMax;
=======
    private float vie;
    [SerializeField] private float vieMax = 15f;


    private void Start()
    {
        vie = vieMax;
>>>>>>> Stashed changes
    }
    public float GetAtkSpeed()
    {
        return atkSpeed;
    }

<<<<<<< Updated upstream
    public void PlayerTakesDamage(float damageAmount)
    {
        vie -= damageAmount;

        if (vie <= 0)
        {
            Debug.Log("Joueur mort");
=======
    public void PlayerTakesDamage(float damageAmount)
    {
        vie -= damageAmount;

        if (vie <= 0)
        {
            Debug.Log("Joueur mort");
>>>>>>> Stashed changes
        }
    }
}
