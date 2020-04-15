using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ZombieDef : MonoBehaviour
{
    public int score;
    private Main main;
    public int damage;
    public int health;
    public float speed;
    public GameObject[] dropList;
    
    public bool canDrop = false;
    Vector2 moveDirection;
    private Transform playerPos;
    private ZombieSpawner sp;
    private Rigidbody2D rb;
    private AudioSource source;
    
    private WeaponMenager weaponMenager;


    void Start()
    {
        
        main = FindObjectOfType<Main>();
        sp = FindObjectOfType<ZombieSpawner>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
       /* GameObject gameObject = GameObject.FindWithTag("Player");
        playerPos = gameObject.GetComponent<Transform>();
        weaponMenager = gameObject.GetComponentInChildren<WeaponMenager>(); */

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
        if (health<=0)
        {
            sp.playSound();
            main.addScore(score);
            sp.zombieKilled++;
            Drop();
            Destroy(this.gameObject);
        }
        
    }

    public void Drop() 
    {
        if (canDrop)
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
}
