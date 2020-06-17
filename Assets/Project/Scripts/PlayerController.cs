using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationY = 0f;
    private float currentCameraRotationY = 0f;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    [SerializeField]
    private float speed = 4f;

    [SerializeField]
    private float mouseSensitivity = 8f;

    [SerializeField]
    private float jumpForce = 5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check Ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Calculate X-Rotation
        float mouseX = Input.GetAxisRaw("Mouse X");

        rotation = new Vector3(0f, mouseX, 0f) * mouseSensitivity;

        // Calculate Camera Y-Rotation
        float mouseY = Input.GetAxisRaw("Mouse Y");
         
        cameraRotationY = mouseY * mouseSensitivity;

        // Calculate Movement
        float xPosition = Input.GetAxisRaw("Horizontal");
        float zPosition = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xPosition;
        Vector3 moveVertical = transform.forward * zPosition;

        // Calculate Movement
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 2.25f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed = 1.25f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = 4f;
        }

        velocity = (moveHorizontal + moveVertical).normalized * speed;
    }
    void FixedUpdate()
    {
        Move();
        Rotate();
        Jump();
    }

    private void Move()
    {
        // Apply Movement
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButton("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Rotate()
    {
        // Apply Rotation
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            // Rotation Calculation
            currentCameraRotationY -= cameraRotationY;
            currentCameraRotationY = Mathf.Clamp(currentCameraRotationY, -cameraRotationLimit, cameraRotationLimit);
            
            // Applying Rotation
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationY, 0f, 0f);
        }
    }

}
