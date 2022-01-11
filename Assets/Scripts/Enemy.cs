using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ENCAPSULATION

    protected float acceleration = 100.0f;
    protected float maxSpeed = 5.0f;
    protected Rigidbody rigidBody;

    private GameObject player;
    private float maxRadius;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rigidBody = GetComponent<Rigidbody>();
        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        maxRadius = playArea.transform.localScale.x / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        ApplyMovement();
        ApplyRotation();
        ConstrainSpeed();
        ConstrainPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    private void ApplyRotation()
    {
        transform.LookAt(rigidBody.velocity);
    }

    protected virtual void ApplyMovement()
    {
        if (player != null)
        {
            AccelerateTowardsPlayer();
        }
    }

    private void AccelerateTowardsPlayer()
    {
        var playerDirection = (player.transform.position - transform.position).normalized;
        rigidBody.AddForce(playerDirection * acceleration * Time.deltaTime, ForceMode.Force);
    }

    // TODO factor out these constraints into a script common to Player and Enemy

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
}
