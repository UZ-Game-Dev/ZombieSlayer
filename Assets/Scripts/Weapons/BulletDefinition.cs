using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDefinition : MonoBehaviour
{
	public float speed;
    public int damageOnHit;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

   void OnTriggerEnter2D(Collider2D collision)
    {
		Destroy(gameObject);
    }
}
