using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WorkerAnimatorStateController))]
public class RTSWorker :RTSMovableUnit, IGameUnitResourceMining
{
    //
    WorkerAnimatorStateController animatorStateController = null;
    //
    #region work type
    protected enum RTSWorkerJobType
    {
        //
        Mining = 1,
        Building = 1 << 1,
        Repairing = 1 << 2,
        OnTheWayToMine=1<<3,
        OnTheWayToBuilding=1<<4,
        Idle=1<<5,
        //
    }
    protected RTSWorkerJobType currentWorkingJobType;
    #endregion
    //
    protected bool isWorking;
    protected float workerActionAvailabeDistance = 5f;
    //
    #region mining
    public RTSBuilding homeBuilding;
    protected float miningAchievementAddingFrequency = 1f;
    protected int miningAchievementLimit = 5;
    protected int miningAchievement = 0;
    protected bool IsMiningHarvestedFull
    {
        get
        {
            return miningAchievement >= miningAchievementLimit;
        }
    }
    protected bool IsMiningHarvested{
        get
        {
            return miningAchievement > 0;
        }
    }
    #endregion 
    //
    #region building
    protected float buildingAchievementAddingFrequency = 1f;
    #endregion
    //
    #region building
    protected float repairingAchievementAddingFrequency = 1f;
    #endregion
    //
    protected virtual bool IsTargetClosingEnough
    {
        get
        {
            return TargetDistance <= workerActionAvailabeDistance && targetGameUnit != null;
        }
    }
    //
    public override void move(Vector3 pos)
    {
        //
        base.move(pos);
        //
        if (IsMiningHarvested) {
            //
            animatorStateController.WorkerAnimator_walkHarvest();
            //
        }else { 
            //
            animatorStateController.WorkerAnimator_walk();
            //            
        }
    }

    //
    protected override void OnSetTargetPosition(Vector3 pos)
    {
        base.OnSetTargetPosition(pos);
        //
        if (isWorking)
        {
            stopWork();
        }
        //
    }

    protected override void OnSetTargetUnit(RTSGameUnit unit)
    {
        base.OnSetTargetUnit(unit);
        //
        if (isWorking)
        {
            stopWork();
        }
        //
        if (unit is RTSResource)
        {
            //
            if (IsMiningHarvestedFull)
            {
                startReturning();
                //
            }
            else
            {
                goMine();
            }
            //
        }
        else if (unit is RTSBuilding)
        {
            // Debug.Log("Is RTSBuilding");
            //
            RTSBuilding building = (RTSBuilding)unit;
            //
            if (IsMiningHarvested && homeBuilding != null && building == homeBuilding)
            {
                Debug.Log("Is home");
                //
                startReturning();
                //
            }
            else
            {
                // Debug.Log("Is not home");
                //
                if (building.IsNeedRepair)
                {
                    Debug.Log("IsNeedRepair");
                    goRepair();
                }
                //
            }
        }
        //
        move(unit.transform.position);
    }

    protected virtual void goHome()
    {
        //Debug.LogError("goHome");
    }

    protected virtual void goMine()
    {
        //Debug.LogError("goMine");
        currentWorkingJobType = RTSWorkerJobType.Mining;
    }

    protected virtual void goBuild()
    {
        //Debug.LogError("goBuild");
        currentWorkingJobType = RTSWorkerJobType.Building;
    }

    protected virtual void goRepair()
    {
        //Debug.LogError("goRepair");
        currentWorkingJobType = RTSWorkerJobType.Repairing;
    }

    protected virtual void startMining()
    {
        // Debug.Log("startMining");
        isWorking = true;
        StartCoroutine(doMining());
    }

    protected virtual void stopMining()
    {
        // Debug.Log("stopMining");
        isWorking = false;
        StopCoroutine(doMining());
    }

