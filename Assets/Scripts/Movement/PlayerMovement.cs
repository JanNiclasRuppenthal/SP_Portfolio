using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 MassCenter { get; set; }
    public GameObject CenterOfMass;

    private Rigidbody rigidbody;
    private float moveSpeed = 500f;
    private Camera mainCamera;
    protected bool MassCenterMovement { get; set; } = false;
    private CameraMovement cameraMovement;
    private TerrainCollider terrainCollider;
    private bool isGrounded;


    private void Start()
    {
        // initialize variables
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        cameraMovement = mainCamera.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        // switch the movement of the sphere
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            MassCenterMovement = !MassCenterMovement;
            CenterOfMass.SetActive(MassCenterMovement);
        }

        // test if sphere is colliding with terrain (with local point (0, -1, 0))
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.6f) &&
                     hit.collider == terrainCollider;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * moveSpeed/1.25f); // jump
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (!MassCenterMovement)
        {
            Vector3 movementDirection = calculateMovementDirection(horizontalInput, verticalInput);
            rigidbody.AddForce(movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // movement is based on centerOfMass
            rigidbody.centerOfMass = new Vector3(horizontalInput, 0, verticalInput) * 2f;
            CenterOfMass.transform.position = transform.position + transform.rotation * rigidbody.centerOfMass;
            rigidbody.WakeUp();
        }

    }

    private Vector3 calculateMovementDirection(float hInput, float vInput)
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        Vector3 cameraRight = mainCamera.transform.right;

        Vector3 result;
        if (cameraMovement.IsInsideTarget)
        {
            result = (Vector3.right * hInput + Vector3.forward * vInput).normalized;
        }
        else
        {
            // movement is dependent of position of camera
            result = (cameraRight * hInput + cameraForward * vInput).normalized;
        }

        return result;
    }
}
