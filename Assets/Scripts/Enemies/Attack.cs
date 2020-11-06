using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    private ZombieDef _parent;

    private void Start()
    {
        damage = GetComponentInParent<ZombieDef>().damage;
        _parent = GetComponentInParent<ZombieDef>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_parent.GetTimeEffect < 0)
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
