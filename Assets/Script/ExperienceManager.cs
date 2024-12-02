using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public float expValue = 1f;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerLevelManager>().GetExp(expValue);
            Destroy(gameObject);
        }
    }
}
