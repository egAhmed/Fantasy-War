using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RTSGameUnitPathFindingController))]
public class RTSMovableUnit : RTSGameUnit,IGameUnitMovable {

	protected override void Awake ()
	{
		base.Awake ();
	}

    protected RTSGameUnitPathFindingController UnitPathFindingController {
        get;
        set;
    }

    public float MovingSpeed { get;set; }

    public virtual void move(Vector3 pos)
    {
        //
        pathFindingEnable();
        //
        UnitPathFindingController.SetTarget(pos);
        //
    }
    //
    protected virtual void pathFindingEnable() {
        //
        UnitPathFindingController.ShouldIdle = false;
        //
    }
    //
    protected virtual void pathFindingDisable() {
        //
        UnitPathFindingController.ShouldIdle = true;
        //
    }
    //
    protected override void Start()
    {
        base.Start();
        //
        UnitPathFindingController = gameObject.GetComponent<RTSGameUnitPathFindingController>();
		//
    }
	//
	protected override void OnSetTargetPosition(Vector3 pos)
    {
        base.OnSetTargetPosition(pos);
        //
        move(pos);
        //
    }

    protected override void actionBehaviourInit() {
        base.actionBehaviourInit();
        //
        //UnitManager.ShareInstance.Armys[playerInfo].Add (gameObject);
		//
        //
		if(playerInfo.gameUnitBelongSide==RTSGameUnitBelongSide.Player){
			Interaction m = gameObject.AddComponent<Move> ();
			interactionList.Add (m);
		
		}
        //
    }

    protected override void aiBehaviourDelegateRegister() { 
        //
        base.aiBehaviourDelegateRegister();
        //
        FSM AICon = transform.GetComponent<FSM>();
        AICon.AIMove += OnSetTargetPosition;
        //
    }
}
