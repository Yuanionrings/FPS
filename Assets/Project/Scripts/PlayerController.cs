using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float mouseSensitivity = 10f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Calculate Movement
        float xPosition = Input.GetAxisRaw("Horizontal");
        float zPosition = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xPosition;
        Vector3 moveVertical = transform.forward * zPosition;

        // Movement Vector
        if (Input.GetKeyDown(KeyCode.LeftShift) && (xPosition != 0f || zPosition != 0f))
        {
            speed = 2.5f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && (xPosition != 0f || zPosition != 0f)) {
            speed = 4.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 3.5f;
        }

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        // Apply Movement
        motor.SetVelocity(velocity);

        // Calculate X-Rotation
        float mouseX = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, mouseX, 0f) * mouseSensitivity;

        // Apply Rotation
        motor.SetRotation(rotation);

        // Calculate Camera Y-Rotation
        float mouseY = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(mouseY, 0f, 0f) * mouseSensitivity;

        // Apply Camera Rotation
        motor.SetCameraRotation(cameraRotation);
    }
}
