using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Definiowane w panelu inspekcyjnym")]
    [SerializeField]
    public int health = 100;
    public float InvAfterDmg;
    public float moveSpeed=5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Main main;
    public bool invulnerable = false;

    Vector2 movement;

    void Start()
    {
        main = FindObjectOfType<Main>();
        main.setHealth(health);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            StartCoroutine(takingDamage(damage));
        }
    }
    IEnumerator takingDamage(int damage)
    {
        invulnerable = true;

        health -= damage;
        main.setHealth(health);

        if (health <= 0)
        {
           //game over
        }
        yield return new WaitForSeconds(InvAfterDmg); //przez jakis czas niesmiertelnosc
        invulnerable = false;
    }

}
