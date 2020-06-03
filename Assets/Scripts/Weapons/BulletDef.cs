using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDef : MonoBehaviour
{
	public float speed;
    public int damageOnHit;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (onScreen == false)
        {
            Destroy(gameObject, 0.05f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) // 9 - enemy
        {
            collision.SendMessage("TakeDamage",damageOnHit);
        }
		Destroy(gameObject);
    }
}