using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDevelopState : PlayerFSMState
{
    bool armyEnough = false;
    float buildRadius = 1;
    float radiusoffset = 1;
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
        //Settings.ResourcesTable.Get(1009).type更换下面第二个判断条件
        if (AIController.playerInfo.Resources == 0 && AIController.playerInfo.ArmyUnits["worker"].Count == 0)
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
        for (int i = 0; i < TargetResourcesNums.GetLength(0); i++)
        {
            int IndexOfName = TargetResourcesNums[i, 0].LastIndexOf(@"/")+1;
            string name = TargetResourcesNums[i, 0].Substring(IndexOfName);
            //先判断是否存在于建筑的字典里。
            if (AIController.playerInfo.BuildingUnits.ContainsKey(name))
            {
                //判断建筑数量是否达标
                if (AIController.playerInfo.BuildingUnits[name].Count < Convert.ToInt32(TargetResourcesNums[i, 1]))
                {
                    //没达标，建造一个
                    build(TargetResourcesNums[i, 0]);
                    return false;
                }
                continue;
            }
            //判断是否存在部队字典里面
            if (AIController.playerInfo.ArmyUnits.ContainsKey(name))
            {
                //判断部队数量是否达标
                if (AIController.playerInfo.BuildingUnits[name].Count < Convert.ToInt32(TargetResourcesNums[i, 1]))
                {
                    //没达标，造一个
                    creatArmy(TargetResourcesNums[i, 0]);
                    return false;
                }
                continue;
            }
        }
        return true;
    }

    //查找建筑点，然后建建筑的方法
    void build(string buildPath)
    {
        if (Settings.ResourcesTable.idList == null)
            Settings.TableManage.Start();
        //获取基地坐标
        //Settings.ResourcesTable.Get(1101).type更换以下“Base”
        Vector3 basePos = AIController.playerInfo.BuildingUnits["Base"][0].transform.position;
        Vector3 buildPos = basePos;
        float anchor = 0;
        if (FSM.DelAIBuild == null)
            return;
        while (!FSM.DelAIBuild(buildPos, buildPath).canbuild)
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
        //Settings.ResourcesTable.Get(1009).type更换以下worker
        MoveUnitAIController farmerAI = AIController.playerInfo.ArmyUnits["worker"][0].GetComponent<MoveUnitAIController>();
        farmerAI.SetBuildState(FSM.DelAIBuild(buildPos, buildPath).pos, buildPath);
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
    void creatArmy(string armyName)
    {
        AIController.playerInfo.BuildingUnits["人族兵营"][0].GetComponent<Action_Production>().RunAction(KeyCode.A);
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
}


