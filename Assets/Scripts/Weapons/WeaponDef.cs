using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDef : MonoBehaviour
{
    public enum WeaponType { AK, M4, Shotgun};
    public bool isShooting = false;
    [Header("Definiowanie Dynamiczne")]
    public WeaponType weaponType;
    public int ammo;
    public int ammoMax;
    public Transform firePoint;
    public GameObject bullet;
    public float delayBetweenShots;

    private Main main;


    private void Start()
    {
        isShooting = false;
        main = FindObjectOfType<Main>();
    }

    private void OnEnable()
    {
        main = FindObjectOfType<Main>();
        isShooting = false;
        RefilAmmo();
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && !isShooting && Time.timeScale != 0)
        {
            StartCoroutine(ShootOrder());
            if(weaponType != 0 && ammo <= 0) //zmiana na AK jak skonczy sie ammo
            {
                FindObjectOfType<WeaponMenager>().ChangeWeapon(0);
            }
        }
    }
    IEnumerator ShootOrder()
    {
        isShooting = true;
        yield return StartCoroutine(Shoot());
        isShooting = false;
    }
    IEnumerator Shoot()
    {
        Instantiate(bullet, firePoint.position, transform.rotation);
        if (weaponType != WeaponType.AK) //jeśli nie AK to odejmuje ammo
        { 
            ammo--;
            main.SetAmmo(ammo);
        } 
        yield return new WaitForSeconds(delayBetweenShots);
    }
    void OnDisable()
    {
        isShooting = false;
    }

    public void RefilAmmo()
    {
        ammo = ammoMax;
        main.SetAmmo(ammo);
    }
}

