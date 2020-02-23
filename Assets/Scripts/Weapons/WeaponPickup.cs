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

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (touched && Input.GetButtonDown("Use"))
        {
            if (weaponMenager.currentWeapon != weaponNumber)
            {
                weaponMenager.ChangeWeapon(weaponNumber);
                Destroy(gameObject);
            }
            else
            {
                //refil ammo
                if (weaponMenager.currentWeapon == 0)
                {
                    Debug.Log("AK");
                }
                else if (player.GetComponentInChildren<WeaponDef>().ammo == player.GetComponentInChildren<WeaponDef>().ammoMax)
                {
                    Debug.Log("Full ammo!");
                }
                else
                {
                    player.GetComponentInChildren<WeaponDef>().RefilAmmo();
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
