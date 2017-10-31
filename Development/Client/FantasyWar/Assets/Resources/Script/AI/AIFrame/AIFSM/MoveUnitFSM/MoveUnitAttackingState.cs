using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitAttackingState : MoveUnitFSMState {

	RTSGameUnit AttackTarget = null;

	public MoveUnitAttackingState(MoveUnitAIController AICon)
	{
		StateID = MoveUnitFSMStateID.Attacking;
        this.AIController = AICon;
    }

	public override void SwitchIn ()
	{
		
	}

	public override void SwitchOut ()
	{
        base.SwitchOut();
        AttackTarget = null;
	}

	//用来确定是否需要转到其他状态
	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy,myself);
		if (enemy == null)
			return;
		
		//AICon.DesPos = enemy.position;

		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(myself.position, enemy.position);
		if (dist >= attackDistance)
		{
			//Debug.Log("Switch to Chase state");
			myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.SawEnemy);
			//AttackTarget = null;
		}
		//Go back to patrol is it become too far
		else if (dist >= chaseDistance)
		{
			Debug.Log("Switch to Idle state");
			//AttackTarget = null;
			myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.LostEnemy);
		}
	}

	public override void Act(Transform enemy, Transform myself)
	{
		//如果攻击目标和传进的目标不同，就A那个目标
		if (enemy == null)
			return;
		if (enemy.GetComponent<RTSGameUnit>() != AttackTarget) {
            if(myself.GetComponent<Action_Attack>().attackDelegate==null)
            {
                Debug.Log("攻击委托为空");
            }
                myself.GetComponent<Action_Attack> ().attackDelegate (enemy.GetComponent<RTSGameUnit> ());
			AttackTarget = enemy.GetComponent<RTSGameUnit>();
		}
	}

}
