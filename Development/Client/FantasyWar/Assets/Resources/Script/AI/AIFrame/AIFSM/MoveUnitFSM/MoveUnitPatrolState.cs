using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitPatrolState : MoveUnitFSMState {

	RTSGameUnit AttackTarget = null;

	public MoveUnitPatrolState(MoveUnitAIController AICon)
    {
		StateID = MoveUnitFSMStateID.Patrol;
        this.AICon = AICon;
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

        AICon.DesPos = enemy.position;

		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(myself.position, AICon.DesPos);
		if (dist <= attackDistance)
		{
			Debug.LogError("Switch to Attack state");
			myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.ReachEnemy);
			//AttackTarget = null;
		}
	}

	public override void Act(Transform enemy, Transform myself)
	{
		AICon.DesPos = enemy.position;

		if (AttackTarget == null) {
			//遍历所有玩家
			foreach (PlayerInfo playerinfo in PlayerInfoManager.ShareInstance.Players) {
				//如果不是自己
				if (playerinfo != myself.GetComponent<RTSGameUnit> ().playerInfo) {
					//遍历所有单位
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
		//MoveUnitAIController AICon = myself.GetComponent<MoveUnitAIController>();
		//Debug.LogError (MoveUnitAIController.AIMove == null);
		//Debug.Log(AICon.GetComponent<RTSGameUnit>().GetType());
		//if (MoveUnitAIController.AIMove != null) {
		//	if(AICon.GetComponent<RTSWorker>()==null)
		//	MoveUnitAIController.AIMove (AICon.DesPos);
		//}
	}
}
