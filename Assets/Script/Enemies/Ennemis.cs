using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Ennemis : MonoBehaviour
{
    protected GameObject attaque;

    [Header("Infos de l'ennemi")]
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float baseReach;
    [SerializeField] protected float vie;
    [SerializeField] protected float vieMax;
    [SerializeField] protected float vitesse;
    [SerializeField] protected float atkSpeed;
    protected bool isAttacking;

    // Info du joueur
    protected Transform player;

    // Attributs deplacement
    protected Rigidbody rb;
    protected GameObject zoneAttaque;

    // Gestion des animations
    protected Animator anim;

    // Generation de l'experience
    [Header("Generation de l'experience")]
    [SerializeField] protected int experienceValue;
    [SerializeField] protected GameObject experience;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        vie = vieMax;
        if(transform.childCount > 2) {zoneAttaque = transform.GetChild(2).gameObject;}
    }

    public abstract void Attack(); //methode d'attaque
    public abstract void Mouvement();

    public virtual void TakesDamage(float baseDamage)
    {
        vie -= baseDamage;

        if (vie <= 0)
        {
            Destroy(this.gameObject);
            GenerateExp();
        }
    }

    public virtual void GenerateExp()
    {
        ExperienceManager expManager = Instantiate(experience, new Vector3 (transform.position.x, 0.3f, transform.position.z), Quaternion.identity).GetComponent<ExperienceManager>(); //generation de l'experience
        expManager.SetExp(experienceValue);
    }
}
