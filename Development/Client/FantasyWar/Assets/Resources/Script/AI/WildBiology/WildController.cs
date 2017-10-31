using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildController : MonoBehaviour {

	public WildFSMState state = null;
	public Vector3 oriPos;

	void Start(){
		oriPos = transform.position;
		state = new WildIdle (this);
	}

	void Update () {
		state.ActUpdate ();
		state.Reason ();
	}
}
