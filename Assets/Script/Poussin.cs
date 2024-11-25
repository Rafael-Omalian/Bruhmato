using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poussin : Ennemis
{
    private bool isAttacking = false;
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 2f)
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
        vitesse = 4f;
        rb.MovePosition(rb.position + Vector3.Normalize(player.transform.position - rb.position) * vitesse * Time.deltaTime);
        transform.LookAt(new Vector3(player.position.x, rb.position.y, player.position.z));
    }
    public void TakesDamage(float baseDamage)
    {
        vie -= baseDamage;

        if (vie <= 0)
        {
            Debug.Log("Poussin mort");
            Destroy(this);
        }
    }

    public IEnumerator DelayAttack()
    {
        isAttacking = true;
        zoneAttaque.SetActive(true);
        Debug.Log("Poussin attaque");
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
