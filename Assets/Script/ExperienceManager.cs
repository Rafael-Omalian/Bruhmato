using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public float expValue = 1f;
    [SerializeField] private float attractionSpeed = 10f;
    private Transform playerTransform;

    void Update()
    {
        if (playerTransform)
        {
            transform.Translate((playerTransform.position - transform.position) * attractionSpeed * Time.deltaTime);
        }
    }

    public void SetExp(float expValue)
    {
        this.expValue = expValue;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerLevelManager>().GetExp(expValue);
            Destroy(gameObject);
        }
    }

    public void Attraction(Transform target)
    {
        playerTransform = target;
    }
}
