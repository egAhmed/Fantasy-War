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

        MoveUnitCollectingState collecting = new MoveUnitCollectingState(this);
        collecting.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);
        
        AddFSMState(collecting);
        AddFSMState(dead);
        AddFSMState(build);
    }
}
