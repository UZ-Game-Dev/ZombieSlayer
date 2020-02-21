using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieDef : MonoBehaviour
{

    public int damage;
    public int health;
    public float speed;
    private Transform playerPos;
    private ZombieSpawner sp;
    private Rigidbody2D rb;
    void Start()
    {
        sp = FindObjectOfType<ZombieSpawner>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //poruszanie sie w strone gracza
        //transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        rb.MovePosition(Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            sp.zombieKilled++; 
            Destroy(gameObject);
        }
    }
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

    } */
}
