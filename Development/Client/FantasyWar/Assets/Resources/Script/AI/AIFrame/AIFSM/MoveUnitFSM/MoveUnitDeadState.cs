using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在act中还原追逐范围
/// </summary>
public class MoveUnitDeadState : MoveUnitFSMState {

	public MoveUnitDeadState(){
		StateID = MoveUnitFSMStateID.Dead;
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
