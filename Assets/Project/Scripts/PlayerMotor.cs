using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;
    //private Vector3 gravity = Vector3.zero;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void SetRotation(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    public void SetCameraRotation(Vector3 cameraRotation)
    {
        this.cameraRotation = cameraRotation;
    }

    public void SetJumpForce(Vector3 jumpForce)
    {
        this.jumpForce = jumpForce;
    }

    //public void SetGravity(Vector3 gravity)
    //{
    //    this.gravity = gravity;
    //}

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + gravity * Time.fixedDeltaTime);

        Move();
        Rotate();
    }

    private void Move()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (jumpForce != Vector3.zero)
        {
            rb.AddForce(jumpForce * Time.fixedDeltaTime);
        }
    }

    private void Rotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
