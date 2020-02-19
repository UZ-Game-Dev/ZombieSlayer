using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponNumber;
    public bool touched;
    
    private WeaponMenager wm;
    private Collider2D collision;

    private void Update()
    {
        if (touched && Input.GetButtonDown("Use"))
        {
            if (wm.currentWeapon != weaponNumber)
            {
                wm.ChangeWeapon(weaponNumber);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        wm = coll.GetComponentInChildren<WeaponMenager>();
        if (wm != null)
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
