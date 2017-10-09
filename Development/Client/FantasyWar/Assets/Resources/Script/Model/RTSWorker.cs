using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(RTSGameUnitPathFindingController))]
public class RTSWorker : RTSGameUnit, IGameUnitMovable, IGameUnitResourceMining
{
    protected RTSGameUnitPathFindingController unitPathFindingController;
    //
    #region work type
    protected enum RTSWorkerJobType
    {
        Mining = 1,
        Building = 1 << 1,
        Repairing = 1 << 2,
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
    protected int miningAchievementLimit = 3;
    protected int miningAchievement = 0;
    protected bool IsMiningAchievementFull
    {
        get
        {
            return miningAchievement >= miningAchievementLimit;
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
    protected virtual bool IsTargetClosingEnoughToWork
    {
        get
        {
            return TargetDistance <= workerActionAvailabeDistance && targetGameUnit != null;
        }
    }
    //
    public float MovingSpeed
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public void move()
    {
        throw new NotImplementedException();
    }

    protected override void OnSetTargetPosition(Vector3 pos)
    {
        base.OnSetTargetPosition(pos);
        //
        if (isWorking)
        {
            stopWork();
        }
        //
        unitPathFindingController.SetTarget(pos);
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
            if (IsMiningAchievementFull)
            {
                startReturning();
            }
            else
            {
                goMine();
            }
            //
        }
        else if (unit is RTSBuilding)
        {
            RTSBuilding building = (RTSBuilding)unit;
            //
            if (miningAchievement > 0 && homeBuilding != null && building == homeBuilding)
            {
                startReturning();
            }
            else
            {
                //
                if (building.IsNeedRepair)
                {
                    goRepair();
                }
                else
                {
                    goMine();
                }
                //
            }
        }
        //
        unitPathFindingController.SetTarget(unit.transform.position);
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
        Debug.Log("startMining");
        isWorking = true;
        StartCoroutine(doMining());
    }

    protected virtual void stopMining()
    {
        Debug.Log("stopMining");
        isWorking = false;
        StopCoroutine(doMining());
    }

    protected virtual IEnumerator doMining()
    {
        Debug.Log("doMining");
        //
        while (isWorking && currentWorkingJobType == RTSWorkerJobType.Mining && !IsMiningAchievementFull)
        {
            Debug.Log("doMining inside while");
            //
            if (IsTargetClosingEnoughToWork)
            {
                miningAchievement++;
            }
            else
            {
                if (targetGameUnit)
                {
                    unitPathFindingController.SetTarget(targetGameUnit.transform.position);
                }
            }
            Debug.Log("miningAchievement = > " + miningAchievement);
            yield return new WaitForSeconds(miningAchievementAddingFrequency);
        }
        //
        if (IsMiningAchievementFull)
        {
            startReturning();
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
        while (true)
        {
            yield return new WaitForSeconds(repairingAchievementAddingFrequency);
        }
    }

    protected virtual void startReturning()
    {
        Debug.Log("startReturning");
        StartCoroutine(doReturning());
    }

    protected virtual void stopReturning()
    {
        Debug.Log("stopReturning");
        StopCoroutine(doReturning());
    }

    protected virtual IEnumerator doReturning()
    {
        if (homeBuilding)
        {
            unitPathFindingController.SetTarget(homeBuilding.transform.position);
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
    }

    protected override void Start()
    {
        base.Start();
        //
        unitPathFindingController = gameObject.GetComponent<RTSGameUnitPathFindingController>();
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
        if (IsTargetClosingEnoughToWork)
        {
            //Debug.Log("IsTargetClosingEnoughToWork");
            startWork();
        }
    }
}
