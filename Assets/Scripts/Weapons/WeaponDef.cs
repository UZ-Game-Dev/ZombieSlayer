using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDef : MonoBehaviour
{
    public enum WeaponType { AK, M4, Shotgun, SMG };
    public bool isShooting = false;
    [Header("Definiowanie Dynamiczne")]
    public WeaponType weaponType;
    public int startingAmmo;
    public int ammo;
    public int ammoPickup;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject goldBullet;
    public bool shotingGold = false;
    public float delayBetweenShots;

    private AudioSource source;
    private Main main;

    public AudioSource Source { get => source; set => source = value; }

    private void Start()
    {
        Source = GetComponent<AudioSource>();
        isShooting = false;
        main = FindObjectOfType<Main>();
    }

    private void OnEnable()
    {
        main = FindObjectOfType<Main>();
        isShooting = false;
        ResetAmmo();
        if(weaponType == WeaponType.AK) main.SetAmmo("");
        else main.SetAmmo(ammo+"");
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && !isShooting && Time.timeScale != 0)
        {
            source.Play();
            StartCoroutine(ShootOrder());
            if(weaponType != WeaponType.AK && ammo <= 0) //zmiana na AK jak skonczy sie ammo
            {
                FindObjectOfType<WeaponMenager>().ChangeWeapon(0);
                FindObjectOfType<WeaponMenager>().weaponPickedUp = false;
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
        if (shotingGold)
        {
            Instantiate(goldBullet, firePoint.position, transform.rotation);
        }
        else
        {
            
            if (weaponType == WeaponType.Shotgun)
            {
                Instantiate(bullet, firePoint.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, gameObject.transform.parent.eulerAngles.z + 7f));
                Instantiate(bullet, firePoint.position, transform.rotation);
                Instantiate(bullet, firePoint.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, gameObject.transform.parent.eulerAngles.z + -7f));
            }
            else
                Instantiate(bullet, firePoint.position, transform.rotation);
        }
        if (weaponType != WeaponType.AK) //jeśli nie AK to odejmuje ammo
        { 
            ammo--;
            main.SetAmmo(ammo+"");
        }
        yield return new WaitForSeconds(delayBetweenShots);
    }
    void OnDisable()
    {
        isShooting = false;
    }

    public void AddAmmo()
    {
        ammo += ammoPickup;
        main.SetAmmo(ammo+"");

    }
    public void ResetAmmo()
    {
        ammo = startingAmmo;
    }
}

