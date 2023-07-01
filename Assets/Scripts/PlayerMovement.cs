using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 500f;
    public Vector3 massCenter;

    private Camera mainCamera;
    private bool massCenterMovement = false;
    private CameraMovement cameraMovement;
    private TerrainCollider terrainCollider;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        massCenter = rb.centerOfMass;
        mainCamera = Camera.main;
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();

        cameraMovement = mainCamera.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Tastendruck erfassen (hier M)
        {
            massCenterMovement = !massCenterMovement; // Kameramodus umschalten
        }

        // Prüfe, ob die Kugel den Boden berührt
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.6f) &&
                     hit.collider == terrainCollider;

        // Springen, wenn die Kugel den Boden berührt und die Leertaste gedrückt wird
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * moveSpeed/1.25f);
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (!massCenterMovement )
        {
            // Richtung der Kamera in der horizontalen Ebene
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            // Richtung der Kamera senkrecht zur horizontalen Ebene
            Vector3 cameraRight = mainCamera.transform.right;

            // Bewegungsrichtung berechnen basierend auf Kamerarotation und Spieler-Input
            Vector3 movementDirection;
            if (cameraMovement.IsInsideTarget)
            {
                movementDirection = (Vector3.right * horizontalInput + Vector3.forward * verticalInput).normalized;

            }
            else
            {
                movementDirection = (cameraRight * horizontalInput + cameraForward * verticalInput).normalized;
            }


            // Kraft anwenden, basierend auf der Bewegungsrichtung und der Rotationsgeschwindigkeit der Kamera
            rb.AddForce(movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.centerOfMass = new Vector3(horizontalInput, 0, verticalInput) * 2f;
            rb.WakeUp();
            massCenter = rb.centerOfMass;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.rotation * massCenter, .5f);
    }
}
