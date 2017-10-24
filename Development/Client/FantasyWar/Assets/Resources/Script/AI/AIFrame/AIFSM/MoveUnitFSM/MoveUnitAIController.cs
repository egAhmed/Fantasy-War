using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitAIController : MoveUnitAdvanceFSM
{



    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {

        //开始构造状态机
        ConstructFSM();
    }

    //在FSM基类Update中调用
    protected override void FSMUpdate()
    {
		//Debug.Log (CurrentState.GetType ());
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

    protected virtual void ConstructFSM()
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

        MoveUnitBuildState build = new MoveUnitBuildState(this);
        build.AddTransition(MoveUnitFSMTransition.GetCollectCommand, MoveUnitFSMStateID.Collect);
        build.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitCollectingState collecting = new MoveUnitCollectingState(this);
        collecting.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);


        MoveUnitPatrolState patrol = new MoveUnitPatrolState(this);
        patrol.AddTransition(MoveUnitFSMTransition.ReachEnemy, MoveUnitFSMStateID.Attacking);
        patrol.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        MoveUnitMoveState move = new MoveUnitMoveState(this);
        move.AddTransition(MoveUnitFSMTransition.LostEnemy, MoveUnitFSMStateID.Idle);
        move.AddTransition(MoveUnitFSMTransition.NoHealth, MoveUnitFSMStateID.Dead);

        AddFSMState(idle);
        AddFSMState(chase);
        AddFSMState(attack);
        AddFSMState(dead);
        AddFSMState(build);
        AddFSMState(collecting);
        AddFSMState(patrol);

		if (gameObject.GetComponent<RTSWorker> () != null) {
			CurrentStateID = MoveUnitFSMStateID.Collect;
			//Debug.Log ("改状态" + Time.time);
			foreach (var state in fsmStates)
			{
				//Debug.Log("改状态1");
				if (state.StateID == CurrentStateID)
				{
					CurrentState = state;
					//Debug.Log("改状态2");
					break;
				}
			}
		}
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

    //战略层使用，强制进入建造状态
    public void SetBuildState(Vector3 pos,string name)
    {
        CurrentStateID = MoveUnitFSMStateID.Building;
        foreach (var state in fsmStates)
        {
            if (state.StateID == CurrentStateID)
            {
                CurrentState = state;
                break;
            }
        }
        this.DesPos = pos;
        
    }
}
