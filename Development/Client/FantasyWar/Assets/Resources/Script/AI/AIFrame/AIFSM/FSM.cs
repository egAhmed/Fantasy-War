using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ForAIBuild
{
    public Vector3 pos;
    public bool canbuild;
}
public class FSM : MonoBehaviour {

    public delegate ForAIBuild delAIBuild(Vector3 pos, string path);
    public delegate void DelAIMove(Vector3 pos);
    public delegate void Attack(RTSGameUnit tar);
    public delegate void GetResources(RTSGameUnit tar);
    public delegate void CreatArmy(PlayerInfo playerinfo, int resourcesID);

    public DelAIMove AIMove;
    public Attack AIAttack;
    public GetResources AIGetResources;
    public CreatArmy creatArmy;
    //AI建造委托
    public static delAIBuild DelAIBuild;
    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    void Start()
    {
        Initialize();
    }
    
    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
