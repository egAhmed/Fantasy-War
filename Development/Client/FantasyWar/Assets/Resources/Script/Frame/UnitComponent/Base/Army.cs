using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : UnitInfo {

	protected override void Start ()
	{
		base.Start ();
		UnitManager.ShareInstance.Buildings[belong].Add (gameObject);
		if(belong.IsCurrentPlayer){
			Interaction m = gameObject.AddComponent<Move> ();
			interactionList.Add (m);
		}
	}

}
