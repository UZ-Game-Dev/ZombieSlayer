using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieDef : MonoBehaviour
{
    public int score;
    private Main main;
    public int damage;
    public int health;
    public float speed;
    public GameObject[] dropList;

    Vector2 moveDirection;
    private Transform playerPos;
    private ZombieSpawner sp;
    private Rigidbody2D rb;
    void Start()
    {
        main = FindObjectOfType<Main>();
        sp = FindObjectOfType<ZombieSpawner>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //fliper
        if (playerPos.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        rb.MovePosition(Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            main.addScore(score);
            sp.zombieKilled++;
            Drop();
            Destroy(gameObject);
        }
    }

    public void Drop() 
    {
        //leci przez cala liste i sprawdza czy dropnie
        for (int i = 0; i < dropList.Length; i++)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < dropList[i].GetComponent<Item>().chanceToDrop) //pobiera chanceToDrop z dziedziczonej klasy GunDefinition dla każdego GO z listy, zajebiscie
            {
                Instantiate(dropList[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }

}
