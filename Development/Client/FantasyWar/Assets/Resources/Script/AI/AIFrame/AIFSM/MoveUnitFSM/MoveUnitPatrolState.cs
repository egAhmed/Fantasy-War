using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitPatrolState : MoveUnitFSMState {

	RTSGameUnit AttackTarget = null;

	public MoveUnitPatrolState(){
		StateID = MoveUnitFSMStateID.Patrol;
	}

	public override void SwitchIn ()
	{

	}

	public override void SwitchOut ()
	{
		AttackTarget = null;
	}

	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy, myself);
        if (enemy == null)
			return;

		destPos = enemy.position;

		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(myself.position, destPos);
		if (dist <= attackDistance)
		{
			Debug.Log("Switch to Attack state");
			myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.ReachEnemy);
			//AttackTarget = null;
		}
	}

	public override void Act(Transform enemy, Transform myself)
	{
		destPos = enemy.position;

		if (AttackTarget == null) {
			foreach (PlayerInfo playerinfo in PlayerInfoManager.ShareInstance.Players) {
				if (playerinfo != myself.GetComponent<RTSGameUnit> ().playerInfo) {
					foreach (string item in playerinfo.ArmyUnits.Keys) {
						foreach (RTSGameUnit target in playerinfo.ArmyUnits[item]) {
							if (Vector3.Distance(target.transform.position, myself.position) < attackDistance) {
								AttackTarget = target;
								myself.GetComponent<Action_Attack> ().attackDelegate (target);
								return;
							}
						}
					}
				}
			}
		}

		//没有找到要打的敌人，继续移动
		MoveUnitAIController AICon = myself.GetComponent<MoveUnitAIController>();
		if (AICon.AIMove != null)
			AICon.AIMove(destPos);
	}
}
