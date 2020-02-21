using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	Vector3 localScale;
    private ZombieDef zombieDef;
    public float healthMax;
    public float health;

	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
        zombieDef = GetComponentInParent<ZombieDef>();
        healthMax = zombieDef.health;
	}
	
	// Update is called once per frame
	void Update () {
        health = zombieDef.health;
		localScale.x = health / healthMax;
		transform.localScale = localScale;
	}
}
