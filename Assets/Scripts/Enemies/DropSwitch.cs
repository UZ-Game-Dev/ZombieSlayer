using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSwitch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropZone")
        {
            GetComponent<ZombieDef>().canDrop = true;
        }
    }
}
