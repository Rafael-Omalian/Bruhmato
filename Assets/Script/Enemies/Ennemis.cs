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
    protected bool isPoisoned;
    protected int poisonCount;

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
        vie = vieMax * GameState.enemyHealthMult;
        if(transform.childCount > 2) {zoneAttaque = transform.GetChild(2).gameObject;}
    }

    public abstract void Attack(); //methode d'attaque
    public abstract void Mouvement();

    //Prise de dommage
    public virtual void TakesDamage(float baseDamage)
    {
        vie -= baseDamage;

        if (vie <= 0)
        {
            Destroy(GetComponent<Collider>());

            // Si poison, il se repend aux ennemis autour
            if (isPoisoned)
            {
                List<Collider> otherEnemies = Physics.OverlapSphere(transform.position, 3.0f, 1 << 3).ToList();
                foreach (Collider col in otherEnemies)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        col.GetComponent<Ennemis>().Poison();
                    }
                }
            }

            Destroy(this.gameObject);
            GenerateExp();
        }
    }

    //Generation de l'experience
    public virtual void GenerateExp()
    {
        if (experienceValue > 0)
        {
            ExperienceManager expManager = Instantiate(experience, new Vector3 (transform.position.x, 0.3f, transform.position.z), Quaternion.identity).GetComponent<ExperienceManager>(); //generation de l'experience
            expManager.SetExp(experienceValue);
        }
    }

    //Gestion du poison
    public virtual void Poison(float baseDamage = 0.0f)
    {
        if (poisonCount == 0 && isPoisoned == false)
        {
            isPoisoned = true;
            transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;
            poisonCount += 2;
            StartCoroutine(ApplyPoison());
        }
        else
        {
            poisonCount += 2;
        }

        if (baseDamage  != 0) {TakesDamage(baseDamage);}
    }

    //Application du poison toutes les secondes
    protected IEnumerator ApplyPoison()
    {
        yield return new WaitForSeconds(1);
        TakesDamage(poisonCount);
        poisonCount--;
        if (poisonCount == 0)
        {
            isPoisoned = false;
            transform.GetChild(1).GetComponent<Renderer>().material.color = Color.white;
        } else {
            StartCoroutine(ApplyPoison());
        }
    }
}
