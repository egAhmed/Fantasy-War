using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitIdleState : MoveUnitFSMState {
    RTSGameUnit AttackTarget = null;
    public MoveUnitIdleState(MoveUnitAIController AICon)
	{
        this.AIController = AICon;
        StateID = MoveUnitFSMStateID.Idle;
	}

	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy, myself);
        if (enemy == null)
			return;

        //AICon.DesPos = enemy.position;

		//Check the distance with player tank
		//When the distance is near, transition to attack state
		float dist = Vector3.Distance(myself.position, AIController.DesPos);

		//Go back to patrol is it become too far
		if (dist <= chaseDistance)
		{
			Debug.Log("Switch to Chase state");
			myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.SawEnemy);
		}
	}

	public override void Act (Transform enemy, Transform myself)
	{
        if (AttackTarget == null)
        {
            //遍历所有玩家
            foreach (PlayerInfo playerinfo in PlayerInfoManager.ShareInstance.Players)
            {
                //如果不是自己
                if (playerinfo.groupTeam != myself.GetComponent<RTSGameUnit>().playerInfo.groupTeam)
                {
                    //遍历所有单位
                    foreach (string item in playerinfo.ArmyUnits.Keys)
                    {
                        foreach (RTSGameUnit target in playerinfo.ArmyUnits[item])
                        {
                            if (Vector3.Distance(target.transform.position, myself.position) < attackDistance)
                            {
                                AttackTarget = target;
                                AIController.DesPos = AttackTarget.transform.position;
                                myself.GetComponent<Action_Attack>().attackDelegate(target);
                                return;
                            }
                        }
                    }

                    AIController.DesPos = enemy.position;

                }
            }
        }
        return;
	}


	public override void SwitchIn ()
	{

	}

	public override void SwitchOut ()
	{
        base.SwitchOut();
    }
}
