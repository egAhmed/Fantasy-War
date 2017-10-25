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

        CurrentState.Reason(EnemyTransform, transform);
        CurrentState.Act(EnemyTransform, transform);
    }

    //这个方法在每个状态类的Reason中调用
    public void SetTransition(MoveUnitFSMTransition t)
    {
        PerformTransition(t);
    }

    protected virtual void ConstructFSM()
    {


    }

    //战略层使用，强制进入攻击状态
    public void SetAttackState(Transform enemy)
    {
        //改变追击距离
        for (int i = 0; i < fsmStates.Count; i++)
        {
            fsmStates[i].chaseDistance = float.MaxValue;
        }

        foreach (var state in fsmStates)
        {
            if (state.StateID == MoveUnitFSMStateID.Attacking)
            {
                CurrentStateID = MoveUnitFSMStateID.Attacking;
                CurrentState = state;
                break;
            }
        }
    }

    //战略层使用，强制进入建造状态
    public void SetBuildState(Vector3 pos, string name)
    {
        foreach (var state in fsmStates)
        {
            if (state.StateID == MoveUnitFSMStateID.Building)
            {
                CurrentStateID = MoveUnitFSMStateID.Building;
                CurrentState = state;
                break;
            }
        }
        this.DesPos = pos;

    }

    /// <summary>
    /// 强制状态转换，返回是否转换成功
    /// </summary>
    public bool StateSwitchCompel(MoveUnitFSMStateID targetStateID)
    {
        foreach (var item in fsmStates)
        {
            if (item.StateID == targetStateID)
            {
                CurrentState = item;
                CurrentStateID = targetStateID;
                return true;
            }
        }
        return false;
    }
}
