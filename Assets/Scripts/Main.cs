using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public int scoreOverall = 0;

    public Text scoreText;
    public Text endScoreText;
    public Text healthText;
    public Text ammoText;
    public static bool GameOver = false;
    public GameObject gameoverUI;
    public Transform[] PlayerSpawnPoints;


    [Header("Temp save")]
    public int health;
    public int ammo;
    public int currentWeapon;
    Direction direction;

    private void Start()
    {
        LoadData();
    }


    public void setHealth(int health)
    {
        if (health <= 0)
        {
            if (GameOver)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            health = 0;
            //StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
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
        FindObjectOfType<SaveData>().setData(health, ammo, scoreOverall, currentWeapon, direction);
    }

    public void LoadData()
    {
        SaveData sd = FindObjectOfType<SaveData>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().health = sd.health;
        scoreOverall = sd.score;
        scoreText.text = "Score: " + scoreOverall;
        player.GetComponentInChildren<WeaponMenager>().ChangeWeapon(sd.currentWeapon);
        player.GetComponentInChildren<WeaponDef>().ammo = sd.ammo;
        SetAmmo(sd.ammo);
        
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
}
