using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvasiveEnemy : Enemy
{
    private float evasionDistance = 10.0f;

    override protected void ApplyMovement()
    {
        base.ApplyMovement();
        GameObject[] objectsToAvoid = FindObjectsToAvoid();
        AvoidObjects(objectsToAvoid);
    }

    private GameObject[] FindObjectsToAvoid()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return asteroids.Concat(enemies).ToArray();
    }

    private void AvoidObjects(GameObject[] objects)
    {
        foreach (var asteroid in objects)
        {
            float distanceToAsteroid = (asteroid.transform.position - transform.position).magnitude;
            if (distanceToAsteroid <= evasionDistance)
            {
                AccelerateAwayFromAsteroid(asteroid);
            }
        }
    }

    private void AccelerateAwayFromAsteroid(GameObject asteroid)
    {
        var asteroidDirection = (asteroid.transform.position - transform.position).normalized;
        rigidBody.AddForce(-asteroidDirection * acceleration * Time.deltaTime, ForceMode.Force);
    }
}
