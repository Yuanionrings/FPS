using JetBrains.Annotations;
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

    [SerializeField]
    private float jumpForce = 1000f;

    //[SerializeField]
    //private float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check Ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Calculate Movement
        float xPosition = Input.GetAxisRaw("Horizontal");
        float zPosition = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xPosition;
        Vector3 moveVertical = transform.forward * zPosition;

        // Calculate Movement
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

        // Calculate Jump Force
        Vector3 jump = Vector3.zero;

        if (isGrounded && Input.GetButton("Jump"))
        {
            jump = Vector3.up * jumpForce;
        }

        // Apply Jump Force
        motor.SetJumpForce(jump);

        // Calculate Gravity
        //Vector3 fallingVelocity = Vector3.zero;
        //fallingVelocity.y += gravity * Time.deltaTime;

        // Apply Gravity
        //motor.SetGravity(fallingVelocity);
    }
}
