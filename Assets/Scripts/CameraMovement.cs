using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // Referenz auf das Zielobjekt (die Kugel)
    public float distance = 5f; // Abstand zur Kugel
    public float height = 2f; // Höhe der Kamera über der Kugel
    public float rotationSpeed = 3f; // Rotationsgeschwindigkeit der Kamera

    private float mouseX; // Mausbewegung entlang der x-Achse

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; // Mausbewegung entlang der x-Achse erfassen
    }

    private void LateUpdate()
    {
        // Kamera um die Kugel herum positionieren
        Quaternion rotation = Quaternion.Euler(0f, mouseX, 0f);
        Vector3 position = target.position - rotation * Vector3.forward * distance + Vector3.up * height;

        // Kamera auf die neue Position und Rotation setzen
        transform.position = position;
        transform.LookAt(target.position);
    }


}
