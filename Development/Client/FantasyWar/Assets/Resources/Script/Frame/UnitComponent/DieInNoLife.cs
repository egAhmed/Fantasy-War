using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 没血死亡
/// </summary>
public class DieInNoLife : MonoBehaviour {

	//UnitInfo info;
    RTSGameUnit info;

    void Start () {
		//info = gameObject.GetComponent<UnitInfo> ();
		info = gameObject.GetComponent<RTSGameUnit> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (info.HP <= 0) {
			UnitDie ();
		}
	}

	void UnitDie(){
		//TODO
		//在对象池中隐藏
//		UnitInfo unitinfo = gameObject.GetComponent<UnitInfo>();
		RTSGameUnit unitinfo = gameObject.GetComponent<RTSGameUnit>();
		
		// if (unitinfo is Building) {
		if (unitinfo is RTSBuilding) {
			
			//gameObject.GetComponent<RTSGameUnit> ().playerInfo.BuildingUnits ["Base"].Remove (gameObject.GetComponent<RTSGameUnit> ());
		}
//		if (unitinfo is Building) {
		if (unitinfo is RTSBuilding) {
			
			//UnitManager.ShareInstance.Armys [unitinfo.playerInfo].Remove (gameObject);
		}
		else {
			return;
		}
	}
}
