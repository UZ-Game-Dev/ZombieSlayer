using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponDef : MonoBehaviour
{
    public enum WeaponType { pistol, shotgun, rifle };
    public bool isShooting = false;
    [Header("Definiowanie Dynamiczne")]
    public WeaponType weaponType;
    public Transform firePoint;
    public GameObject bullet;
    public float delayBetweenShots;

    private void Start()
    {
        isShooting = false;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && !isShooting && Time.timeScale != 0)
        {
            StartCoroutine(ShootOrder());
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
        yield return new WaitForSeconds(delayBetweenShots);
    }
    void OnDisable()
    {
        isShooting = false;
    }
}

