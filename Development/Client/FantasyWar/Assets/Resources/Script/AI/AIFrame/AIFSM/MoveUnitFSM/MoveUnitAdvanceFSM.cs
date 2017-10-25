using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitAdvanceFSM : FSM
{
    private Vector3 _desPos;
    public Vector3 DesPos
    {
        get { return _desPos; }
        set
        {
            if (Vector3.Distance(_desPos, value) > 1)
            {
                _desPos = value;
                if (AIMove != null)
                {
                    AIMove(_desPos);
                }
            }
        }
    }
    private PlayerInfo _playerInfo;
    public PlayerInfo playerInfo
    {
        get
        {
            if (_playerInfo == null)
                _playerInfo = transform.GetComponent<RTSGameUnit>().playerInfo;
            return _playerInfo;
        }
    }

    private Transform enemyTransform;
    public Transform EnemyTransform
    {
        get { return enemyTransform; }
        set
        {
            RTSGameUnit unit = value.GetComponent<RTSGameUnit>();

            if (unit == null)
            {
                Debug.LogError("攻击目标没有RTSGameUnit");
            }
            else
            {
                if (unit.playerInfo.groupTeam == playerInfo.groupTeam)
                    Debug.LogError("选择了自己人作为攻击目标");
            }
            enemyTransform = value;
        }
    }
    //所有状态的集合
    public List<MoveUnitFSMState> fsmStates;

    //当前状态编号
    private MoveUnitFSMStateID currentStateID;
    public MoveUnitFSMStateID CurrentStateID { get { return currentStateID; } set { currentStateID = value; } }

    private MoveUnitFSMState currentState;
    public MoveUnitFSMState CurrentState
    {
        get { return currentState; }
        //调用切出切入函数
        set
        {
            if (value == currentState)
                return;
            if (currentState != null)
                currentState.SwitchOut();
            currentState = value;
            currentState.SwitchIn();
        }
    }

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
            CurrentState = fsmState;
            CurrentStateID = fsmState.StateID;
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
        MoveUnitFSMStateID id = CurrentState.GetOutputState(trans);

        CurrentStateID = id;
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
