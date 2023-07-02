using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 MassCenter { get; set; }
    public GameObject CenterOfMass;

    private Rigidbody rBody;
    private float moveSpeed = 500f;
    private Camera mainCamera;
    protected bool MassCenterMovement { get; set; } = false;
    private CameraMovement cameraMovement;
    private TerrainCollider terrainCollider;
    private bool isGrounded;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();

        cameraMovement = mainCamera.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // Tastendruck erfassen (hier M)
        {
            MassCenterMovement = !MassCenterMovement; // Kameramodus umschalten
            CenterOfMass.SetActive(MassCenterMovement);
        }

        // Prüfe, ob die Kugel den Boden berührt
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.6f) &&
                     hit.collider == terrainCollider;

        // Springen, wenn die Kugel den Boden berührt und die Leertaste gedrückt wird
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rBody.AddForce(Vector3.up * moveSpeed/1.25f);
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (!MassCenterMovement)
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
            rBody.AddForce(movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rBody.centerOfMass = new Vector3(horizontalInput, 0, verticalInput) * 2f;
            CenterOfMass.transform.position = transform.position + transform.rotation * rBody.centerOfMass;
            rBody.WakeUp();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.rotation * MassCenter, .5f);
    }
}
