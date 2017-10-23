using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitMoveState : MoveUnitFSMState {

	public MoveUnitMoveState(MoveUnitAIController AICon)
	{
		StateID = MoveUnitFSMStateID.Move;
        this.AICon = AICon;
    }

	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy, myself);
        float dist = Vector3.Distance(myself.position, AICon.DesPos);
		if (dist <= 2) {
			Debug.Log ("Switch to Idle state");
			myself.GetComponent<MoveUnitAIController> ().SetTransition (MoveUnitFSMTransition.LostEnemy);
		}
	}

	public override void Act(Transform enemy, Transform myself)
	{
		//MoveUnitAIController AICon = myself.GetComponent<MoveUnitAIController>();
		//if (MoveUnitAIController.AIMove != null)
  //          MoveUnitAIController.AIMove(AICon.DesPos);
	}


	public override void SwitchIn ()
	{

	}

	public override void SwitchOut ()
	{

	}
}
