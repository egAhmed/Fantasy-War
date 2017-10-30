using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerFSMState
{
    int population;

    PlayerInfo nearestPlayer;
    PlayerInfo NearestPlayer
    {
        get { return nearestPlayer; }
        set
        {
            if (value!=null&&value.groupTeam == AIController.playerInfo.groupTeam)
                Debug.LogError("选择了同队队友作为目标");
            nearestPlayer = value;
        }
    }
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
        foreach (string item in AIController.playerInfo.ArmyUnits.Keys)
        {
            tempPopulation += AIController.playerInfo.ArmyUnits[item].Count;
        }

        if (NearestPlayer != null)
            if (!PlayerInfoManager.ShareInstance.Players.Contains(NearestPlayer))
            {
                Debug.Log("目标主基地消失");
            }
        if (!PlayerInfoManager.ShareInstance.Players.Contains(NearestPlayer))
            NearestPlayer = null;
        if (NearestPlayer == null )
        {

            //找到最近的主基地
            foreach (PlayerInfo item in PlayerInfoManager.ShareInstance.Players)
            {
                if (item.groupTeam == AIController.playerInfo.groupTeam)
                    continue;
                if (item != AIController.playerInfo)
                {
                    if (NearestPlayer != null)
                    {
                        if (Vector3.Distance(item.location, AIController.playerInfo.location) < Vector3.Distance(NearestPlayer.location, AIController.playerInfo.location))
                        {
                            NearestPlayer = item;
                        }
                    }
                    else
                    {
                        NearestPlayer = item;
                    }
                }
            }
            if (NearestPlayer == null)
                return;
            //让小兵打过去
            foreach (string item in AIController.playerInfo.ArmyUnits.Keys)
            {
                foreach (RTSGameUnit unit in AIController.playerInfo.ArmyUnits[item])
                {
                    if (unit is RTSWorker)
                    {
                        continue;
                    }
                    MoveUnitAIController control = unit.GetComponent<MoveUnitAIController>();
                    //control.SetTransition (MoveUnitFSMTransition.GetPatrolCommand);
                    control.EnemyTransform = NearestPlayer.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].transform;
                    control.DesPos = NearestPlayer.location;
                    foreach (var state in control.fsmStates)
                    {
                        if (state.StateID == MoveUnitFSMStateID.Patrol)
                        {
                            control.CurrentStateID = MoveUnitFSMStateID.Patrol;
                            control.CurrentState = state;
                            break;
                        }
                    }
                }
            }
        }

        //if (tempPopulation != population) {
        //	foreach (string item in AIController.playerInfo.ArmyUnits.Keys) {
        //		foreach (RTSGameUnit unit in AIController.playerInfo.ArmyUnits[item]) {
        //			MoveUnitAIController control = unit.GetComponent<MoveUnitAIController> ();
        //			//control.SetTransition (MoveUnitFSMTransition.GetPatrolCommand);
        //			control.DesPos = nearestPlayer.location;
        //		}
        //	}
        //	population = tempPopulation;
        //}

    }

    public override void Reason(Transform enemy, Transform myself)
    {
        base.Reason(enemy, myself);
        if (!IsReasonOvrrideRun)
            return;
        if (population < 10)
        {
            AIController.SetTransition(PlayerFSMTransition.ArmyUseUp);
        }
        //基地没血
        //没钱
    }
}