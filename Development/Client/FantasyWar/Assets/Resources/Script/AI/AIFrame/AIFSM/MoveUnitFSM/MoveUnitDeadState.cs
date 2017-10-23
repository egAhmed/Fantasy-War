using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在act中还原追逐范围
/// </summary>
public class MoveUnitDeadState : MoveUnitFSMState {

	public MoveUnitDeadState(MoveUnitAIController AICon)
    {
		StateID = MoveUnitFSMStateID.Dead;
        this.AICon = AICon;
    }

	public override void Act (Transform enemy, Transform myself)
	{
		
	}

	public override void Reason (Transform enemy, Transform myself)
	{
		
	}

	public override void SwitchIn ()
	{

	}

	public override void SwitchOut ()
	{

	}
}
