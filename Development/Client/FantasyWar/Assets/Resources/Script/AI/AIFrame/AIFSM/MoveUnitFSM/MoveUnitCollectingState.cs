using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitCollectingState : MoveUnitFSMState {

	public RTSResource nearestMine = null;
	RTSResource currentMine = null;

	public MoveUnitCollectingState(MoveUnitAIController AICon)
	{
		StateID = MoveUnitFSMStateID.Collect;
        this.AICon = AICon;
    }

	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy, myself);
        return;
	}

	public override void Act(Transform enemy, Transform myself)
	{
		//当前挖的为空  或者  当前挖的矿不是最近的矿
		if (currentMine == null ||(nearestMine != null && nearestMine != currentMine)) {
			Debug.Log ("挖矿");
			currentMine = nearestMine;
			myself.GetComponent<Action_Collect> ().collectDelegate (nearestMine);
		}
	}

	public void FindNearMine(Transform myself){
		//现在还没有矿物的矿石的列表
		Debug.Log("寻找最近的矿");
		foreach (RTSResource item in PlayerInfoManager.ShareInstance.resourceses) {
			if (nearestMine == null) {
				nearestMine = item;
			}
			else {
				if (Vector3.Distance (myself.position, item.transform.position) < Vector3.Distance (myself.position, nearestMine.transform.position)) {
					nearestMine = item;
				}
			}
		}
	}

	public override void SwitchIn ()
	{
		FindNearMine (AICon.transform);
	}

	public override void SwitchOut ()
	{
		currentMine = null;
		Debug.Log ("不挖矿了");
	}
}
