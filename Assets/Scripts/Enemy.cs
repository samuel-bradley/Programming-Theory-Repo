using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float acceleration;
    public float maxSpeed;

    private Rigidbody rigidBody;
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
        if (player != null)
        {
            AccelerateTowardsPlayer();
            RotateTowardsPlayer();
        }
        ConstrainSpeed();
        ConstrainPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

    private void AccelerateTowardsPlayer()
    {
        var playerDirection = (player.transform.position - transform.position).normalized;
        rigidBody.AddForce(playerDirection * acceleration * Time.deltaTime, ForceMode.Force);
    }

    private void RotateTowardsPlayer()
    {
        transform.LookAt(player.transform);
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
        Debug.Log(transform.position.magnitude);
        if (transform.position.magnitude > maxRadius)
        {
            transform.position = transform.position.normalized * maxRadius;
        }
    }
}
