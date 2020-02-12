using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject exit1;
    public GameObject exit2;
    public GameObject exit3;
    public GameObject exit4;
    public int zombieKilled=0;
    public int zombieCounter;
    public int zombieCount;
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
        if (!spawning && zombieCount<zombieCounter)
        { 
            StartCoroutine(SpawnOrder());
            zombieCount++;
        
        }

        

       
        if (zombieKilled==zombieCounter)
        {
            exit1.SetActive(false);
            exit2.SetActive(false);
            exit3.SetActive(false);
            exit4.SetActive(false);


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
