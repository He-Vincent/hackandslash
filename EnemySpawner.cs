using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;


    public float spawnWaitDuration = 3;
    float spawnWaitUntilTime;

    int enemyGroupNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        spawnWaitUntilTime = Time.time + spawnWaitDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyGroupNumber < 5 && spawnWaitUntilTime <= Time.time)
        {
            //instantiate the enemies at sand place
            Instantiate(enemy1, transform.position, Quaternion.identity);
            Instantiate(enemy2, transform.position, Quaternion.identity);
            Instantiate(enemy3, transform.position, Quaternion.identity);
            enemyGroupNumber += 1;
            //Debug.Log(enemyGroupNumber);

            spawnWaitUntilTime = Time.time + spawnWaitDuration;
        }

    }

}

