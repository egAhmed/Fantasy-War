using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSBuildingBarrack : RTSBuilding {
	//
	protected override void actionBehaviourInit() {
		///
		base.actionBehaviourInit();
		//
		ActionBehaviour apr = gameObject.AddComponent<Action_ProductionRider> ();
		ActionList.Add (apr);
		//
	}
}
