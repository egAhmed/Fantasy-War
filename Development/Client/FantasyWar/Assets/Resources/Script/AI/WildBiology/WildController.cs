using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildController : MonoBehaviour {

	public WildFSMState state = null;
	public Vector3 oriPos;


	void Start(){
		gameObject.GetComponent<UnitBloodBar> ().enabled = true;
		oriPos = transform.position;
		state = new WildIdle (this);
	}

	void Update () {
		//Debug.Log (state.GetType());
		state.ActUpdate ();
		state.Reason ();
	}
}
