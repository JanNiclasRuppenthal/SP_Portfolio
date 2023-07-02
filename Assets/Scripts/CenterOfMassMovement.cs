using UnityEngine;

public class CenterOfMassMovement : MonoBehaviour
{

    private Vector3 massCenterOfParent;
    private PlayerMovement playerMovement;
    private Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponentInParent<Rigidbody>();
        massCenterOfParent = body.centerOfMass;

        parentTransform = GetComponentInParent<Transform>();

        playerMovement = GetComponentInParent<PlayerMovement>();

     
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + playerMovement.MassCenter;
    }
}
