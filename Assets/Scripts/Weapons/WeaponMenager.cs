using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenager : MonoBehaviour
{
    public Text WeaponName;
    public List<GameObject> weapon;
    public int currentWeapon;
    public int secondaryWeapon;
    public bool weaponPickedUp = false;
    private void Start()
    {
        currentWeapon = 0;
        for (int i = 1; i < weapon.Capacity; i++) { weapon[i].SetActive(false); } //wylacza reszte broni
        ChangeWeapon(0);
    }
    public void ChangeWeapon(int nr)
    {
        weapon[currentWeapon].SetActive(false);
        weapon[nr].SetActive(true);
        currentWeapon = nr;
        switch (nr)
        {
            case 0:
                WeaponName.text = "AK-47";
                break;

            case 1:
                WeaponName.text = "M4";
                break;
            case 2:
                WeaponName.text = "Shotgun";
                break;
            case 3:
                WeaponName.text = "SMG";
                break;
        }



        }
}
