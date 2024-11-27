using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Bulldog : Ennemis
{
    [SerializeField] protected bool isExplosive = false;

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 2.5f)
        {
            if (!isExplosive)
            {
                Attack();
            }
        }
    }
    private void FixedUpdate()
    {
        if (!isExplosive)
        {
            Mouvement();
        }
    }
    public override void Attack()
    {
        isExplosive = true;
        zoneAttaque.SetActive(true);
        Debug.Log("Bulldog explose");
        Destroy(gameObject, 0.2f); //temps -> 0.2

        //Corotine -> plus en plus rouge puis explose
    }

    // Permet de suivre le joueur
    public override void Mouvement()
    {
        vitesse = 1f;
        rb.MovePosition(rb.position + Vector3.Normalize(player.transform.position - rb.position) * vitesse * Time.deltaTime);
        transform.LookAt(new Vector3(player.position.x, rb.position.y, player.position.z));
    }
    public void TakesDamage(float baseDamage)
    {
        vie -= baseDamage;

        if (vie <= 0)
        {
            Debug.Log("Bulldog mort");
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().PlayerTakesDamage(damageAmount);
        }
    }
}
