using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Vache : Ennemis
{
    public GameObject bullet;
    public Transform bulletPos;
    private bool isShooting = false;
    private bool isFlee = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 14)
        {
            isFlee = true;
        }
        if (distance > 18)
        {
            isFlee = false;
        }
        if (distance > 14 && isFlee == false && !isShooting)
        {
            StartCoroutine(DelayShoot());
        }
        transform.LookAt(new Vector3(player.position.x, rb.position.y, player.position.z));
    }

    private void FixedUpdate()
    {

        if (isFlee)
        {
            Mouvement();
        }
    }

    public override void Attack()
    {
        isShooting = true;

    }

    // Permet de suivre le joueur
    public override void Mouvement()
    {
        rb.MovePosition(rb.position - Vector3.Normalize(player.transform.position - rb.position) * vitesse * Time.deltaTime);
    }
    public void TakesDamage(float baseDamage)
    {
        vie -= baseDamage;

        if (vie <= 0)
        {
            Debug.Log("Vache mort");
            Destroy(this);
        }
    }
    public IEnumerator DelayShoot()
    {
        isShooting = true;
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Debug.Log("Vache tire");
        yield return new WaitForSeconds(0.01f);
        isShooting = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().PlayerTakesDamage(damageAmount);
        }
    }
}
