using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // sphere
    public GameObject filter; // blue canvas

    private float distance = 10f; 
    private float height = 2f; 
    private float rotationSpeed = 3f; 

    private float mouseX;
    private float mouseY;
    private float tempMouseY;

    public bool IsInsideTarget { get; set; } = true;

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        tempMouseY = mouseY + Input.GetAxis("Mouse Y") * rotationSpeed;
        if (tempMouseY < 0)
        {
            mouseY = 0;
        }
        else if (tempMouseY < 90)
        {
            mouseY = tempMouseY;
        }

        // switch camera mode and  (de-) activate filter
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            IsInsideTarget = !IsInsideTarget; 
            filter.SetActive(IsInsideTarget);
        }
    }

    private void LateUpdate()
    {
        if (!IsInsideTarget) 
        {
            // place camera around the target (sphere)
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);
            Vector3 position = target.position - rotation * Vector3.forward * distance + Vector3.up * height;
            transform.position = position;
            transform.LookAt(target.position);
        }
        else 
        {
            // camera is in our target (sphere)
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }


}
