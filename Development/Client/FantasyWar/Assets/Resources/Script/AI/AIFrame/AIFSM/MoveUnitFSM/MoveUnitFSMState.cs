using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//可能的转换
public enum MoveUnitFSMTransition
{
    SawEnemy = 0,//看到敌人
    ReachEnemy,//接近敌人，即敌人在攻击范围内
    LostEnemy,//敌人离开视线
    NoHealth,
}
//可能的状态
public enum MoveUnitFSMStateID
{
    Idle = 0,//休闲状态
    Chasing,//追逐状态
    Attacking,//攻击状态
    Collect,//挖矿
    Building,//建造
    Dead,//死亡状态
}
public abstract class MoveUnitFSMState : FSMState
{


    //目标点位置
    protected Vector3 destPos;
    //字典，用于保存“转换-状态”的信息
    protected Dictionary<MoveUnitFSMTransition, MoveUnitFSMStateID> _map;
    public Dictionary<MoveUnitFSMTransition, MoveUnitFSMStateID> Map
    {
        get
        {
            if (_map == null)
                _map = new Dictionary<MoveUnitFSMTransition, MoveUnitFSMStateID>();
            return _map;
        }
        set
        {
            _map = value;
        }
    }

    //当前状态的ID
    protected MoveUnitFSMStateID _stateID;
    public MoveUnitFSMStateID StateID { get; set; }

    //各个状态的变量

    //追逐范围
    public float chaseDistance = originDistance;
    //初始追击范围
    public static float originDistance = 40.0f;
    //攻击距离
    protected float attackDistance = 20.0f;
    //与巡逻点的距离
    protected float arriveDistance = 3.0f;

    //向字典添加转换-状态
    public void AddTransition(MoveUnitFSMTransition transition, MoveUnitFSMStateID stateID)
    {
        if (Map.ContainsKey(transition))
            return;
        Map.Add(transition, stateID);
    }

    public void DeleteTransition(MoveUnitFSMTransition transition)
    {
        if (Map.ContainsKey(transition))
            Map.Remove(transition);
    }

    public MoveUnitFSMStateID GetOutputState(MoveUnitFSMTransition transition)
    {
        return Map[transition];
    }

}
