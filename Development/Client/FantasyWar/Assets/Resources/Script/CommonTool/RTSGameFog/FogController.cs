using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
	public bool HaveVision = true ;
	// Use this for initialization
	void Start () {
		GameObject.Find ("Hero").GetComponent<FOWRevealer> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (HaveVision) {
			
			Debug.Log ("视野开启"); 
		} else {
			GameObject.Find ("Hero").GetComponent<FOWRevealer> ().enabled = false ;
			Debug.Log ("视野关闭");
		}
	}
}
