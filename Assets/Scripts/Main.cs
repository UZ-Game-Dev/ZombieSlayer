using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text healthText;
    public static bool GameOver = false;

    public GameObject gameoverUI;


    public void setHealth(int health)
    {
        if (health < 0) 
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


    IEnumerator LoadLevelAsync(int nr)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }




}
