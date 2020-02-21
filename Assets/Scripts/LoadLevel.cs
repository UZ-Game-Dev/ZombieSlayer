using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    private Main main;
    private void Start()
    {
        main = FindObjectOfType<Main>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
          
            StartCoroutine(main.LoadLevelAsync(SceneManager.GetActiveScene().buildIndex+1));
        }

    }
}
