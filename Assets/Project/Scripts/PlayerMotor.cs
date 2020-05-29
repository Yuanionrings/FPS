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

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (this.velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + this.velocity * Time.fixedDeltaTime);
        }
    }

    private void Rotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(this.rotation));

        if (cam != null)
        {
            cam.transform.Rotate(-this.cameraRotation);
        }
    }
}
