using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class FastEnemy : Enemy
{
    void Awake()
    {
        acceleration = 150.0f;
        maxSpeed = 10.0f;
    }

}
