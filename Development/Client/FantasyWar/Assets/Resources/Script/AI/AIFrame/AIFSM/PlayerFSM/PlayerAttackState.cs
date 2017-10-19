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

			foreach (string item in AIController.playerInfo.ArmyUnits.Keys) {
				foreach (RTSGameUnit unit in AIController.playerInfo.ArmyUnits[item]) {
					MoveUnitAIController control = unit.GetComponent<MoveUnitAIController> ();
					control.SetTransition (MoveUnitFSMTransition.GetPatrolCommand);
					control.CurrentState.destPos = nearestPlayer.location;
				}
			}
		}

		if (tempPopulation != population) {
			foreach (string item in AIController.playerInfo.ArmyUnits.Keys) {
				foreach (RTSGameUnit unit in AIController.playerInfo.ArmyUnits[item]) {
					MoveUnitAIController control = unit.GetComponent<MoveUnitAIController> ();
					control.SetTransition (MoveUnitFSMTransition.GetPatrolCommand);
					control.CurrentState.destPos = nearestPlayer.location;
				}
			}
		}

	}

    public override void Reason(Transform enemy, Transform myself)
    {
		if (population < 10) {
			AIController.SetTransition (PlayerFSMTransition.ArmyUseUp);
		}
		//基地没血
		//没钱
    }
}