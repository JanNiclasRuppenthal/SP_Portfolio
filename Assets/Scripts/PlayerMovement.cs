using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 1000f;
    private Vector3 massCenter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        massCenter = rb.centerOfMass;
    }

    private void FixedUpdate()
    {
        // Spieler-Input abrufen
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Zentrum der Masse basierend auf dem Spieler-Input anpassen
        rb.AddForce(new Vector3(horizontalInput * moveSpeed * Time.fixedDeltaTime,0, verticalInput * moveSpeed * Time.fixedDeltaTime));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.rotation * massCenter, .5f);
    }
}
