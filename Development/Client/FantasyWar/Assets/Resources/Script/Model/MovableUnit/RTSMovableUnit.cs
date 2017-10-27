using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RTSGameUnitPathFindingController))]
public class RTSMovableUnit : RTSGameUnit,IGameUnitMovable {

	protected override void Awake ()
	{
		base.Awake ();
	}

    protected RTSGameUnitPathFindingController unitPathFindingController;

    public float MovingSpeed { get;set; }

    public virtual void move(Vector3 pos)
    {
		//
        unitPathFindingController.SetTarget(pos);
		//
    }

	 protected override void Start()
    {
        base.Start();
        //
        unitPathFindingController = gameObject.GetComponent<RTSGameUnitPathFindingController>();
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
