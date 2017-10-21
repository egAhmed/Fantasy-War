using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitCollectingState : MoveUnitFSMState {

	public RTSGameUnit nearestMine = null;
	RTSGameUnit currentMine = null;

	public MoveUnitCollectingState()
	{
		StateID = MoveUnitFSMStateID.Collect;

	}

	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy, myself);
        return;
	}

	public override void Act(Transform enemy, Transform myself)
	{
		if (currentMine == null ||(nearestMine != null && nearestMine != currentMine)) {
			myself.GetComponent<Action_Collect> ().collectDelegate (nearestMine);
			currentMine = nearestMine;
		}
	}

//	public RTSGameUnit FindNearMine(Transform myself){
//		现在还没有矿物的矿石的列表
//		foreach (RTSGameUnit item in MineList) {
//			if (Vector3.Distance (myself.position, item.transform.position) < Vector3.Distance (myself.position, nearestMine.transform.position)) {
//				nearestMine = item;
//			}
//		}
//	}

	public override void SwitchIn ()
	{
		//nearestMine = FindNearMine ();
	}

	public override void SwitchOut ()
	{
		currentMine = null;
	}
}
