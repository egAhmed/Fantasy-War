using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitAIController : MoveUnitAdvanceFSM {
    public delegate void DelAIMove(Vector3 pos);
    public delegate void Attack(RTSGameUnit tar);
    public delegate void GetResources(RTSGameUnit tar);

    public DelAIMove AIMove;
    public Attack AIAttack;
    public GetResources AIGetResources;
    //这个方法在每个状态类的Reason中调用
    public void SetTransition(MoveUnitFSMTransition tran)
    {

    }
}
