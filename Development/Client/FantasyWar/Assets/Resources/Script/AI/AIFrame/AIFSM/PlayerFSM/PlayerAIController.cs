using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIController : PlayerAdvancedFSM {
    //public delegate void DelAIMove(Vector3 pos);
    //public delegate void Attack(RTSGameUnit tar);
    //public delegate void GetResources(RTSGameUnit tar);

    //public DelAIMove AIMove;
    //public Attack AIAttack;
    //public GetResources AIGetResources;
	public PlayerInfo playerInfo;
    
    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        //rtswork = transform.GetComponent<RTSWorker>();
        //开始构造状态机
        ConstructFSM();

		DelAIBuild = AIBuild;
    }

	private bool AIBuild(Vector3 pos,string prefabPath){
		foreach (RTSGameUnit item in playerInfo.ArmyUnits["worker"]) {
			//选一个农民执行建造动作，建造建筑到pos
			if (item is RTSWorker) {
				MoveUnitAIController muac = item.GetComponent<MoveUnitAIController> ();
				muac.SetTransition (MoveUnitFSMTransition.SetBuild);
				muac.CurrentState.destPos = pos;
				return true;
			}
		}
		return false;
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
    public void SetTransition(PlayerFSMTransition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {


        PlayerAttackState attack = new PlayerAttackState(this);
        attack.AddTransition(PlayerFSMTransition.BaseNoHealth, PlayerFSMStateID.Dead);
        attack.AddTransition(PlayerFSMTransition.ArmyUseUp, PlayerFSMStateID.Develop);

        PlayerDevelopState develop = new PlayerDevelopState(this);
        develop.AddTransition(PlayerFSMTransition.ArmyEnough, PlayerFSMStateID.Attack);
        develop.AddTransition(PlayerFSMTransition.BaseNoHealth, PlayerFSMStateID.Dead);
        develop.AddTransition(PlayerFSMTransition.NoMoney, PlayerFSMStateID.Attack);

        PlayerDeadState dead = new PlayerDeadState(this);
        attack.AddTransition(PlayerFSMTransition.BaseNoHealth, PlayerFSMStateID.Dead);
        

        AddFSMState(attack);
        AddFSMState(develop);
        AddFSMState(dead);
    }

    //战略层使用，强制进入攻击状态
//    public void SetAttackState(Transform enemy)
//    {
//        //改变追击距离
//        for (int i = 0; i < fsmStates.Count; i++)
//        {
//            fsmStates[i].chaseDistance = float.MaxValue;
//        }
//
//        CurrentStateID = MoveUnitFSMStateID.Attacking;
//        foreach (var state in fsmStates)
//        {
//            if (state.StateID == CurrentStateID)
//            {
//                CurrentState = state;
//                break;
//            }
//        }
//    }
}
