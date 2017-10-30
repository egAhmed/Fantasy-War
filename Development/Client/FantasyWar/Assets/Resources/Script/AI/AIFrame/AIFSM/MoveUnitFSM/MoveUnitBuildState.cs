using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitBuildState : MoveUnitFSMState
{
    Vector3 basePos;
    //建造成功
    bool buildsucess = false;
    //建造中
    bool building = false;
    Vector3 BasePos
    {
        get
        {
            if(basePos==default(Vector3))
                basePos= AIController.playerInfo.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0].transform.position;
            return basePos;
        }
    }

    public int buildid;


    public MoveUnitBuildState(MoveUnitAIController AICon)
    {
        StateID = MoveUnitFSMStateID.Building;
        this.AIController = AICon;
    }

    public override void Reason(Transform enemy, Transform myself)
    {
        base.Reason(enemy, myself);
        if (buildsucess)
        {
            AIController.SetTransition(MoveUnitFSMTransition.GetCollectCommand);
        }
        return;
    }

    public override void Act(Transform enemy, Transform myself)
    {
        base.Act(enemy, myself);
        if (building)
            return;
        //获取基地坐标
        Vector3 buildPos = BasePos;
        float anchor = 0;
        float radius = 5;
        int count = 0;
        if (AIController.DelAIBuild == null)
            return;
        do
        {
            buildPos = BasePos + new Vector3(radius * Mathf.Sin(anchor), -100, radius * Mathf.Cos(anchor));
            if (anchor == 2 * Mathf.PI)
            {
                anchor = 0;
                radius += 5;
            }
            else
                anchor += Mathf.PI / 4;
            //Debug.Log(count++);
        }
        while (!AIController.DelAIBuild(buildPos, buildid).canbuild);
        //Debug.Log(count+=10);
        //buildPos += new Vector3(30, -100, 0);
        //Debug.Log("开始建造") ;
        //bool test = AIController.DelAIBuild(buildPos, buildid).canbuild;
        //Debug.Log("能否建造" + test);

        building = true;
        //以基地为圆心，在一个圆上寻找建筑点
        //以基地坐标为初始值，计算水平增量后的坐标，
        //在此坐标上，发射一条垂直的射线，射线与地面相交的一点，即使建造点
        //把建造点传给建造函数，根据返回值，判断是否建造成功。不成功就继续查找建造点。
        //若可以建造，则将建造点传给农民;
        //AICon.DelAIBuild()
        //if (Vector3.Distance(myself.position, AICon.DesPos) < 3)
        //{
        //    RTSBuilding gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuilding>(@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house", AICon.DesPos, Quaternion.identity);
        //    MeshCollider collider = gameUnit.gameObject.AddComponent<MeshCollider>();
        //    collider.convex = true;
        //    AICon.SetTransition(MoveUnitFSMTransition.GetCollectCommand);
        //    //isFinish = true;
        //}
        //else {
        //          //MoveUnitAIController AICon = npc.GetComponent<MoveUnitAIController>();
        //          AICon.DesPos = npc.position;
        //}
    }

    public override void SwitchIn()
    {
        building = false;
        buildsucess = false;
    }

    public override void SwitchOut()
    {
        base.SwitchOut();
        buildid = 0;
        //Debug.Log("出去建造状态");
    }

    public void buildSuccess()
    {
        buildsucess = true;
    }
}
