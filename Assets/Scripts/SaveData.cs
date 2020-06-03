using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public int lastLevel;

    public int health;
    public int ammo;
    public int score;
    public int currentWeapon;
    public Direction direction;
    public bool fraudsIsActivated;
    /*
    public int zombieScore;
    public int zombieDamage;
    public int zombieHealth;
    public float zombieSpeed;
    public int zombieCounter;
    public float timeBetwSpawn;
    */

    public static SaveData Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void setData(int currentLevel, int health, int ammo, int score, int currentWeapon, Direction direction/*, int zombieScore, int zombieDamage, int zombieHealth, float zombieSpeed, int zombieCount, float timeBetwSpawn*/ )
    {
        SaveData.Instance.lastLevel = currentLevel;

        SaveData.Instance.health = health;
        SaveData.Instance.ammo = ammo;
        SaveData.Instance.score = score;
        SaveData.Instance.currentWeapon = currentWeapon;
        SaveData.Instance.direction = direction;
        //zombie save
        /* wszystko bazuje na current lvl, ale może kiedyś bedzie potrzebne
        SaveData.Instance.zombieScore = zombieScore;
        SaveData.Instance.zombieDamage = zombieDamage;
        SaveData.Instance.zombieHealth = zombieHealth;
        SaveData.Instance.zombieSpeed = zombieSpeed;
        SaveData.Instance.zombieCounter = zombieCount;
        SaveData.Instance.timeBetwSpawn = timeBetwSpawn;
        */
}
/* 
    public void LoadData()
    {
        health = SaveData.Instance.health;
        ammo = SaveData.Instance.ammo;
        score = SaveData.Instance.score;
        currentWeapon = SaveData.Instance.currentWeapon;

    }
    */
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
