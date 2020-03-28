using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    private PlayerMovement sp;
    public GameObject pasueMenuUI;

    // Update is called once per frame
    void Start()
    {
        sp = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

     public void Resume()
    {
        pasueMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        if (sp.health <= 0)
        {

            Time.timeScale = 0f;

        }
    }

    void Pause()
    {
        pasueMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;

    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
         Application.Quit();
    }
}
