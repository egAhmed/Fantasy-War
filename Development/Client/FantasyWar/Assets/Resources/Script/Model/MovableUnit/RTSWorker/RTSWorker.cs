using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WorkerAnimatorStateController))]
public class RTSWorker :RTSMovableUnit, IGameUnitResourceMining
{
    //
    #region building
    protected float BuildingAllowMinDistance = 1f;
    protected float BuildingAchievementWaitingFrequency = 5f;
    #endregion
    //
    List<RTSBuildingPendingToBuildTempUnit> _pendingBuildingList;
    List<RTSBuildingPendingToBuildTempUnit> PendingBuildingList
    {
        get
        {
            if (_pendingBuildingList == null)
            {
                _pendingBuildingList = new List<RTSBuildingPendingToBuildTempUnit>();
            }
            return _pendingBuildingList;
        }
    }
    public void addPendingBuildTask(RTSBuildingPendingToBuildTempUnit pendingUnit)
    {
        //
        if (PendingBuildingList == null) {
            return;
        }
        //
        lock (PendingBuildingList) { 
            PendingBuildingList.Add(pendingUnit);
        }
        //
        if (currentWorkingJobType != RTSWorkerJobType.Building && currentWorkingJobType != RTSWorkerJobType.OnTheWayToBuilding && PendingBuildingList.Count > 0)
        {
            //
            startGoingToBuilding();
            //
        }
        //
    }
    //
public virtual void startGoingToBuilding()
    {
        if (isWorking)
        {
            //
            stopWork();
            //
        }
        //
        isWorking = true;
        currentWorkingJobType = RTSWorkerJobType.OnTheWayToBuilding;
        //
        StartCoroutine(doGogingToBuildingPlace());
        //
    }

    protected virtual void stopGoingToBuilding()
    {
        //
        isWorking = false;
        currentWorkingJobType = RTSWorkerJobType.Idle;
        //
        StopCoroutine(doGogingToBuildingPlace());
        //
    }

    protected virtual IEnumerator doGogingToBuildingPlace()
    {
        //
        // Debug.LogError("Fucking here...PendingBuildingList.Count = "+PendingBuildingList.Count);
        //
        if (PendingBuildingList == null || PendingBuildingList.Count <= 0)
        {
            stopGoingToBuilding();
        }
        //
        int pendingIndex = 0;
        while (PendingBuildingList.Count>pendingIndex&&PendingBuildingList[pendingIndex] != null)
        {
            //
            RTSBuildingPendingToBuildTempUnit pendingUnit = PendingBuildingList[pendingIndex];
            //
            move(pendingUnit.transform.position);
            //
            while (isWorking && currentWorkingJobType == RTSWorkerJobType.OnTheWayToBuilding && pendingUnit != null&&pendingUnit.gameObject.activeSelf)
            {
                yield return null;
                if (Vector3.Distance(transform.position, pendingUnit.transform.position) < BuildingAllowMinDistance)
                {
                    transform.LookAt(pendingUnit.transform);
                    //
                    if (IsMiningHarvested)
                    {
                        //
                        animatorStateController.WorkerAnimator_digHarvest();
                        //
                    }
                    else
                    {
                        //
                        animatorStateController.WorkerAnimator_dig();
                        //
                    }
                    //
                    RTSWorkerJobType typeRecord = currentWorkingJobType;
                    currentWorkingJobType = RTSWorkerJobType.Building;
                    //
                    yield return new WaitForSeconds(BuildingAchievementWaitingFrequency);
                    //
                    doActuallyBuild(pendingUnit);
                    //
                    currentWorkingJobType = typeRecord;
                    //
                    break;
                    //
                }
            }
            //
            pendingIndex++;
            //
        }
        //
        pendingListRelease();
        //
        stopWork();
        //
    }

    protected virtual void pendingListRelease() {
        //
        lock (PendingBuildingList) {
            //
            for (int i = 0; i < PendingBuildingList.Count;i++)
            {
                RTSBuildingPendingToBuildTempUnit pendingUnit=PendingBuildingList[i];
                //
                if (pendingUnit == null) {
                    continue;
                }
                //
                if (pendingUnit.gameObject == null) {
                    continue;
                }
                //
                pendingUnit.gameObject.SetActive(false);
                //
                DestroyImmediate(pendingUnit.gameObject);
                //
                pendingUnit = null;
            }
            //
            PendingBuildingList.Clear();
            //
        }
        //
    }

    protected virtual void doActuallyBuild(RTSBuildingPendingToBuildTempUnit pendingUnit)
    {
        if (currentWorkingJobType == RTSWorkerJobType.Building) {
            //
            string buildingPath = pendingUnit.RealBuildingPrefabPath;
            //
            pendingUnit.gameObject.SetActive(false);
            //
            RTSBuildingManager.ShareInstance.createRTSRealBuilding(buildingPath,pendingUnit.gameObject.transform.position,Quaternion.identity,playerInfo);
            //
            alertAIControllerThatBuildingIsAlreadySuccess();
            //
        }
    }