    protected virtual IEnumerator doMining()
    {
        // Debug.Log("doMining");
        //
        while (isWorking && currentWorkingJobType == RTSWorkerJobType.Mining && !IsMiningHarvestedFull&&targetGameUnit!=null&&targetGameUnit is RTSResource)
        {
            // Debug.Log("doMining inside while");
            //
            if (IsTargetClosingEnough)
            {
                //
                move(transform.position);
                //
                transform.LookAt(targetGameUnit.transform);
                //
                //
                if (IsMiningHarvested) { 
                    animatorStateController.WorkerAnimator_digHarvest();
                }else { 
                    animatorStateController.WorkerAnimator_dig();
                }
                //
                miningAchievement++;
                //
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
            yield return new WaitForSeconds(miningAchievementAddingFrequency);
        }
        //
        if (IsMiningHarvestedFull)
        {
            startReturning();
        }else {
            stopWork();
        }
    }

    protected virtual void startBuilding()
    {
        isWorking = true;
        StartCoroutine(doBuilding());
    }

    protected virtual void stopBuilding()
    {
        isWorking = false;
        StopCoroutine(doBuilding());
    }

    protected virtual IEnumerator doBuilding()
    {
        while (true)
        {
            yield return new WaitForSeconds(buildingAchievementAddingFrequency);
        }
    }

    protected virtual void startRepairing()
    {
        StartCoroutine(doRepairing());
        isWorking = true;
    }

    protected virtual void stopRepairing()
    {
        StopCoroutine(doRepairing());
        isWorking = false;
    }

    protected virtual IEnumerator doRepairing()
    {
        // Debug.Log("doRepairing");
        //
        while (isWorking && currentWorkingJobType == RTSWorkerJobType.Repairing &&targetGameUnit!=null&&targetGameUnit is RTSBuilding)
        {
            // Debug.Log("doMining inside while");
            //
            if (IsTargetClosingEnough)
            {
                //
                move(transform.position);
                //
                RTSBuilding building = (RTSBuilding)targetGameUnit;
                //
                if (building.IsNeedRepair) {
                    //
                    Debug.LogError("IsTargetClosingEnoughToWork && IsNeedRepair");
                    //
                    transform.LookAt(targetGameUnit.transform);
                //
                if (IsMiningHarvested) { 
                    //
                    animatorStateController.WorkerAnimator_digHarvest();
                    //
                }else { 
                    //
                    animatorStateController.WorkerAnimator_dig();
                    //
                }
                //
                }
                //
            }
            else
            {
                move(targetGameUnit.transform.position);
            }
            //Debug.Log("repairingAchievementAddingFrequency = > " + repairingAchievementAddingFrequency);
            yield return new WaitForSeconds(repairingAchievementAddingFrequency);
        }
        //
        stopWork();
        //
    }

    protected virtual void startReturning()
    {
        // Debug.Log("startReturning");
        StartCoroutine(doReturning());
    }

    protected virtual void stopReturning()
    {
        // Debug.Log("stopReturning");
        StopCoroutine(doReturning());
    }

    protected virtual IEnumerator doReturning()
    {
        if (homeBuilding)
        {
            move(homeBuilding.transform.position);
            //
            bool returnSuccess = false;
            //
            while (isWorking)
            {
                float distance = Vector3.Distance(transform.position, homeBuilding.transform.position);
                if (distance <= workerActionAvailabeDistance)
                {
                    returnSuccess = true;
                    break;
                }
                yield return null;
            }
            //
            if (returnSuccess)
            {
                //
                playerResourceAdded();
                //
                miningAchievement = 0;
                //
                animatorStateController.WorkerAnimator_idle();
                //
                OnSetTargetUnit(targetGameUnit);
                //
            }
            //
        }
    }

    protected virtual void playerResourceAdded()
    {

    }

    protected virtual void startWork()
    {
        switch (currentWorkingJobType)
        {
            case RTSWorkerJobType.Mining:
                startMining();
                break;
            case RTSWorkerJobType.Building:
                startBuilding();
                break;
            case RTSWorkerJobType.Repairing:
                startRepairing();
                break;
            default:
                break;
        }
        //
    }

    protected virtual void stopWork()
    {
        switch (currentWorkingJobType)
        {
            case RTSWorkerJobType.Mining:
                stopMining();
                break;
            case RTSWorkerJobType.Building:
                stopBuilding();
                break;
            case RTSWorkerJobType.Repairing:
                stopRepairing();
                break;
            default:
                break;
        }
        //
            if (IsMiningHarvested) {
            //
            animatorStateController.WorkerAnimator_idleHarvest();
            //
            }else { 
            //
            animatorStateController.WorkerAnimator_idle();
            //            
            }
        //
    }

    protected override void Update()
    {
        base.Update();
        //
        if (isWorking)
        {
            //Debug.Log("isWorking, return update");
            return;
        }
        //
        if (IsTargetClosingEnough)
        {
            //Debug.Log("IsTargetClosingEnoughToWork");
            startWork();
        }
    }

    protected override void Start()
    {
        base.Start();
        //
        if(animatorStateController==null)
            animatorStateController = GetComponent<WorkerAnimatorStateController>();
        //
        animatorStateController.WorkerAnimator_idle();
        //
        HPMax = 100;
        HP = HPMax;
        //
    }

    //
    protected override void actionBehaviourInit() {
        base.actionBehaviourInit();
        //
        //
//		Debug.Log(playerInfo.name);
		playerInfo.ArmyUnits[Settings.ResourcesTable.Get(1009).type].Add(this);


		ActionBehaviour ac = gameObject.AddComponent<Action_Collect> ();
		ActionList.Add (ac);
		ActionBehaviour ab = gameObject.AddComponent<Action_Build> ();
		ActionList.Add (ab);
		ActionBehaviour abb = gameObject.AddComponent<Action_BuildBarrack> ();
		ActionList.Add (abb);

		Action_Collect acc=gameObject.GetComponent<Action_Collect> ();
		acc.collectDelegate += OnSetTargetUnit;
//        if(playerInfo.gameUnitBelongSide==RTSGameUnitBelongSide.Player){
//		
//            //
//            //
//        }
    }

	public ForAIBuild CreatBuilding(Vector3 pos, int ID){
		ForAIBuild foraibuild = new ForAIBuild ();
		Ray ray = new Ray (pos,Vector3.up);
		RaycastHit hitinfo;
		Physics.Raycast (ray,out hitinfo);
		Vector3 hitpos = hitinfo.point;
		foraibuild.pos = hitpos;
		switch (ID) {
		case 1101:
			Action_Build ab = gameObject.GetComponent<Action_Build> ();
			string path = ab.pathh;
			bool canBuild = RTSBuildingManager.ShareInstance.isPosValidToBuild (hitpos, path);
			foraibuild.canbuild = canBuild;
			if (canBuild) {
				ab.beginToBuildTheBuilding (pos, playerInfo);
			}
			break;
		case 1102:
			Action_BuildBarrack abb = gameObject.GetComponent<Action_BuildBarrack> ();
			string pathh = abb.pathh;
			bool canBuildd = RTSBuildingManager.ShareInstance.isPosValidToBuild (hitpos, pathh);
			if (canBuildd) {
				abb.beginToBuildTheBuilding (pos, playerInfo);
			}
			break;
		default:
			break;
		}
		return foraibuild;
	}
}
