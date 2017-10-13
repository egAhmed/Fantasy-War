using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Army {

	protected override void Start ()
	{
		base.Start ();
		unitName = "农民";
		HP = 10;
		maxHP = 11;
		if(belong.IsCurrentPlayer){
			ActionBehaviour ac = gameObject.AddComponent<Action_Collect> ();
			ActionList.Add (ac);
		}

	}
}
