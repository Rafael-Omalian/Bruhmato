using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;
    [SerializeField] private float reach = 10f;
    [SerializeField] private float speed = 10f;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, startPosition) >= reach)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (transform.forward * speed * Time.deltaTime));
    }
}
