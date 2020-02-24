using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    N,
    E,
    S,
    W
}
public class LoadLevel : MonoBehaviour
{

    public Direction direction;
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
            main.SaveData(direction);
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                StartCoroutine(main.LoadLevelAsync(2));
            }
            else
            {
                StartCoroutine(main.LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }
    }
}
