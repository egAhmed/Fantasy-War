using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDevelopState : PlayerFSMState
{
    private Vector3 requireSupportPos;
    public Vector3 RequireSupportPos
    {
        set
        {
            if(requireSupportPos!=null)
                if(Vector3.Distance(requireSupportPos,value)>1)
                {
                    requireSupportPos = value;
                    SendSupport(requireSupportPos);
                }
            else
                {
                    requireSupportPos = value;
                    SendSupport(requireSupportPos);
                }
        }
    }
    bool armyEnough = false;
    float buildRadius = 1;
    float radiusoffset = 1;
    float checkTime = 0.5f;
    float currentTime = 0;
    public PlayerDevelopState(PlayerAIController AICon)
    {
        StateID = PlayerFSMStateID.Develop;
        AIController = AICon;
    }
    public override void Act(Transform enemy, Transform myself)
    {
        armyEnough = develop(AIController.TargetResourcesNums);
    }

    public override void Reason(Transform enemy, Transform myself)
    {
        base.Reason(enemy, myself);
        if (Settings.ResourcesTable.idList == null)
            Settings.TableManage.Start();
        //士兵已满
        if (armyEnough)
        {
            AIController.SetTransition(PlayerFSMTransition.ArmyEnough);
            return;
        }

        //没钱没农民
        //判断是否还有农民
        if (AIController.playerInfo.Resources == 0 && AIController.playerInfo.ArmyUnits[Settings.ResourcesTable.Get(1009).type].Count == 0)
        {
            //还要没兵
            if (isNoArmy())
            {
                AIController.SetTransition(PlayerFSMTransition.NoMoney & PlayerFSMTransition.ArmyUseUp);
                return;
            }
            AIController.SetTransition(PlayerFSMTransition.NoMoney);
            return;
        }

    }

    //依次建造建筑或建造兵种
    bool develop(string[,] TargetResourcesNums)
    {
        //if (currentTime < checkTime)
        //{
        //    currentTime += Time.deltaTime;
        //    return false;
        //}
        //currentTime = 0;
        for (int i = 0; i < TargetResourcesNums.GetLength(0); i++)
        {
            //int IndexOfName = TargetResourcesNums[i, 0].LastIndexOf(@"/")+1;
			string name = Settings.ResourcesTable.Get(Convert.ToInt32(TargetResourcesNums[i, 0])).type; //TargetResourcesNums[i, 0].Substring(IndexOfName);
            //先判断是否存在于建筑的字典里。
            if (AIController.playerInfo.BuildingUnits.ContainsKey(name))
            {
                //判断建筑数量是否达标
                if (AIController.playerInfo.BuildingUnits[name].Count < Convert.ToInt32(TargetResourcesNums[i, 1]))
                {
                    //没达标，建造一个
                    build(Convert.ToInt32(TargetResourcesNums[i, 0]));
                    return false;
                }
                continue;
            }
            //判断是否存在部队字典里面
            if (AIController.playerInfo.ArmyUnits.ContainsKey(name))
            {
                //判断部队数量是否达标
               // Debug.LogError("target"+Convert.ToInt32(TargetResourcesNums[i, 1]));
               // Debug.Log("now"+AIController.playerInfo.ArmyUnits[name].Count);
                if (AIController.playerInfo.ArmyUnits[name].Count < Convert.ToInt32(TargetResourcesNums[i, 1]))
                {
                    //Debug.Log("进来了");
                    creatArmy(Convert.ToInt32(TargetResourcesNums[i, 0]));
                    //没达标，造一个
                    return false;
                }
                continue;
            }
        }
        return true;
    }

    //查找建筑点，然后建建筑的方法
    void build(int id)
    {
        if (Settings.ResourcesTable.idList == null)
            Settings.TableManage.Start();
        //获取基地坐标
        Vector3 basePos = AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].transform.position;
        Vector3 buildPos = basePos;
        float anchor = 0;
        if (AIController.DelAIBuild == null)
            return;
        while (!AIController.DelAIBuild(buildPos, id).canbuild)
        {
            buildPos = basePos + new Vector3(Mathf.Sin(anchor), -100, Mathf.Cos(anchor));
            if (anchor == 2 * Mathf.PI)
                anchor = 0;
            else
                anchor += Mathf.PI / 4;
        }
        //以基地为圆心，在一个圆上寻找建筑点
        //以基地坐标为初始值，计算水平增量后的坐标，
        //在此坐标上，发射一条垂直的射线，射线与地面相交的一点，即使建造点
        //把建造点传给建造函数，根据返回值，判断是否建造成功。不成功就继续查找建造点。
        //若可以建造，则将建造点传给农民;


        //找一个农民去建房子
        MoveUnitAIController farmerAI = AIController.playerInfo.ArmyUnits[Settings.ResourcesTable.Get(1009).type][0].GetComponent<MoveUnitAIController>();
        farmerAI.SetBuildState(AIController.DelAIBuild(buildPos, id).pos, Settings.ResourcesTable.Get(id).type);
    }

    ////，用于寻找合适的建造地点
    //public Vector3 FindBuildPos(Vector3 pos)
    //{
    //    pos += new Vector3(0, -5 00, 0);
    //    Ray ray = new Ray(pos, Vector3.up);
    //    RaycastHit rayinfo = new RaycastHit();
    //    Physics.Raycast(ray, out rayinfo,1000.0f, RTSLayerManager.ShareInstance.LayerMaskRayCastMouse1, QueryTriggerInteraction.Ignore);
    //    return rayinfo.point;
    //}
    void creatArmy(int resourcesID)
	{
            AIController.creatArmy(resourcesID);

        if (AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type].Count > 0 && Time.time > Time.deltaTime && resourcesID == 1009)
        {
            //Debug.Log("tmpwww");
            //			Debug.Log (AIController.playerInfo.BuildingUnits [Settings.ResourcesTable.Get (1101).type] [0].GetComponent<Action_Production> ()==null);
            AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].GetComponent<Action_Production>().RunAction(KeyCode.A);
        }
        if (AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type].Count > 0 && Time.time > Time.deltaTime && resourcesID == 1002)
        {
            //			Debug.Log (AIController.playerInfo.BuildingUnits [Settings.ResourcesTable.Get (1101).type] [0].GetComponent<Action_Production> ()==null);
            //AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].GetComponent<Action_ProductionRider>().RunAction(KeyCode.A);
            //AIController.StartCoroutine(tmpknight());
        }
    }
    bool isNoArmy()
    {
        foreach (var item in AIController.playerInfo.ArmyUnits.Values)
        {
            if (item.Count != 0)
                return false;
        }
        return true;
    }

    IEnumerator tmpworker()
    {
        yield return null;
        AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].GetComponent<Action_Production>().RunAction(KeyCode.A);
    }

    IEnumerator tmpknight()
    {
        yield return null;
        AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].GetComponent<Action_ProductionRider>().RunAction(KeyCode.A);
    }

    /// <summary>
    /// 派出支援
    /// </summary>
    /// <param name="pos"></param>
    void SendSupport(Vector3 pos)
    {
        foreach (var armylist in AIController.playerInfo.ArmyUnits.Values)
        {
            if (armylist[0] == null || armylist[0] is RTSWorker)
                continue;
            foreach (var unit in armylist)
            {
                //让这些单位去支援
                                    MoveUnitAIController control = unit.GetComponent<MoveUnitAIController>();
                //control.SetTransition (MoveUnitFSMTransition.GetPatrolCommand);
                
                    control.DesPos = pos;
            }
        }
    }
}


