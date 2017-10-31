using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSWild : RTSMovableUnit {
	
	RTSMeleeAnimatorStateController animatorStateController = null;
	protected bool isDoingAction;
	protected RTSMeleeActionType currentState;

	public float AttackDistance = 3;
	public int AttackForce = 10;
	public float AttackFrequency = 1;

	protected enum RTSMeleeActionType
	{
		//
		Attacking = 1,
		Idle=1<<2,
		//
	}

	protected virtual bool IsTargetClosingEnough
	{
		get
		{
			return targetGameUnit != null&&TargetDistance <= AttackDistance;
		}
	}

	public override void move(Vector3 pos)
	{
		//
		base.move(pos);
		//
		animatorStateController.doWalk();
		//
	}

	protected override void OnSetTargetPosition(Vector3 pos)
	{
		base.OnSetTargetPosition(pos);
		//
		if (isDoingAction)
		{
			stop();
		}
		//
	}

	protected override void OnSetTargetUnit(RTSGameUnit unit)
	{
		base.OnSetTargetUnit(unit);
		//
		if (isDoingAction)
		{
			stop();
		}
		//
		startAttacking();
		//
	}

	protected virtual void startAttacking()
	{
		if (!isDoingAction) { 
			currentState = RTSMeleeActionType.Attacking;
			isDoingAction = true;
			//
			Debug.Log("startAttacking");
			StartCoroutine(attacking());
			//
		}
	}

	protected virtual IEnumerator attacking()
	{
		// Debug.Log("doMining");
		//
		while (isDoingAction && currentState == RTSMeleeActionType.Attacking&&targetGameUnit!=null)
		{
			// Debug.Log("doMining inside while");
			//
			if (IsTargetClosingEnough)
			{
				move(transform.position);
				//
				transform.LookAt(targetGameUnit.transform);
				//
				animatorStateController.doAttack();
				//
				targetGameUnit.getHurt(this,AttackForce);
				//
			}
			else
			{
				if (targetGameUnit)
				{
					move(targetGameUnit.transform.position);
				}
			}
			//Debug.Log("miningAchievement = > " + miningAchievement);
			yield return new WaitForSeconds(AttackFrequency);
		}
		//
		stop();
		//
	}

	protected virtual void stop()
	{
		isDoingAction = false;
		move(transform.position);
		currentState = RTSMeleeActionType.Idle;
		animatorStateController.doIdle();
		//
		// switch (currentState)
		// {
		//     case RTSMeleeActionType.Attacking:
		//         stopAttacking();
		//         break;
		//     default:
		//         break;
		// }
		StopAllCoroutines();
		//
	}

	protected override void Awake()
	{
		base.Awake();
		//
		HPMAX = 100;
		HP = HPMAX;

		if(animatorStateController==null)
			animatorStateController = GetComponent<RTSMeleeAnimatorStateController>();
	}

	protected override void Start()
	{
		base.Start();
		//
		animatorStateController.doIdle();
		//
		HPMAX = 100;
		HP = HPMAX;
		//
		IconCameraPos = new Vector3 (1998, 5001, 1.167f);
		//
	}

	protected override void actionBehaviourInit() {
		base.actionBehaviourInit();
		//

		//playerInfo.ArmyUnits[Settings.ResourcesTable.Get(1002).type].Add(this);

		ActionBehaviour aa = gameObject.AddComponent<Action_Attack> ();
		ActionList.Add (aa);

		//		if(playerInfo.gameUnitBelongSide==RTSGameUnitBelongSide.Player){
		//			
		//			//
		//
		//            //
		//		}

		Action_Attack ab = GetComponent<Action_Attack>();
		ab.attackDelegate += OnSetTargetUnit;
		//
		// if(gameUnitBelongSide==RTSGameUnitBelongSide.Player){
		// 	ActionBehaviour att = gameObject.AddComponent<Action_Attack> ();
		// 	ActionList.Add (att);
		// }
	}

	protected override void aiBehaviourDelegateRegister() {
		//
		base.aiBehaviourDelegateRegister();
		//
		//
	}
}
