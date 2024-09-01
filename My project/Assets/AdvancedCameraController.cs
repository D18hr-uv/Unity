using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCameraController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float zoomSpeed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        // Movement controls
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, vertical);

        // Zoom controls (using mouse scroll wheel)
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Camera.main.fieldOfView -= scroll;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 20f, 100f);

        // Rotation controls (right mouse button)
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            transform.Rotate(-rotY, rotX, 0);
        }
    }
}
