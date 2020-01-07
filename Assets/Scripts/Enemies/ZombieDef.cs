using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieDef : MonoBehaviour
{

    public int damage;
    public int health;
    public float speed;
    private Transform playerPos;
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {

        //poruszanie sie w strone gracza
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime); 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* jak bedzie skrypt player
        player player = collision.GetComponent<player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        */
    }
}
