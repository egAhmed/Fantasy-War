using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//可能的转换
public enum PlayerFSMTransition
{
    ArmyEnough = 1,//军队充足
    ArmyUseUp=1<<1,//进攻时候，军队死完
    BaseNoHealth=1<<2,//基地没血
    NoMoney=1<<3,//没钱，且没农民
}
//可能的状态
public enum PlayerFSMStateID
{
    Develop = 0,//发展状态
    Attack,//攻击状态
    Dead,//死亡状态
}
public abstract class PlayerFSMState
{
    protected bool IsReasonOvrrideRun=true;
    protected RTSGameUnit mainUnit;
    protected RTSGameUnit MainUnit
    {
        get
        {
            if(mainUnit==null)
                mainUnit = AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0];
            return mainUnit;
        }
    }
    protected PlayerAIController AIController;
    //字典，用于保存“转换-状态”的信息
    protected Dictionary<PlayerFSMTransition, PlayerFSMStateID> _map;
    public Dictionary<PlayerFSMTransition, PlayerFSMStateID> Map
    {
        get
        {
            if (_map == null)
                _map = new Dictionary<PlayerFSMTransition, PlayerFSMStateID>();
            return _map;
        }
        set
        {
            _map = value;
        }
    }

    //当前状态的ID
    protected PlayerFSMStateID _stateID;
    public PlayerFSMStateID StateID
    {
        get
        {
            return _stateID;
        }
        set
        {
            _stateID = value;
        }
    }




    //向字典添加转换-状态
    public void AddTransition(PlayerFSMTransition transition, PlayerFSMStateID stateID)
    {
        if (Map.ContainsKey(transition))
            return;
        Map.Add(transition, stateID);
    }

    public void DeleteTransition(PlayerFSMTransition transition)
    {
        if (Map.ContainsKey(transition))
            Map.Remove(transition);
    }

    public PlayerFSMStateID GetOutputState(PlayerFSMTransition transition)
    {
        return Map[transition];
    }

    //用来确定是否需要转换到其他状态
    public virtual void Reason(Transform enemy, Transform myself)
    {
        //Debug.Log("HP"+mainunit.HP);
		//基地爆炸（问题不大）
        if (MainUnit == null || MainUnit.HP <= 0)
        {
            AIController.SetTransition(PlayerFSMTransition.BaseNoHealth);
            IsReasonOvrrideRun = false;
            return;
        }
        IsReasonOvrrideRun = true;

    }
    //本状态的角色行为
    public abstract void Act(Transform enemy, Transform myself);
    
}
