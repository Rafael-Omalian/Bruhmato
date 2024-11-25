using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Ennemis : MonoBehaviour
{
    protected GameObject attaque;
    [SerializeField] protected string ennemiFileName;
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float baseReach;
    [SerializeField] protected float vie;
    [SerializeField] protected float vieMax;
    [SerializeField] protected float vitesse;
    [SerializeField] private float atkSpeed;

    // Info du joueur
    protected Transform player;

    // Attributs deplacement
    protected Rigidbody rb;
    protected GameObject zoneAttaque;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        vie = vieMax;
        zoneAttaque = transform.GetChild(2).gameObject;
    }

    public abstract void Attack(); //méthode d'attaque
    public abstract void Mouvement();
}
