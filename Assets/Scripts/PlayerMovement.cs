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
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject pickUP_Sprite;
    public GameObject shield_sprite;

    private Main main;
    private WeaponMenager weaponMenager;
    public bool invulnerable = false;
    public bool invulnerable_PU = false;

    private AudioSource source;
    private float _speeding;

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
    public int goldValue;

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
        rb.MovePosition(rb.position + movement * (moveSpeed+_speeding/2.5f) * Time.fixedDeltaTime);
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

    public void Pickup(Types types)
    {
        switch (types)
        {
            case Types.Ammunition:
                if (weaponMenager.currentWeapon == 0)
                {
                    break;
                }
                else
                {
                    GetComponentInChildren<WeaponDef>().AddAmmo();
                    
                }
                break;
            case Types.Bandages:
       
                health += HealthPickup;
                main.setHealth(health);
                break;
            case Types.Gold:
                main.addScore(goldValue);
                break;
            case Types.GoldBullets_PU:
                StopCoroutine("ShotingGold");
                StartCoroutine("ShotingGold");
                break;
            case Types.Inv_PU:
                StopCoroutine("Inv");
                StartCoroutine("Inv");
                break;
            case Types.Speed_PU:
                StopCoroutine("Speed");
                StartCoroutine("Speed");
                break;
            case Types.Nuke_PU:
                Nuke(nuke_range, nuke_dmg);
                break;
            case Types.Freeze_PU:
                StopCoroutine("Freeze");
                StartCoroutine("Freeze");
                break;
            case Types.Scare_PU:
                StopCoroutine("Scare");
                StartCoroutine("Scare");
                break;
            default:
                break;
        }
    }

    IEnumerator ShotingGold()
    {
        GetComponentInChildren<WeaponDef>().shotingGold = true;
        yield return new WaitForSeconds(GoldBullets_Time);
        GetComponentInChildren<WeaponDef>().shotingGold = false;
    }
    IEnumerator Inv()
    {
        invulnerable_PU = true;
        shield_sprite.SetActive(true);
        shield_sprite.GetComponent<SpriteRenderer>().color = Color.white;
        shield_sprite.GetComponent<Shield>().timer = 0;
        shield_sprite.GetComponent<Shield>().timeToPing = inv_Time-2;
        yield return new WaitForSeconds(inv_Time);
        shield_sprite.SetActive(false);
        invulnerable_PU = false;
    }
    IEnumerator Speed()
    {
        _speeding = speed_Value;
        //animacje chyba blokuja zmiane coloru? - odp: Może tak być ;)
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 210f, 0f, 255f);
        yield return new WaitForSeconds(speed_Time);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        _speeding = 1;
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
    IEnumerator Freeze()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, freeze_range);
        foreach (Collider2D nearbyObject in colliders)
        {
            ZombieDef zombie = nearbyObject.transform.GetComponent<ZombieDef>();
            if (zombie != null)
            {
                zombie.getCold(scare_Time);
            }
        }
        yield return new WaitForSeconds(freeze_Time);
    }
    IEnumerator Scare()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, scare_range);
        foreach (Collider2D nearbyObject in colliders)
        {
            ZombieDef zombie = nearbyObject.transform.GetComponent<ZombieDef>();
            if (zombie != null)
            {
                zombie.getSpooky(scare_Time);
            }
        }
        yield return new WaitForSeconds(scare_Time);

    }
}
