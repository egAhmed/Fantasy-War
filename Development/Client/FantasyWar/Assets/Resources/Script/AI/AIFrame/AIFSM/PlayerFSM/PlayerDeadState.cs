using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerFSMState
{
    public PlayerDeadState(PlayerAIController AICon)
    {
        StateID = PlayerFSMStateID.Dead;
        AIController = AICon;
    }
    public override void Act(Transform enemy, Transform myself)
    {
		Debug.Log ("deaddead");
        PlayerInfoManager.ShareInstance.Players.Remove(AIController.playerInfo);
        foreach (var item in AIController.playerInfo.BuildingUnits.Keys)
        {
            List<RTSGameUnit> unitList = AIController.playerInfo.BuildingUnits[item];
            for (int i=0;i<unitList.Count;i++)
            {
                unitList[i].HP = 0;
            }
        }
        foreach (var item in AIController.playerInfo.ArmyUnits.Keys)
        {
            List<RTSGameUnit> unitList = AIController.playerInfo.ArmyUnits[item];
            for (int i = 0; i < unitList.Count; i++)
            {
                unitList[i].HP = 0;
            }
        }
        AIController.enabled = false;
    }

    public override void Reason(Transform enemy, Transform myself)
    {
       
    }

   
}
