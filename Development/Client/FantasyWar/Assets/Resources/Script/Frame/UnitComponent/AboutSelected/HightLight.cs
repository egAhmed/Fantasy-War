using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight : Interaction {

	public override void Select ()
	{
		transform.GetChild (0).gameObject.SetActive (true);
	}

	public override void Deselect ()
	{
		transform.GetChild (0).gameObject.SetActive (false);
	}

	void Start(){
		Deselect ();
	}

}
