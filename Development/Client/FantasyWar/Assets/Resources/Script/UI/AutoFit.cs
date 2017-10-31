using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFit : MonoBehaviour {

	private float scaleFactor = 1;

	private RectTransform mapRect;
	public int width;
	public int hight;

	void Start(){
		mapRect = GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update () {
		scaleFactor = Screen.width / 1024f;
		mapRect.sizeDelta = new Vector2(width * scaleFactor, hight * scaleFactor);
	}
}
