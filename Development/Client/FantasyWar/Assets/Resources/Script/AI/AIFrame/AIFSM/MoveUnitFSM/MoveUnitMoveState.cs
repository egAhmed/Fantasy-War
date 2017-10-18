using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitMoveState : MoveUnitFSMState {

	public MoveUnitMoveState()
	{
		StateID = MoveUnitFSMStateID.Move;

	}

	public override void Reason(Transform enemy, Transform myself)
	{
		float dist = Vector3.Distance(myself.position, destPos);
		if (dist <= 2) {
			Debug.Log ("Switch to Idle state");
			myself.GetComponent<MoveUnitAIController> ().SetTransition (MoveUnitFSMTransition.LostEnemy);
		}
	}

	public override void Act(Transform enemy, Transform myself)
	{
		MoveUnitAIController AICon = myself.GetComponent<MoveUnitAIController>();
		if (AICon.AIMove != null)
			AICon.AIMove(destPos);
	}


	public override void SwitchIn ()
	{

	}

	public override void SwitchOut ()
	{

	}
}
