using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float maxRadiusBeyondPlayArea;

    private float maxRadius;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        maxRadius = (playArea.transform.localScale.x / 2.0f) + maxRadiusBeyondPlayArea;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > maxRadius)
        {
            Destroy(gameObject);
        }
    }
}
