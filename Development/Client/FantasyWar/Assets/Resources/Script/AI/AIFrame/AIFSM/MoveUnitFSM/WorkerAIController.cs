using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAIController : MoveUnitAIController {
 protected override void ConstructFSM()
    {

       
        MoveUnitDeadState dead = new MoveUnitDeadState(this);

        MoveUnitBuildState build = new MoveUnitBuildState(this);
        build.AddTransition(MoveUnitFSMTransition.GetCollectCommand, MoveUnitFSMStateID.Collect);
        build.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);
        build.AddTransition(MoveUnitFSMTransition.BeAttack, MoveUnitFSMStateID.RunAway);

        MoveUnitCollectingState collecting = new MoveUnitCollectingState(this);
        collecting.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);
        collecting.AddTransition(MoveUnitFSMTransition.BeAttack, MoveUnitFSMStateID.RunAway);

        MoveUnitRunAwayState runaway = new MoveUnitRunAwayState(this);
        runaway.AddTransition(MoveUnitFSMTransition.GetCollectCommand, MoveUnitFSMStateID.Collect);
        runaway.AddTransition(MoveUnitFSMTransition.SetBuild, MoveUnitFSMStateID.Building);

        AddFSMState(collecting);
        AddFSMState(dead);
        AddFSMState(build);
        AddFSMState(runaway);
    }
}
