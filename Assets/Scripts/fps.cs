using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fps : MonoBehaviour {
	private float _frameCount;
	private float _dt;
	private float _fps;
	private float _updateRate = 4.0f;
    private bool view = false;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = this.GetComponent<TextMeshProUGUI>();
    }

    void Update (){
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (view) view = false;
            else view = true;
        }

        if (view)
        {
            _frameCount++;
            _dt += Time.deltaTime;
            if (_dt > 1.0f / _updateRate)
            {
                _fps = _frameCount / _dt;
                _frameCount = 0;
                _dt -= 1.0f / _updateRate;
            }
        }
    }

    void OnGUI()
    {
        if (view)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "FPS: " + _fps.ToString("0"));
        }
    }
}
