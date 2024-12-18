using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Coq : Ennemis
{

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 1.7f)
        {
            if (!isAttacking)
            {
                StartCoroutine(DelayAttack());
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            Mouvement();
        }
    }

    public override void Attack()
    {
        isAttacking = true;
        zoneAttaque.SetActive(true);
    }

    // Permet de suivre le joueur
    public override void Mouvement()
    {
        vitesse = 2f;
        rb.MovePosition(rb.position + Vector3.Normalize(player.transform.position - rb.position) * vitesse * Time.deltaTime);
        transform.LookAt(new Vector3(player.position.x, rb.position.y, player.position.z));
    }

    

    public IEnumerator DelayAttack()
    {
        isAttacking = true;
        zoneAttaque.SetActive(true);
        Debug.Log("Coq attaque");
        yield return new WaitForSeconds(0.2f);
        zoneAttaque.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().PlayerTakesDamage(damageAmount);
        }
    }
}
