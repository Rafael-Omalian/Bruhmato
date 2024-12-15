using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Attributs internes du projectile
    private Vector3 startPosition;
    private Rigidbody rb;
    [SerializeField]private float speed = 10f;

    // Attributs herites de l'arme
    private bool isExplosive = false;
    private bool isPoison = false;
    private bool bounceIsRandom = false;
    private float damage;
    private float reach;
    private int bounce;
    private int pierce;
    private float range;
    
// Initialisation
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    
// Gestion des deplacements
    void Update()
    {
        // Detruit le projectile lorsqu'il depasse la portee maximum
        if (Vector3.Distance(transform.position, startPosition) >= reach)
        {
            // Fait exploser le projectile lorsqu'il arrive a la portee maximum
            if (isExplosive)
            {
                Explode();
                startPosition = transform.position; // Pour eviter l'appel a la fonction Explode() plusieurs fois
            } else {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        // Deplacement du projectile
        if (rb != null) {rb.MovePosition(rb.position + (transform.forward * speed * Time.deltaTime));}
    }

// Gestion des collisions
    void OnTriggerEnter(Collider col)
    {
        // Detruit le projectile lorsqu'il touche un ennemi s'il n'a plus de perforations ou de rebonds
        if (col.gameObject.tag == "Enemy")
        {
            // Applique les dommages a l'ennemi
            Ennemis enemy = col.GetComponent<Ennemis>();
            enemy.TakesDamage(damage);

            // Applique le poison
            if (isPoison) {enemy.Poison();}

            // Destruction du projectile ou rebond
            if (bounce <= 0 && pierce <= 0)
            {
                // Fait exploser le projectile
                if (isExplosive)
                {
                    Explode();
                }
                else
                {
                    // Destruction du script pour eviter d'autres degats
                    Destroy(this);
                    // Destruction du projectile un peu apres
                    Destroy(gameObject, 0.1f);
                }
            } else if (pierce > 0) {
                pierce--;
            } else {
                bounce--;
                if (bounceIsRandom) {RandomBounce(col);} else {NearestBounce(col);}
            }
        }
    }

    // Fait rebondir le projectile sur l'ennemi le plus proche
    private void NearestBounce(Collider col)
    {
        List<Collider> enemies = Physics.OverlapSphere(transform.position, reach, 1 << 3).ToList();

        // Supprime l'ennemi qui a provoque le rebond de la liste des cibles
        if (enemies.Contains(col)) {enemies.Remove(col);}

        // S'il y a une cible, faire rebondir le projectile
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

            // Change la direction du projectile et sa position de reference pour sa portee
            transform.LookAt(new Vector3(nearestEnemy.transform.position.x, transform.position.y, nearestEnemy.transform.position.z));
            startPosition = transform.position;
        } else {
            // Change la direction du projectile au hasard s'il n'y a pas de cible valide
            transform.rotation = transform.rotation * Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
    }

    // Fait rebondir le projectile sur un ennemi aléatoire
    private void RandomBounce(Collider col)
    {
        List<Collider> enemies = Physics.OverlapSphere(transform.position, reach, 1 << 3).ToList();

        // Supprime l'ennemi qui a provoque le rebond de la liste des cibles
        if (enemies.Contains(col)) {enemies.Remove(col);}

        // S'il y a une cible, faire rebondir le projectile
        if (enemies.Count > 0)
        {
            // Initialise l'ennemi au hasard
            Collider targetedEnemy = enemies[Random.Range(0, enemies.Count)];

            // Change la direction du projectile et sa position de reference pour sa portee
            transform.LookAt(new Vector3(targetedEnemy.transform.position.x, transform.position.y, targetedEnemy.transform.position.z));
            startPosition = transform.position;
        } else {
            // Change la direction du projectile au hasard s'il n'y a pas de cible valide
            transform.rotation = transform.rotation * Quaternion.Euler(0, Random.Range(0, 360), 0);
        }
    }

    // Fait exploser le projectile
    private void Explode()
    {
        transform.localScale = new Vector3(range*10, transform.localScale.y, range*10);
        rb = null;
        Destroy(gameObject, 0.1f);
    }

    // Assgine les statistiques du projectile
    public void SetStats(bool weaponIsExplosive, bool weaponIsPoison, bool weaponIsRandom, float weaponDamage, float weaponReach, int weaponBounce, int weaponPierce, float explosionRange = 2f)
    {
        isExplosive = weaponIsExplosive;
        isPoison = weaponIsPoison;
        bounceIsRandom = weaponIsRandom;
        damage = weaponDamage;
        reach = weaponReach;
        bounce = weaponBounce;
        pierce = weaponPierce;
        range = explosionRange;
    }
}
