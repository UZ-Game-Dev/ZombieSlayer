﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
		        Application.Quit();
        #endif
    }
}