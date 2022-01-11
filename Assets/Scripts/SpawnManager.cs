using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] asteroidPrefabs;
    public float enemySpawnInterval;
    public float asteroidSpawnInterval;
    public float minRadiusBeyondPlayArea;
    public float maxRadiusBeyondPlayArea;
    public float asteroidMinDriftSpeed;
    public float asteroidMaxDriftSpeed;
    public float asteroidMinRotationSpeed;
    public float asteroidMaxRotationSpeed;

    private float playAreaRadius;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playArea = GameObject.FindGameObjectWithTag("PlayArea");
        playAreaRadius = playArea.transform.localScale.x / 2.0f;
        InvokeRepeating("SpawnAsteroid", asteroidSpawnInterval, asteroidSpawnInterval);
        InvokeRepeating("SpawnEnemy", enemySpawnInterval, enemySpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, RandomPosition(), enemyPrefab.transform.rotation);
    }

    private void SpawnAsteroid()
    {
        GameObject randomAsteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject asteroid = Instantiate(randomAsteroidPrefab, RandomPosition(), randomAsteroidPrefab.transform.rotation);
        Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
        asteroidScript.driftDirection = RandomDirection();
        asteroidScript.driftSpeed = Random.Range(asteroidMinDriftSpeed, asteroidMaxDriftSpeed);
        asteroidScript.rotationAxis = Random.rotation * Vector3.up;
        asteroidScript.rotationSpeed = Random.Range(asteroidMinRotationSpeed, asteroidMaxRotationSpeed);
    }

    private Vector3 RandomPosition()
    {
        float randomRadius = Random.Range(playAreaRadius + minRadiusBeyondPlayArea, playAreaRadius + maxRadiusBeyondPlayArea);
        return RandomDirection() * randomRadius;
    }

    private Vector3 RandomDirection()
    {
        float randomAngle = Random.Range(0.0f, 360.0f);
        return Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
    }
}
