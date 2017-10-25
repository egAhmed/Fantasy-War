using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAIControlller : MoveUnitAIController
{

    protected override void ConstructFSM()
    {


        MoveUnitDeadState dead = new MoveUnitDeadState(this);

        BuildingIdleState idle = new BuildingIdleState(this);
        idle.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        AddFSMState(idle);
        AddFSMState(dead);
    }
}

