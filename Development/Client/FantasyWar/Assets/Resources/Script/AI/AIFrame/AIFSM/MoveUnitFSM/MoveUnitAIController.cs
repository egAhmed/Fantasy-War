﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitAIController : MoveUnitAdvanceFSM
{
    public delegate void DelAIMove(Vector3 pos);
    public delegate void Attack(RTSGameUnit tar);
    public delegate void GetResources(RTSGameUnit tar);

    public DelAIMove AIMove;
    public Attack AIAttack;
    public GetResources AIGetResources;


    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {

        //开始构造状态机
        ConstructFSM();
    }

    //在FSM基类Update中调用
    protected override void FSMUpdate()
    {

    }

    //在FSM基类FixedUpdate中调用
    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(enemyTransform, transform);
        CurrentState.Act(enemyTransform, transform);
    }

    //这个方法在每个状态类的Reason中调用
    public void SetTransition(MoveUnitFSMTransition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {


        MoveUnitIdleState idle = new MoveUnitIdleState();
        idle.AddTransition(MoveUnitFSMTransition.SawEnemy, MoveUnitFSMStateID.Chasing);
        idle.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitChaseState chase = new MoveUnitChaseState();
        chase.AddTransition(MoveUnitFSMTransition.LostEnemy, MoveUnitFSMStateID.Idle);
        chase.AddTransition(MoveUnitFSMTransition.ReachEnemy, MoveUnitFSMStateID.Attacking);
        chase.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitAttackingState attack = new MoveUnitAttackingState();
        attack.AddTransition(MoveUnitFSMTransition.LostEnemy, MoveUnitFSMStateID.Idle);
        attack.AddTransition(MoveUnitFSMTransition.SawEnemy, MoveUnitFSMStateID.Chasing);
        attack.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitDeadState dead = new MoveUnitDeadState();
        dead.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        AddFSMState(idle);
        AddFSMState(chase);
        AddFSMState(attack);
        AddFSMState(dead);
    }

    //战略层使用，强制进入攻击状态
    public void SetAttackState(Transform enemy)
    {
        //改变追击距离
        for (int i = 0; i < fsmStates.Count; i++)
        {
            fsmStates[i].chaseDistance = float.MaxValue;
        }

        CurrentStateID = MoveUnitFSMStateID.Attacking;
        foreach (var state in fsmStates)
        {
            if (state.StateID == CurrentStateID)
            {
                CurrentState = state;
                break;
            }
        }
    }
}
