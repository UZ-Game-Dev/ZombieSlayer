﻿using System.Collections;
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

    public bool frozen = false;
    public bool scared = false;

    Vector2 moveDirection;
    private Transform playerPos;
    private ZombieSpawner sp;
    private Rigidbody2D rb;
    private AudioSource source;



    void Start()
    {
        
        main = FindObjectOfType<Main>();
        sp = FindObjectOfType<ZombieSpawner>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //big flipper
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (frozen)
        {
            
        }
        else if (scared)
        {
            if (playerPos.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            //fliper
            if (playerPos.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }


        if (frozen)
        {

        }
        else if (scared)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, transform.position + (transform.position - playerPos.position), speed * Time.deltaTime));
        }
        else
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime));
        }
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
    public void getCold(float time)
    {
        StartCoroutine(freeze(time));
    }
    IEnumerator freeze(float time)
    {
        frozen = true;
        GetComponent<Animator>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<SpriteRenderer>().color = new Color(0f, 180f, 255f, 255f);
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Animator>().enabled = true;
        frozen = false;
    }
    public void getSpooky(float time)
    {       
        StartCoroutine(scare(time));
    }
    IEnumerator scare(float time)
    {
        scared = true;
        yield return new WaitForSeconds(time);
        scared = false;
    }
}
