using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private bool GamePaused = false;
    private PlayerMovement sp;
    public GameObject pasueMenuUI;
    public GameObject LeaveMenu;
    public GameObject LeaveGame;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Start()
    {
        sp = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !LeaveMenu.activeSelf && !LeaveGame.activeSelf)
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
        FindObjectOfType<SaveData>().Destroy();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
		Application.Quit();
    #endif
    }
}
