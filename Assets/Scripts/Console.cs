using System;
using System.Collections;
using UnityEngine;

/*
 * copyright © 2020,
 * Przemysław (Przemwo) Woźny,
 * All rights reserved.
 * xD
 * */

public class Console : MonoBehaviour
{

    private Main _main;
    private PlayerMovement _playerMovement;
    private WeaponMenager _weaponMenager;
    private SaveData _saveData;
    private bool _consoleActive = false;
    private string _stringCode = "";

    void Start()
    {
        _main = (Main)FindObjectOfType(typeof(Main));
        _playerMovement = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        _weaponMenager = (WeaponMenager)FindObjectOfType(typeof(WeaponMenager));
        _saveData = (SaveData)FindObjectOfType(typeof(SaveData));
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab)) _consoleActive = true;
    }

    private void OnGUI()
    {
        if (_consoleActive)
        {
            GUI.FocusControl("MyTextField");
            GUI.SetNextControlName("MyTextField");
            _stringCode = GUI.TextField(new Rect(10, 0.07f*Screen.height, 200, 20), _stringCode, 25);

            switch (Event.current.keyCode)
            {
                case KeyCode.Escape:
                    _consoleActive = false;
                    break;
                case KeyCode.Return:
                case KeyCode.KeypadEnter:
                    _consoleActive = false;
                    string[] arrayCode = _stringCode.Split(' ');
                    string text="";
                    try
                    {
                        switch (arrayCode[0].ToLower())
                        {
                            case "hp":
                                _playerMovement.health = _playerMovement.health + int.Parse(arrayCode[1]);
                                _main.setHealth(_playerMovement.health);
                                text = "+" + arrayCode[1] + " HP";
                                break;
                            case "inv":
                                _playerMovement.Pickup(Types.Inv_PU);
                                text = "invincible activated";
                                break;
                            case "ammo":
                                _playerMovement.Pickup(Types.Ammunition);
                                text = "Add ammunition";
                                break;
                            case "freeze":
                                _playerMovement.Pickup(Types.Freeze_PU);
                                text = "Freeze zombie activated";
                                break;
                            case "goldbullets":
                                _playerMovement.Pickup(Types.GoldBullets_PU);
                                text = "Gold bullets activated";
                                break;
                            case "nuke":
                                _playerMovement.Pickup(Types.Nuke_PU);
                                text = "Nuke activated";
                                break;
                            case "scare":
                                _playerMovement.Pickup(Types.Scare_PU);
                                text = "Scare activated";
                                break;
                            case "speed":
                                _playerMovement.Pickup(Types.Speed_PU);
                                text = "Speed activated";
                                break;
                            case "ak47":
                                _weaponMenager.ChangeWeapon(0);
                                text = "AK-47 activated";
                                break;
                            case "m4":
                                _weaponMenager.ChangeWeapon(1);
                                text = "M4 activated";
                                break;
                            case "shotgun":
                                _weaponMenager.ChangeWeapon(2);
                                text = "Shotgun activated";
                                break;
                            case "smg":
                                _weaponMenager.ChangeWeapon(3);
                                text = "SMG activated";
                                break;
                            case "bosson":
                                _playerMovement.health = 1000;
                                _main.setHealth(_playerMovement.health);
                                _playerMovement.Pickup(Types.Nuke_PU);
                                StartCoroutine("Boss");
                                text = "BOSS activated!";
                                break;
                            case "bossoff":
                                _playerMovement.health = 100;
                                _main.setHealth(_playerMovement.health);
                                StopCoroutine("Boss");
                                text = "BOSS disabled";
                                break;
                            case "timespeed":
                                Time.timeScale = float.Parse(arrayCode[1]);
                                text = "time speed = " + float.Parse(arrayCode[1]);
                                break;
                            default:
                                _stringCode = "";
                                return;
                        }
                        if (_saveData.fraudsIsActivated == false)
                        {
                            Destroy(GameObject.Find("Input_window backup"));
                            _saveData.fraudsIsActivated = true;
                        }
                        _playerMovement.playSound();
                        Debug.Log(text);
                        break;
                    }
                    catch (Exception)
                    {
                        _stringCode = "";
                        return;
                    }
            }
        }
    }

    IEnumerator Boss()
    {
        while (true)
        {
            _playerMovement.Pickup(Types.Inv_PU);
            _playerMovement.Pickup(Types.Speed_PU);
            _playerMovement.Pickup(Types.GoldBullets_PU);
            yield return new WaitForSeconds(5);
        }
    }
}
