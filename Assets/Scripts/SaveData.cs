using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public int health;
    public int ammo;
    public int score;
    public int currentWeapon;

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

    public void setData(int health, int ammo, int score, int currentWeapon)
    {
        SaveData.Instance.health = health;
        SaveData.Instance.ammo = ammo;
        SaveData.Instance.score = score;
        SaveData.Instance.currentWeapon = currentWeapon;
    }

    public void LoadData()
    {
        health = SaveData.Instance.health;
        ammo = SaveData.Instance.ammo;
        score = SaveData.Instance.score;
        currentWeapon = SaveData.Instance.currentWeapon;

    }
}
