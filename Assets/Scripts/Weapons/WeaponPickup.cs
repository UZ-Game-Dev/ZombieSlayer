using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int weaponNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        WeaponMenager wp = collision.GetComponentInChildren<WeaponMenager>();
        if (wp != null)
        {
            if (wp.currentWeapon != weaponNumber)
            {
                wp.ChangeWeapon(weaponNumber);
                Destroy(gameObject);
            }
        }

    }
}
