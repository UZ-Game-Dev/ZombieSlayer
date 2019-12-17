using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public float timeBetwSpawn;
    public GameObject enemy;
    public Transform[] zombies = new Transform[4];
    public bool spawning = false;
    private void Start()
    {
        spawning = false;
    }
    private void Update()

    {
        if (!spawning)
        { 
            StartCoroutine(SpawnOrder()); 
        
        }
       
   
    }
    IEnumerator SpawnOrder()
    {
        spawning = true;
        yield return StartCoroutine(SpawnTimer());
        spawning = false;
    }
    IEnumerator SpawnTimer()
    {
        Instantiate(enemy, zombies[Random.Range(0, 4)].position, Quaternion.identity);
        yield return new WaitForSeconds(timeBetwSpawn);
    }
}
