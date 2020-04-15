using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponNumber;
    public bool touched;
    
    private WeaponMenager weaponMenager;
    private PlayerMovement player;
    private Collider2D collision;

    private AudioSource source;


    private void Start()
    {
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (touched && Input.GetButtonDown("Use"))
        {
            player.playSound();
            weaponMenager.weaponPickedUp = true;
            if (weaponMenager.currentWeapon != weaponNumber)
            {
                weaponMenager.ChangeWeapon(weaponNumber);
                Destroy(gameObject);
              
            }
            else
            {
                //add ammo
                if (weaponMenager.currentWeapon == 0)
                {
                    Debug.Log("AK");
                }
                else
                {
                    player.GetComponentInChildren<WeaponDef>().AddAmmo();
                    Destroy(gameObject);
                }
            }           
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        weaponMenager = coll.GetComponentInChildren<WeaponMenager>();
        if (weaponMenager != null)
        {
            collision = coll;
            touched = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (touched)
        {
            touched = false;
        }
    }
}
