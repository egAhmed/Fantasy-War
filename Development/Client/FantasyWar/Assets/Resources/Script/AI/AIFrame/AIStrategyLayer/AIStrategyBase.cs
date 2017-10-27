using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AIStrategyState
{
    Development=0,
    Attack,
    Defences,
}
enum AIStrategyTransition
{
    SawEnemy=0,
    ReachEnemy,//有部队接触玩家
    LostEnemy,//丢失玩家视野
    ArmyBeAttack,//部队受到攻击

}
public class AIStrategyBase : AIBehaviorTreeBase {
    private string _playerName;
    public string PlayerName
    {
        get
        {
            if (string.IsNullOrEmpty(_playerName))
                _playerName = transform.name;
            return _playerName;
        }
    }



    //所有种类建筑单位集合
    private List<List<RTSBuilding>> _listBuildings;
    public List<List<RTSBuilding>> ListBuildings
    {
        get
        {
            if (_listBuildings == null)
                _listBuildings = new List<List<RTSBuilding>>();
            return _listBuildings;
        }
        set
        {
            _listBuildings = value;
        }
    }


    //可移动单位的集合
    private List<List<RTSMovableUnit>> _listMovableUnits;

    public List<List<RTSMovableUnit>> ListMovableUnits
    {
        get
        {
            if (_listMovableUnits == null)
                _listMovableUnits = new List<List<RTSMovableUnit>>();
            return _listMovableUnits;
        }
        set
        {
            _listMovableUnits = value;
        }
    }

    //可选行为层集合
    private List<List<AIMotionBase>> _listAIMotion;

    public List<List<AIMotionBase>> ListAIMotion
    {
        get
        {
            if (_listAIMotion == null)
                _listAIMotion = new List<List<AIMotionBase>>();
            return _listAIMotion;
        }
        set
        {
            _listAIMotion = value;
        }
    }

    //刷新所拥有的单位集合
    void RefreshDics()
    {
        
    }

    //刷新单个单位的集合
    void RefreshDic(Dictionary<string,List<RTSGameUnit>> dic)
    {

    }
    
}
