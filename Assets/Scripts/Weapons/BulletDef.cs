using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDef : MonoBehaviour
{
	public float speed;
    public int damageOnHit;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

   void OnTriggerEnter2D(Collider2D collision)
    {
        ZombieDef zombie = collision.GetComponent<ZombieDef>();
        if (zombie != null)
        {
            zombie.TakeDamage(damageOnHit);
        }
		Destroy(gameObject);
    }
}
