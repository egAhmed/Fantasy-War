using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdvancedFSM : FSM
{
    public Transform enemyTransform;
    //所有状态的集合
    public List<PlayerFSMState> fsmStates;

    //当前状态编号
    private PlayerFSMStateID currentStateID;
    public PlayerFSMStateID CurrentStateID { get { return currentStateID; } }

    private PlayerFSMState currentState;
    public PlayerFSMState CurrentState { get { return currentState; } }

    public PlayerAdvancedFSM()
    {
        fsmStates = new List<PlayerFSMState>();
    }

    /// <summary>
    /// Add New State into the list
    /// </summary>
    public void AddFSMState(PlayerFSMState fsmState)
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


    public void DeleteState(PlayerFSMStateID fsmState)
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
    public void PerformTransition(PlayerFSMTransition trans)
    {
        PlayerFSMStateID id = currentState.GetOutputState(trans);

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
