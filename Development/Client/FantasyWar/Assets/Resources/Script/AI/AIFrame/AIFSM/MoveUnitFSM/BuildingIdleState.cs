using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIdleState : MoveUnitFSMState
{
    public BuildingIdleState(MoveUnitAIController AICon)
    {
        this.AIController = AICon;
        StateID = MoveUnitFSMStateID.Idle;
    }
    public override void SwitchIn()
    {

    }
    public override void Act(Transform enemy, Transform myself)
    {
        base.Act(enemy, myself);
    }
    public override void Reason(Transform enemy, Transform myself)
    {
        base.Reason(enemy, myself);
    }
    public override void SwitchOut()
    {

    }
}
