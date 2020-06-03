using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float timeToPing;
    public float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToPing)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * 4, 1f)+0.25f);
        }
    }
}