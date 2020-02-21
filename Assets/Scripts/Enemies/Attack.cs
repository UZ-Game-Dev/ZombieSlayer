using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    private void Start()
    {
        damage = GetComponentInParent<ZombieDef>().damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

    }
}
