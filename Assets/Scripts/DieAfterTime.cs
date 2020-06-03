using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
    public float time;

    private void Update()
    {
        Destroy(gameObject, time);
    }
}