using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;

    private Rigidbody rigidBody;
    private float maxRadius;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        maxRadius = playArea.transform.localScale.x / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyAcceleration();
        RotateToVelocity();
        ConstrainSpeed();
        ConstrainPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    private void ApplyAcceleration()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float xForce = horizontalInput * acceleration * Time.deltaTime;
        float zForce = verticalInput * acceleration * Time.deltaTime;
        rigidBody.AddForce(Vector3.right * xForce, ForceMode.Force);
        rigidBody.AddForce(Vector3.forward * zForce, ForceMode.Force);
    }

    private void ConstrainSpeed()
    {
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
    }

    private void ConstrainPosition()
    {
        if (transform.position.magnitude > maxRadius)
        {
            transform.position = transform.position.normalized * maxRadius;
        }
    }

    private void RotateToVelocity()
    {
        transform.LookAt(transform.position + rigidBody.velocity.normalized);
    }
}
