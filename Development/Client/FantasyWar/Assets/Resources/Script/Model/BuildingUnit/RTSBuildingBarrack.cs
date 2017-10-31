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
        ActionBehaviour carProduction = gameObject.AddComponent<Action_ProductCar>();
		ActionList.Add (carProduction);
		//
        if (!playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1102).type].Contains(this)) {
            playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1102).type].Add(this);
        }
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
