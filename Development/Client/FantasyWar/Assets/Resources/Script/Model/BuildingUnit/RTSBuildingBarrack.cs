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

	public void CreatArmy(int ID){
		switch (ID) {
		case 1002:
			gameObject.GetComponent<Action_ProductionRider> ().RunAction (KeyCode.A);
			break;
		default:
			break;
		}

	}
}
