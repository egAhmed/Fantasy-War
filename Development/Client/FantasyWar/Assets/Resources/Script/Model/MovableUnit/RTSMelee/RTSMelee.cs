﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RTSMeleeAnimatorStateController))]
public class RTSMelee : RTSMovableUnit, IGameUnitAttackableMelee
{
    public int AttackForce{
        get;
        set;
    }
    //
    public float AttackFrequency
    {
        get;
        set;
    }
    //
    public bool AttackEnabled
    {
        get;
        set;
    }
    //
    public float AttackDistance
    {
        get;
        set;
    }

    public void attack()
    {
        
    }

    //
    RTSMeleeAnimatorStateController animatorStateController = null;
    //
    #region work type
    protected enum RTSMeleeActionType
    {
        //
        Attacking = 1,
        Idle=1<<2,
        //
    }
    protected RTSMeleeActionType currentState;
    #endregion
    //
    protected bool isDoingAction;
    protected float workerActionAvailabeDistance = 5f;
    //
    //
    protected virtual bool IsTargetClosingEnough
    {
        get
        {
            return targetGameUnit != null&&TargetDistance <= AttackDistance;
        }
    }
    //
    public override void move(Vector3 pos)
    {
        //
        base.move(pos);
        //
        animatorStateController.doWalk();
        //
    }
    //
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
        //Debug.Log("startAttacking");
        StartCoroutine(attacking());
        //
        }
    }

    protected virtual void stopAttacking()
    {
        // Debug.Log("stopMining");
        isDoingAction = false;
        StopCoroutine(attacking());
    }

    protected virtual IEnumerator attacking()
    {
        // Debug.Log("doMining");
        //
		while (targetGameUnit.HP>=0 && isDoingAction && currentState == RTSMeleeActionType.Attacking&&targetGameUnit!=null)
        {
            // Debug.Log("doMining inside while");
            //
            if (IsTargetClosingEnough)
            {
                pathFindingDisable();
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

    protected virtual void startAction()
    {
        switch (currentState)
        {
            case RTSMeleeActionType.Attacking:
                startAttacking();
                break;
            default:
                break;
        }
        //
    }

    protected virtual void stop()
    {
        isDoingAction = false;
        pathFindingDisable();
        //
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
        //
        AttackDistance = 3F;
        AttackFrequency = 1F;
        //
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
        AttackForce = 10;
        //
        IconCameraPos = new Vector3 (1998, 5001, 1.167f);
        //
    }

    protected override void Update()
    {
        base.Update();
        //
        if (UnitPathFindingController.ShouldIdle) {
            //
            animatorStateController.doIdle();
            //
        }
        //
    }

    //
    protected override void actionBehaviourInit() {
        base.actionBehaviourInit();
        //

		playerInfo.ArmyUnits[Settings.ResourcesTable.Get(1002).type].Add(this);

		ActionBehaviour aa = gameObject.AddComponent<Action_Attack> ();
		ActionList.Add (aa);

		ActionBehaviour ask = gameObject.AddComponent<Action_Skill1> ();
		ActionList.Add (ask);

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
        FSM AICon =transform.GetComponent<FSM>();
        AICon.AIAttack += OnSetTargetUnit;
        //
    }
    //
}
