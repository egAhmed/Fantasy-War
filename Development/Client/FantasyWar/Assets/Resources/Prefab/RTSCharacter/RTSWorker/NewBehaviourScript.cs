﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
			Action_Production ap = gameObject.GetComponent<Action_Production> ();
			ap.RunAction (KeyCode.Y);
		}
	}
}
