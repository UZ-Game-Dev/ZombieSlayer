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

    private Main main;
    private WeaponMenager weaponMenager;
    public bool invulnerable = false;

    Vector2 movement;

    void Start()
    {
        main = FindObjectOfType<Main>();
        weaponMenager = GetComponentInChildren<WeaponMenager>();
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

    public void Pickup(Types types, GameObject pickup)
    {
        switch (types)
        {
            case Types.Ammunition:
                if (weaponMenager.currentWeapon == 0)
                {
                    Debug.Log("AK");
                    break;
                }
                else if(GetComponentInChildren<WeaponDef>().ammo == GetComponentInChildren<WeaponDef>().ammoMax)
                {
                    Debug.Log("Full ammo!");
                    break;
                }
                else
                {
                    GetComponentInChildren<WeaponDef>().RefilAmmo();
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
            default:
                break;
        }
    }

}
