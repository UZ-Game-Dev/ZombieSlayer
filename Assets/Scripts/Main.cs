using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text healthText;


    public void setHealth(int health)
    {
        if (health < 0) 
        { 
            health = 0;
            StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
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
}
