using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private float healthMax;

    public void SetHealthMax(float _healthMax) => healthMax = _healthMax;

    public void UpdateHealthBar(float _health)
    {
        transform.localScale = new Vector3(_health/healthMax, transform.localScale.y, transform.localScale.z);
    }
}