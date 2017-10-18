using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitAdvanceFSM : FSM
{
    //所有状态的集合
    private List<MoveUnitFSMState> fsmStates;

    //当前状态编号
    private MoveUnitFSMStateID currentStateID;
    public MoveUnitFSMStateID CurrentStateID { get { return currentStateID; } }

    private MoveUnitFSMState currentState;
    public MoveUnitFSMState CurrentState { get { return currentState; } }

    public MoveUnitAdvanceFSM()
    {
        fsmStates = new List<MoveUnitFSMState>();
    }

    /// <summary>
    /// Add New State into the list
    /// </summary>
    public void AddFSMState(MoveUnitFSMState fsmState)
    {
        if (fsmState == null)
            return;
        
        //若列表是空的，则加入列表并设置状态
        if (fsmStates.Count == 0)
        {
            fsmStates.Add(fsmState);
            currentState = fsmState;
            currentStateID = fsmState.StateID;
            return;
        }

        // 检查要加入的状态是否已经在列表中
        foreach (var state in fsmStates)
        {
            if (state.StateID == fsmState.StateID)
                return;
        }

        fsmStates.Add(fsmState);
    }

  
    public void DeleteState(MoveUnitFSMStateID fsmState)
    {
        foreach (var state in fsmStates)
        {
            if (state.StateID == fsmState)
            {
                fsmStates.Remove(state);
                return;
            }
        }
    }

    //根据当前状态，和参数中传递的转换，转移到新状态
    public void PerformTransition(MoveUnitFSMTransition trans)
    {
        MoveUnitFSMStateID id = currentState.GetOutputState(trans);
        
        currentStateID = id;
        foreach (var state in fsmStates)
        {
            if (state.StateID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }
}
