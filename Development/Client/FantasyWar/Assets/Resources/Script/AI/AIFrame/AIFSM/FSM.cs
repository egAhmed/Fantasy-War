using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CommonDelegaet();
public struct ForAIBuild
{
    public Vector3 pos;
    public bool canbuild;
}
public class FSM : MonoBehaviour {

	public delegate ForAIBuild delAIBuild(Vector3 pos, int ID);
    public delegate void DelAIMove(Vector3 pos);
    public delegate void Attack(RTSGameUnit tar);
    public delegate void GetResources(RTSGameUnit tar);

    public DelAIMove AIMove;
    public Attack AIAttack;
    public GetResources AIGetResources;
    //AI建造委托
	public delAIBuild DelAIBuild;
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
