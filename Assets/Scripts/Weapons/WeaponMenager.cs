using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenager : MonoBehaviour
{
    public List<GameObject> weapon;
    public int currentWeapon;
    private void Start()
    {
        weapon[0].SetActive(true);
        for (int i = 1; i < weapon.Capacity; i++) { weapon[i].SetActive(false); } //wylacza reszte broni
        currentWeapon = 0;
    }

    public void ChangeWeapon(int nr)
    {
        weapon[currentWeapon].SetActive(false);
        weapon[nr].SetActive(true);
        currentWeapon = nr;
    }
}
