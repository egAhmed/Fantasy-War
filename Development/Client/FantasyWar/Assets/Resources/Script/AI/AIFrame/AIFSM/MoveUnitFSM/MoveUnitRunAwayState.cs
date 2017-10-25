using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitRunAwayState : MoveUnitFSMState
{
    float attackTimeCount=0;
    float returnToWorkTime = 5;
    public MoveUnitRunAwayState(MoveUnitAIController AICon)
    {
        this.AICon = AICon;
        StateID = MoveUnitFSMStateID.RunAway;
    }

    public override void Reason(Transform enemy, Transform myself)
    {
        base.Reason(enemy, myself);
        //多久没受到攻击，就回去继续该干嘛干嘛
        if(attackTimeCount>returnToWorkTime)
        {
            if(AICon.LastState is MoveUnitCollectingState)
                AICon.SetTransition(MoveUnitFSMTransition.GetCollectCommand);
            if (AICon.LastState is MoveUnitBuildState)
                AICon.SetTransition(MoveUnitFSMTransition.SetBuild);

        }

    }

    public override void Act(Transform enemy, Transform myself)
    {
        base.Act(enemy, myself);
        //回到基地附近
        AICon.DesPos=AICon.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].transform.position;
        return;
    }


    public override void SwitchIn()
    {
        attackTimeCount = 0;
    }

    public override void SwitchOut()
    {
        base.SwitchOut();
        attackTimeCount = 0;
    }
}
