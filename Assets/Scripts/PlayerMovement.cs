using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Definiowane w panelu inspekcyjnym")]
    [SerializeField]
    public int health = 100;
    public int HealthPickup;
    public float InvAfterDmg;
    public float moveSpeed=5f;
    private int iterator;
    private int wDupe;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject pickUP_Sprite;

    private Main main;
    private WeaponMenager weaponMenager;
    public bool invulnerable = false;
    public bool invulnerable_PU = false;

    private AudioSource source;

    [Header("Power Ups")]
    public float GoldBullets_Time;
    public float inv_Time;
    public float speed_Time;
    public float speed_Value;
    public float nuke_range;
    public int nuke_dmg;
    public float freeze_Time;
    public float freeze_range;
    public float scare_Time;
    public float scare_range;

    Vector2 movement;

    void Start()
    {
        source = GetComponent<AudioSource>();
        main = FindObjectOfType<Main>();
        weaponMenager = GetComponentInChildren<WeaponMenager>();
        main.setHealth(health);
        pickUP_Sprite.SetActive(false);
    }

    void Update()
    {
        if (iterator != wDupe) source.Play();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (invulnerable)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color (1f/(Mathf.PingPong(Time.time * 4, 0.9f)+0.1f), Mathf.PingPong(Time.time * 4, 1f), Mathf.PingPong(Time.time * 4, 1f), 1f);
        }
        if (!invulnerable )
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        }
    }

    void FixedUpdate()
    {
        //animator
        if (movement != Vector2.zero)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (movement.x > 0)
        {
            animator.SetBool("walkRight", true);
        }
        if (movement.x < 0)
        {
            animator.SetBool("walkRight", false);
        }
        
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void playSound()
    {
        source.Play();
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable_PU)
        {
            if (!invulnerable)
            {
                StartCoroutine(takingDamage(damage));
            }
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

    public void Pickup(Types types, GameObject pickup)
    {
        source.Play();
        
        switch (types)
        {
            case Types.Ammunition:
                if (weaponMenager.currentWeapon == 0)
                {
                    Debug.Log("AK");
                    break;
                }
                else
                {
                    GetComponentInChildren<WeaponDef>().AddAmmo();
                    Destroy(pickup);
                    
                }
                break;
            case Types.Bandages:
       
                health += HealthPickup;
                main.setHealth(health);
                Destroy(pickup);
                break;
            case Types.Coin:
                //może kiedyś?
                break;
            case Types.GoldBullets_PU:
                StartCoroutine(ShotingGold(GoldBullets_Time));
                Destroy(pickup);
                break;
            case Types.Inv_PU:
                StartCoroutine(Inv(inv_Time));
                Destroy(pickup);
                break;
            case Types.Speed_PU:
                StartCoroutine(Speed(speed_Time, speed_Value));
                Destroy(pickup);
                break;
            case Types.Nuke_PU:
                Nuke(nuke_range, nuke_dmg);
                Destroy(pickup);
                break;
            case Types.Freeze_PU:
                StartCoroutine(Freeze(freeze_Time, freeze_range));
                Destroy(pickup);
                break;
            case Types.Scare_PU:
                StartCoroutine(Scare(scare_Time, scare_range));
                Destroy(pickup);
                break;
            default:
                break;
        }
    }

    IEnumerator ShotingGold(float x)
    {
        GetComponentInChildren<WeaponDef>().shotingGold = true;
        yield return new WaitForSeconds(x);
        GetComponentInChildren<WeaponDef>().shotingGold = false;
    }
    IEnumerator Inv(float time)
    {
        invulnerable_PU = true;
        GetComponent<SpriteRenderer>().color = new Color(0f, 200f, 255f, 255f);
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        invulnerable_PU = false;
    }
    IEnumerator Speed(float time, float speed)
    {
        float oldSpeed = moveSpeed;
        moveSpeed = speed;
        //animacje chyba blokuja zmiane coloru?
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 210f, 0f, 255f);
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        moveSpeed = oldSpeed;
    }
    private void Nuke(float range, int damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, range);
        foreach (Collider2D nearbyObject in colliders)
        {
            ZombieDef zombie = nearbyObject.transform.GetComponent<ZombieDef>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
    IEnumerator Freeze(float time, float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, range);
        foreach (Collider2D nearbyObject in colliders)
        {
            ZombieDef zombie = nearbyObject.transform.GetComponent<ZombieDef>();
            if (zombie != null)
            {
                zombie.getCold(scare_Time);
            }
        }
        yield return new WaitForSeconds(time);
    }
    IEnumerator Scare(float time, float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, range);
        foreach (Collider2D nearbyObject in colliders)
        {
            ZombieDef zombie = nearbyObject.transform.GetComponent<ZombieDef>();
            if (zombie != null)
            {
                zombie.getSpooky(time);
            }
        }
        yield return new WaitForSeconds(time);

    }
}
