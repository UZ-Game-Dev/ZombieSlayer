using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public int currentLevel = 0;
    public int scoreOverall = 0;
    public Text name_input;
    public Text levelText;
    public Text scoreText;
    public Text endScoreText;
    public Text healthText;
    public Text ammoText;
    public static bool GameOver = false;
    public GameObject gameoverUI;
    public Transform[] PlayerSpawnPoints;

    [Header("Temp save")]
    private ZombieDef zombieDef;
    public int health;
    public int ammo;
    public int currentWeapon;
    Direction direction;

    public int zombieScore_current;
    public int zombieDamage_current;
    public int zombieHealth_current;
    public float zombieSpeed_current;
    public int zombieCounter_current;
    public float timeBetwSpawn_current;

    private void Start()
    {
        LoadData();
    }


    public void setHealth(int health)
    {
        if (health <= 0)
        {
            health = 0;
            Pause();
        }
        healthText.text = "Health: " + health;
    }

    public void addScore(int score)
    {
        this.scoreOverall += score;
        scoreText.text = "Score: " + this.scoreOverall;
        endScoreText.text = "" + scoreOverall;
        
    }
    public void SetAmmo(int ammo)
    {
        ammoText.text = "" + ammo;
    }


    public IEnumerator LoadLevelAsync(int nr)
    {
        AsyncOperation loadLevelAsync = SceneManager.LoadSceneAsync(nr);

        //wait until loaded
        while (!loadLevelAsync.isDone)
        {
            yield return null;
        }
    }


    public void Resume()
    {
        gameoverUI.SetActive(false);
        Time.timeScale = 1f;
        GameOver = false;
    }


    void Pause()
    {
        gameoverUI.SetActive(true);
        Time.timeScale = 0f;
        GameOver = true;

    }
    public void RetryGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<SaveData>().Destroy();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SaveData(Direction direction)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<PlayerMovement>().health;
        ammo = player.GetComponentInChildren<WeaponDef>().ammo;
        currentWeapon = player.GetComponentInChildren<WeaponMenager>().currentWeapon;
        //get stats from origin zombie
        zombieDef = GameObject.FindGameObjectWithTag("OriginZombie").GetComponent<ZombieDef>();
        zombieScore_current = zombieDef.score;
        zombieDamage_current = zombieDef.damage;
        zombieHealth_current = zombieDef.health;
        zombieSpeed_current = zombieDef.speed;
        //get stats from zombie spawner
        ZombieSpawner zombieSpawner = FindObjectOfType<ZombieSpawner>();
        zombieCounter_current = zombieSpawner.zombieCounter;
        timeBetwSpawn_current = zombieSpawner.timeBetwSpawn;
        //save
        FindObjectOfType<SaveData>().setData(currentLevel, health, ammo, scoreOverall, currentWeapon, direction/*, zombieScore_Current, zombieDamage_current, zombieHealth_current, zombieSpeed_current, zombieCounter_current, timeBetwSpawn_current*/);
    }

    public void LoadData()
    {
        SaveData sd = FindObjectOfType<SaveData>();

        //multiply stats
        currentLevel = sd.lastLevel + 1;
        zombieScore_current = 10;
        zombieDamage_current = (int)(10f + ((1f - (15f / ((float)currentLevel + 15f))) * (35f - 10f)));
        zombieHealth_current = (int)(10f + ((1f - (15f / ((float)currentLevel + 15f))) * (35f - 10f)));
        zombieSpeed_current = (1.6f + ((1.0f - (5.0f / ((float)currentLevel + 5.0f))) * (2.5f - 1.6f)));
        zombieCounter_current = (int)(10f + ((1f - (35f / ((float)currentLevel + 35f))) * (200f - 10f)));
        timeBetwSpawn_current = (1f + ((1f - (15f / ((float)currentLevel + 15f))) * (0.4f - 1f)));

        levelText.text = "Level " + currentLevel;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().health = sd.health;
        scoreOverall = sd.score;
        scoreText.text = "Score: " + scoreOverall;
        player.GetComponentInChildren<WeaponMenager>().ChangeWeapon(sd.currentWeapon);
        player.GetComponentInChildren<WeaponDef>().ammo = sd.ammo;
        SetAmmo(sd.ammo);
        //load stats into origin zombie
        ZombieDef zombieDef = GameObject.FindGameObjectWithTag("OriginZombie").GetComponent<ZombieDef>();
        zombieDef.score = zombieScore_current;
        zombieDef.damage = zombieDamage_current;
        zombieDef.health = zombieHealth_current;
        zombieDef.speed = zombieSpeed_current;
        //assign buffed orgin zombie to spawner
        FindObjectOfType<ZombieSpawner>().enemy = GameObject.FindGameObjectWithTag("OriginZombie");
        //loead stats into zombie spawner
        ZombieSpawner zombieSpawner = FindObjectOfType<ZombieSpawner>();
        zombieSpawner.zombieCounter = zombieCounter_current;
        zombieSpawner.timeBetwSpawn = timeBetwSpawn_current;

        switch (sd.direction)
        {
            case Direction.N:
                player.transform.position = PlayerSpawnPoints[2].position;
                break;
            case Direction.E:
                player.transform.position = PlayerSpawnPoints[3].position;
                break;
            case Direction.S:
                player.transform.position = PlayerSpawnPoints[0].position;
                break;
            case Direction.W:
                player.transform.position = PlayerSpawnPoints[1].position;
                break;
            default:
                break;
        }
    }


