﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RTSMeleeAnimatorStateController))]
public class RTSMelee : RTSMovableUnit, IGameUnitAttackableMelee
{
    public float AttackFrequency
    {
        get;
        set;
    }

    public bool AttackEnabled
    {
        get;
        set;
    }
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
        move(unit.transform.position);
    }

    protected virtual void startAttacking()
    {
        // Debug.Log("startMining");
        currentState = RTSMeleeActionType.Attacking;
        isDoingAction = true;
        StartCoroutine(attacking());
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
        switch (currentState)
        {
            case RTSMeleeActionType.Attacking:
                startAttacking();
                break;
            default:
                break;
        }
        //
        currentState = RTSMeleeActionType.Idle;
        animatorStateController.doIdle();
        //
    }

    protected override void Awake()
    {
        base.Awake();
        //
        if(animatorStateController==null)
            animatorStateController = GetComponent<RTSMeleeAnimatorStateController>();
        //
        AttackDistance = 3F;
        AttackFrequency = 0.5F;
        //
    }

    protected override void Start()
    {
        base.Start();
        //
        animatorStateController.doIdle();
        //
        HPMax = 100;
        HP = HPMax;
        //
    }

    //
    protected override void actionBehaviourInit() {
        base.actionBehaviourInit();
        //
        Action_Attack ab = GetComponent<Action_Attack>();
        ab.attackDelegate += OnSetTargetUnit;
        //
        // if(gameUnitBelongSide==RTSGameUnitBelongSide.Player){
        // 	ActionBehaviour att = gameObject.AddComponent<Action_Attack> ();
        // 	ActionList.Add (att);
        // }
    }
}
