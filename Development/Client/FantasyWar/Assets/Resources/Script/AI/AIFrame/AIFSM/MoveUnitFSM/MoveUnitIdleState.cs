using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitIdleState : MoveUnitFSMState {

	public MoveUnitIdleState(MoveUnitAIController AICon)
	{
        this.AICon = AICon;
        StateID = MoveUnitFSMStateID.Idle;
	}

	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy, myself);
        if (enemy == null)
			return;

        AICon.DesPos = enemy.position;

		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(myself.position, AICon.DesPos);

		//Go back to patrol is it become too far
		if (dist <= chaseDistance)
		{
			Debug.Log("Switch to Chase state");
			myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.SawEnemy);
		}
	}

	public override void Act (Transform player, Transform npc)
	{
		return;
	}


	public override void SwitchIn ()
	{

	}

	public override void SwitchOut ()
	{

	}
}
