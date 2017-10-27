using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAIController : MoveUnitAIController {

    protected override void ConstructFSM()
    {
        
        //Debug.Log ("start" + Time.time);
        MoveUnitIdleState idle = new MoveUnitIdleState(this);
        idle.AddTransition(MoveUnitFSMTransition.SawEnemy, MoveUnitFSMStateID.Chasing);
        idle.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitChaseState chase = new MoveUnitChaseState(this);
        chase.AddTransition(MoveUnitFSMTransition.LostEnemy, MoveUnitFSMStateID.Idle);
        chase.AddTransition(MoveUnitFSMTransition.ReachEnemy, MoveUnitFSMStateID.Attacking);
        chase.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitAttackingState attack = new MoveUnitAttackingState(this);
        attack.AddTransition(MoveUnitFSMTransition.LostEnemy, MoveUnitFSMStateID.Idle);
        attack.AddTransition(MoveUnitFSMTransition.SawEnemy, MoveUnitFSMStateID.Chasing);
        attack.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitDeadState dead = new MoveUnitDeadState(this);


        MoveUnitPatrolState patrol = new MoveUnitPatrolState(this);
        patrol.AddTransition(MoveUnitFSMTransition.ReachEnemy, MoveUnitFSMStateID.Attacking);
        patrol.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);
        

        AddFSMState(idle);
        AddFSMState(chase);
        AddFSMState(attack);
        AddFSMState(dead);
        AddFSMState(patrol);
    }
}
