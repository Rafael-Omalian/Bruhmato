using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Bulldog : Ennemis
{
    private Material bdMat;

    private void Awake()
    {
        bdMat = transform.GetChild(1).GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 2.5f)
        {
            if (!isAttacking)
            {
                StartCoroutine(DelayExplosion());
            }
        }

        if (isAttacking)
        {
            bdMat.color = new Color(bdMat.color.r + 1 * Time.deltaTime, bdMat.color.g, bdMat.color.b, bdMat.color.a); //rgb, alpha
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
        Debug.Log("Bulldog explose");
        Destroy(gameObject, 3f); //temps -> 3
    }

    // Permet de suivre le joueur
    public override void Mouvement()
    {
        vitesse = 1.5f;
        rb.MovePosition(rb.position + Vector3.Normalize(player.transform.position - rb.position) * vitesse * Time.deltaTime);
        transform.LookAt(new Vector3(player.position.x, rb.position.y, player.position.z));
    }

    public IEnumerator DelayExplosion()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1.5f);
        zoneAttaque.SetActive(true);
        Debug.Log("Bulldog explose");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        zoneAttaque.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().PlayerTakesDamage(damageAmount);
        }
    }
}