    protected virtual void alertAIControllerThatBuildingIsAlreadySuccess() {
        if (playerInfo == null) {
            return;
        }
        //
        if (playerInfo.isAI)
                {
                    var list = transform.GetComponent<MoveUnitAIController>().fsmStates;
                    MoveUnitBuildState buildstate = null;
                    foreach (var item in list)
                    {
                        if (item.StateID == MoveUnitFSMStateID.Building)
                        {
                            buildstate = item as MoveUnitBuildState;
                            break;
                        }
                    }
                    if (buildstate != null)
                        buildstate.buildSuccess();
                }
    }
    //
    //
    GetCoinBehaviour getCoinBehaviour;
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
    #region repair
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
               //
            pendingListRelease();
            //
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
               //
            pendingListRelease();
            //
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
        // getCoinBehaviour = GetComponent<GetCoinBehaviour>();
       
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
        if (getCoinBehaviour != null) { 
        getCoinBehaviour.WorkStart(miningAchievementAddingFrequency); //采集头顶动画
        }
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
            //
            if (playerInfo.gameUnitBelongSide == RTSGameUnitBelongSide.Player)
            {
                getCoinBehaviour.WorkDone();//采集完成,取消头顶动画
            }
        }
        else {
            stopWork();
            //
            if (playerInfo.gameUnitBelongSide == RTSGameUnitBelongSide.Player)
            {
                getCoinBehaviour.WorkDone();//采集中断,取消头顶动画
            }
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
        StartCoroutine(doReturning());
    }

    protected virtual void stopReturning()
    {       
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
        if (playerInfo == null) {
            return;
        }
        //
        playerInfo.Resources = playerInfo.Resources + 60;
        //
        if (playerInfo.gameUnitBelongSide == RTSGameUnitBelongSide.Player) {
            //金币结算
            CoinCollect.Current.Money = playerInfo.Resources;
            //
            CoinCollect.Current.Done(gameObject);
            //
        }
        //
    }

    protected virtual void startWork()
    {
        switch (currentWorkingJobType)
        {
            case RTSWorkerJobType.Mining:
                startMining();
                break;
            case RTSWorkerJobType.Building:
                //
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
        //
        currentWorkingJobType = RTSWorkerJobType.Idle;
        //
        isWorking = false;
        //
        switch (currentWorkingJobType)
        {
            case RTSWorkerJobType.Mining:
                stopMining();
                break;
            case RTSWorkerJobType.OnTheWayToBuilding:
                stopGoingToBuilding();
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
		HPMAX = 100;
		HP = HPMAX;
		IconCameraPos = new Vector3 (2000, 5001, 1.167f);
        //
        if (playerInfo!=null&&playerInfo.gameUnitBelongSide==RTSGameUnitBelongSide.Player) {
            //
            getCoinBehaviour = gameObject.GetComponent<GetCoinBehaviour>();
            if (getCoinBehaviour == null) { 
            getCoinBehaviour = gameObject.AddComponent<GetCoinBehaviour>();
            }
            //
        }
        //
    }

    //
    protected override void actionBehaviourInit() {
        base.actionBehaviourInit();
        //
		playerInfo.ArmyUnits[Settings.ResourcesTable.Get(1009).type].Add(this);
        //
		ActionBehaviour ac = gameObject.AddComponent<Action_Collect> ();
		ActionList.Add (ac);
		ActionBehaviour ab = gameObject.AddComponent<Action_Build> ();
		ActionList.Add (ab);
		ActionBehaviour abb = gameObject.AddComponent<Action_BuildBarrack> ();
		ActionList.Add (abb);
		Action_Collect acc=gameObject.GetComponent<Action_Collect> ();
		acc.collectDelegate += OnSetTargetUnit;
        //
    }

	public ForAIBuild CreatBuilding(Vector3 pos, int ID){
        // Debug.LogError("fuck 1");
        ForAIBuild foraibuild = new ForAIBuild ();
		Ray ray = new Ray (pos,Vector3.up);
		RaycastHit hitinfo;
		Physics.Raycast (ray,out hitinfo);
		Vector3 hitpos = hitinfo.point;
		foraibuild.pos = hitpos;
		switch (ID) {
		case 1101:
        //  Debug.LogError("fuck 2");
			Action_Build ab = gameObject.GetComponent<Action_Build> ();
			bool canBuild = RTSBuildingManager.ShareInstance.isPosValidToBuild (hitpos, Action_Build.PATH);
			foraibuild.canbuild = canBuild;
			if (canBuild) {
                //
                // Debug.LogError("fuck 3");
                //
                 RTSBuilding build=RTSBuildingManager.ShareInstance.createRTSRealBuilding(Action_Build.PATH,hitpos,Quaternion.identity,playerInfo);
                    //
                    //础瀚加的
                    playerInfo.BuildingUnits[Settings.ResourcesTable.Get(ID).type].Add(build);
                    alertAIControllerThatBuildingIsAlreadySuccess();
                    // addPendingBuildTask(RTSBuildingManager.ShareInstance.createPendingBuildingInstance(Action_Build.PATH, pos, Quaternion.identity));
                }
			break;
		case 1102:
        //  Debug.LogError("fuck 4");
			Action_BuildBarrack abb = gameObject.GetComponent<Action_BuildBarrack> ();
			bool canBuildd = RTSBuildingManager.ShareInstance.isPosValidToBuild (hitpos, Action_BuildBarrack.PATH);
            foraibuild.canbuild = canBuildd;
			if (canBuildd) {
                //
        //  Debug.LogError("fuck 5");
                
                //   Debug.LogError("fuck 5 pos =>"+pos);
                    RTSBuilding build = RTSBuildingManager.ShareInstance.createRTSRealBuilding(Action_BuildBarrack.PATH,hitpos,Quaternion.identity,playerInfo);
                    //
                    //础瀚加的
                    playerInfo.BuildingUnits[Settings.ResourcesTable.Get(ID).type].Add(build);

                    alertAIControllerThatBuildingIsAlreadySuccess();
                // addPendingBuildTask(RTSBuildingManager.ShareInstance.createPendingBuildingInstance(Action_BuildBarrack.PATH,hitpos, Quaternion.identity));
                //
			}
			break;
		default:
			break;
		}
        //  Debug.LogError("fuck 6");
        
		return foraibuild;
	}
}
