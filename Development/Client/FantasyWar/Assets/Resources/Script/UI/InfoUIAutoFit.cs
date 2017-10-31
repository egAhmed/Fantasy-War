using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUIAutoFit : MonoBehaviour {
    private float scaleFactor = 1;

    private RectTransform mapRect;
    public int width;
    public int hight;
    public Vector3 origin;
    void Start()
    {
        mapRect = GetComponent<RectTransform>();
        origin = mapRect.position;

    }

    // Update is called once per frame
    void Update()
    {
        scaleFactor = Screen.width / 1024f;
        mapRect.sizeDelta = new Vector2(width * scaleFactor, hight * scaleFactor);
        mapRect.position = origin * scaleFactor;
    }
}
