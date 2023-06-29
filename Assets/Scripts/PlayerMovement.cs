using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 500f;
    public Vector3 massCenter;

    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        massCenter = rb.centerOfMass;
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Richtung der Kamera in der horizontalen Ebene
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Richtung der Kamera senkrecht zur horizontalen Ebene
        Vector3 cameraRight = mainCamera.transform.right;

        // Bewegungsrichtung berechnen basierend auf Kamerarotation und Spieler-Input
        Vector3 movementDirection = (cameraRight * horizontalInput + cameraForward * verticalInput).normalized;

        // Kraft anwenden, basierend auf der Bewegungsrichtung und der Rotationsgeschwindigkeit der Kamera
        rb.AddForce(movementDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + /*transform.rotation */ massCenter, .5f);
    }
}
