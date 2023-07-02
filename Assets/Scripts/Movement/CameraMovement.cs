using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Referenz auf das Zielobjekt (die Kugel)
    public float distance = 5f; // Abstand zur Kugel
    public float height = 2f; // Höhe der Kamera über der Kugel
    public float rotationSpeed = 3f; // Rotationsgeschwindigkeit der Kamera

    public GameObject filter;

    private float mouseX; // Mausbewegung entlang der x-Achse
    private float mouseY;
    private float tempMouseY;

    public bool IsInsideTarget { get; set; } = true; // Flag, um den Kameramodus zu verfolgen

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; // Mausbewegung entlang der x-Achse erfassen
        tempMouseY = mouseY + Input.GetAxis("Mouse Y") * rotationSpeed;
        if (tempMouseY < 0)
        {
            mouseY = 0;
        }
        else if (tempMouseY < 90) // Begrenzung auf unter 90 Grad
        {
            mouseY = tempMouseY;
        }

        if (Input.GetKeyDown(KeyCode.C)) // Tastendruck erfassen (hier C)
        {
            IsInsideTarget = !IsInsideTarget; // Kameramodus umschalten
            filter.SetActive(IsInsideTarget);
        }
    }

    private void LateUpdate()
    {
        if (!IsInsideTarget) // Wenn nicht im Kameramodus innerhalb der Kugel
        {
            // Kamera um die Kugel herum positionieren
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);
            Vector3 position = target.position - rotation * Vector3.forward * distance + Vector3.up * height;

            // Kamera auf die neue Position und Rotation setzen
            transform.position = position;
            transform.LookAt(target.position);
        }
        else 
        {
            // Kamera in der Kugel positionieren und sich mit der Kugel rotieren
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }


}
