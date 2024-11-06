using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    private Vector3 movement;
    private Quaternion targetedRotation;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1.0f);
    }

    void FixedUpdate()
    {
        //Deplacement
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        //Rotation
        if (movement != Vector3.zero)
        {
            targetedRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetedRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
