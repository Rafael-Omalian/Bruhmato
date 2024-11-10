using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Attributs internes du projectile
    private Vector3 startPosition;
    private Rigidbody rb;
    [SerializeField]private float speed = 10f;

    // Attributs hérités du projectile
    private bool isExplosive = false;
    private bool isPoison = false;
    private float damage;
    private float reach;
    private int bounce;
    private int pierce;
    
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
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // Deplacement du projectile
        rb.MovePosition(rb.position + (transform.forward * speed * Time.deltaTime));
    }

// Gestion des collisions
    void OnTriggerEnter(Collider col)
    {
        // Detruit le projectile lorsqu'il touche un ennemi s'il n'a plus de perforations ou de rebonds
        if (col.gameObject.tag == "Enemy")
        {
            // Appliquer les dommages a l'ennemi ici

            // Destruction du projectile ou rebond
            if (bounce <= 0 && pierce <= 0)
            {
                Destroy(gameObject);
            } else if (pierce > 0) {
                pierce--;
            } else {
                bounce--;
                Bounce(col);
            }
        }
    }

    // Fait rebondir le projectile
    private void Bounce(Collider col)
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
        }
    }

    // Assgine les statistiques du projectile
    public void SetStats(bool weaponIsExplosive, bool weaponIsPoison, float weaponDamage, float weaponReach, int weaponBounce, int weaponPierce)
    {
        isExplosive = weaponIsExplosive;
        isPoison = weaponIsPoison;
        damage = weaponDamage;
        reach = weaponReach;
        bounce = weaponBounce;
        pierce = weaponPierce;

        // Activer le projectile une fois les statistiques assignees
        gameObject.SetActive(true);
    }
}