public void sumbitScore()
    {
        if(!PlayerPrefs.HasKey("highscore1"))
        {
            PlayerPrefs.SetInt("highscore1", scoreOverall);
            PlayerPrefs.SetString("name1", name_input.text);
            PlayerPrefs.Save();
            
        }
        else
        {
            if (PlayerPrefs.GetInt("highscore1") < scoreOverall)
            {
                obniz(1);//przesuwa  pozostale wyniki w dół o 1
                PlayerPrefs.SetInt("highscore1", scoreOverall);
                PlayerPrefs.SetString("name1", name_input.text);
                PlayerPrefs.Save();


            }
            else
            {
                //highscore 2
                if (!PlayerPrefs.HasKey("highscore2"))
                {
                    PlayerPrefs.SetInt("highscore2", scoreOverall);
                    PlayerPrefs.SetString("name2", name_input.text);
                    PlayerPrefs.Save();

                }
                else
                {
                    if (PlayerPrefs.GetInt("highscore2") < scoreOverall)
                    {
                        obniz(2);
                        PlayerPrefs.SetInt("highscore2", scoreOverall);
                        PlayerPrefs.SetString("name2", name_input.text);
                        PlayerPrefs.Save();


                    }
                    else
                    {
                        //highscore3
                        if (!PlayerPrefs.HasKey("highscore3"))
                        {
                            PlayerPrefs.SetInt("highscore3", scoreOverall);
                            PlayerPrefs.SetString("name3", name_input.text);
                            PlayerPrefs.Save();

                        }
                        else
                        {
                            if (PlayerPrefs.GetInt("highscore3") < scoreOverall)
                            {
                                obniz(3);
                                PlayerPrefs.SetInt("highscore3", scoreOverall);
                                PlayerPrefs.SetString("name3", name_input.text);
                                PlayerPrefs.Save();


                            }
                            else
                            {
                                //highcore4
                                if (!PlayerPrefs.HasKey("highscore4"))
                                {
                                    PlayerPrefs.SetInt("highscore4", scoreOverall);
                                    PlayerPrefs.SetString("name4", name_input.text);
                                    PlayerPrefs.Save();

                                }
                                else
                                {
                                    if (PlayerPrefs.GetInt("highscore4") < scoreOverall)
                                    {
                                        obniz(4);
                                        PlayerPrefs.SetInt("highscore4", scoreOverall);
                                        PlayerPrefs.SetString("name4", name_input.text);
                                        PlayerPrefs.Save();


                                    }
                                    else
                                    {
                                        //higscore 5
                                        if (!PlayerPrefs.HasKey("highscore5"))
                                        {
                                            PlayerPrefs.SetInt("highscore5", scoreOverall);
                                            PlayerPrefs.SetString("name5", name_input.text);
                                            PlayerPrefs.Save();

                                        }
                                        else
                                        {
                                            if (PlayerPrefs.GetInt("highscore5") < scoreOverall)
                                            {

                                                PlayerPrefs.SetInt("highscore5", scoreOverall);
                                                PlayerPrefs.SetString("name5", name_input.text);
                                                PlayerPrefs.Save();


                                            }
                                            else
                                            {
                                                Debug.Log("Słabeusz");

                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }

                }
            }

        }

     

        
    }
    public void obniz(int poziom)
    {
        switch (poziom)
        {
            case 1:
                if (PlayerPrefs.HasKey("highscore4")) { PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4")); }
                if (PlayerPrefs.HasKey("highscore3")) { PlayerPrefs.SetInt("highscore4", PlayerPrefs.GetInt("highscore3")); }
                if (PlayerPrefs.HasKey("highscore2")) { PlayerPrefs.SetInt("highscore3", PlayerPrefs.GetInt("highscore2")); }
                if (PlayerPrefs.HasKey("highscore1")) { PlayerPrefs.SetInt("highscore2", PlayerPrefs.GetInt("highscore1")); }

                if (PlayerPrefs.HasKey("name4")) { PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4")); }
                if (PlayerPrefs.HasKey("name3")) { PlayerPrefs.SetString("name4", PlayerPrefs.GetString("name3")); }
                if (PlayerPrefs.HasKey("name2")) { PlayerPrefs.SetString("name3", PlayerPrefs.GetString("name2")); }
                if (PlayerPrefs.HasKey("name1")) { PlayerPrefs.SetString("name2", PlayerPrefs.GetString("name1")); }

        
                break;

            case 2:
                if (PlayerPrefs.HasKey("highscore4")) { PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4")); }
                if (PlayerPrefs.HasKey("highscore3")) { PlayerPrefs.SetInt("highscore4", PlayerPrefs.GetInt("highscore3")); }
                if (PlayerPrefs.HasKey("highscore2")) { PlayerPrefs.SetInt("highscore3", PlayerPrefs.GetInt("highscore2")); }

                if (PlayerPrefs.HasKey("name4")) { PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4")); }
                if (PlayerPrefs.HasKey("name3")) { PlayerPrefs.SetString("name4", PlayerPrefs.GetString("name3")); }
                if (PlayerPrefs.HasKey("name2")) { PlayerPrefs.SetString("name3", PlayerPrefs.GetString("name2")); }
                break;

            case 3:
                if (PlayerPrefs.HasKey("highscore4")) { PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4")); }
                if (PlayerPrefs.HasKey("highscore3")) { PlayerPrefs.SetInt("highscore4", PlayerPrefs.GetInt("highscore3")); }

                if (PlayerPrefs.HasKey("name4")) { PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4")); }
                if (PlayerPrefs.HasKey("name3")) { PlayerPrefs.SetString("name4", PlayerPrefs.GetString("name3")); }
                break;

            case 4:
                if (PlayerPrefs.HasKey("highscore4")) { PlayerPrefs.SetInt("highscore5", PlayerPrefs.GetInt("highscore4")); }

                if (PlayerPrefs.HasKey("name4")) { PlayerPrefs.SetString("name5", PlayerPrefs.GetString("name4")); }
                break;

            default:
                break;
        }

        PlayerPrefs.SetInt("highscore2", PlayerPrefs.GetInt("highscore1"));

    }
}
