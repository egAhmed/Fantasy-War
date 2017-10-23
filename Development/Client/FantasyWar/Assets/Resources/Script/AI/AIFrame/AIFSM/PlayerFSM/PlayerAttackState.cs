using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerFSMState
{
	int population;

	PlayerInfo nearestPlayer;
	bool targetChange = false;

    public PlayerAttackState(PlayerAIController AICon)
    {
        StateID = PlayerFSMStateID.Attack;
        AIController = AICon;
    }

	//传进来的参数是没用的
    public override void Act(Transform enemy, Transform myself)
	{
		int tempPopulation = 0;
		foreach (string item in AIController.playerInfo.ArmyUnits.Keys) {
			tempPopulation += AIController.playerInfo.ArmyUnits [item].Count;
		}

		if (nearestPlayer == null) {
			//找到最近的主基地
			foreach (PlayerInfo item in PlayerInfoManager.ShareInstance.Players) {
				if (item != AIController.playerInfo) {
					if (nearestPlayer != null) {
						if (Vector3.Distance (item.location, AIController.playerInfo.location) < Vector3.Distance (nearestPlayer.location, AIController.playerInfo.location)) {
							nearestPlayer = item;
						}
					}
					else {
						nearestPlayer = item;
					}
				}
			}

			//让小兵打过去
			foreach (string item in AIController.playerInfo.ArmyUnits.Keys) {
				foreach (RTSGameUnit unit in AIController.playerInfo.ArmyUnits[item]) {
					if (unit is RTSWorker) {
						continue;
					}
					MoveUnitAIController control = unit.GetComponent<MoveUnitAIController> ();
					//control.SetTransition (MoveUnitFSMTransition.GetPatrolCommand);
					control.enemyTransform=nearestPlayer.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].transform;
					control.destPos = nearestPlayer.location;
					control.CurrentStateID = MoveUnitFSMStateID.Patrol;
					foreach (var state in control.fsmStates)
					{
						if (state.StateID == control.CurrentStateID)
						{
							control.CurrentState = state;
							break;
						}
					}
				}
			}
		}

		if (tempPopulation != population) {
			foreach (string item in AIController.playerInfo.ArmyUnits.Keys) {
				foreach (RTSGameUnit unit in AIController.playerInfo.ArmyUnits[item]) {
					MoveUnitAIController control = unit.GetComponent<MoveUnitAIController> ();
					//control.SetTransition (MoveUnitFSMTransition.GetPatrolCommand);
					control.CurrentState.destPos = nearestPlayer.location;
				}
			}
			population = tempPopulation;
		}

	}

    public override void Reason(Transform enemy, Transform myself)
    {
        base.Reason(enemy, myself);
		if ( population < 10) {
			AIController.SetTransition (PlayerFSMTransition.ArmyUseUp);
		}
		//基地没血
		//没钱
    }
}