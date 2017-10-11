using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitFogController : MonoBehaviour {
    FOWRevealer fowRevealer;
    public bool HaveVision = true ;
	// Use this for initialization
	void Start () {
        fowRevealer = gameObject.AddComponent<FOWRevealer>();
        fowRevealer.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (HaveVision) {
			// Debug.Log ("视野开启"); 
		} else {
			fowRevealer.enabled = false ;
			// Debug.Log ("视野关闭");
		}
	}
}
