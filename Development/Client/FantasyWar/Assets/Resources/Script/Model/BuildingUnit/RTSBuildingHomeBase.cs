using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSBuildingHomeBase : RTSBuilding {
	//
	protected override void actionBehaviourInit() {
		///
		base.actionBehaviourInit();
		//
		ActionBehaviour apro = gameObject.AddComponent<Action_Production> ();
		ActionList.Add (apro);
		//
	}
}
