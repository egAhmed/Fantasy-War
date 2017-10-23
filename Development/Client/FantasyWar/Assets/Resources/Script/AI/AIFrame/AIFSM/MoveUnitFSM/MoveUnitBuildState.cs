using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitBuildState : MoveUnitFSMState {

	public MoveUnitBuildState(MoveUnitAIController AICon)
	{
		StateID = MoveUnitFSMStateID.Building;
        this.AICon = AICon;
    }

	public override void Reason(Transform enemy, Transform myself)
	{
        base.Reason(enemy, myself);
        return;
	}
		
	public override void Act (Transform player, Transform npc)
	{
		if (Vector3.Distance (npc.position, destPos) < 3) {
			RTSBuilding gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuilding> (@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house", destPos, Quaternion.identity);
			MeshCollider collider = gameUnit.gameObject.AddComponent<MeshCollider> ();
			collider.convex = true;
			npc.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.GetCollectCommand);
			//isFinish = true;
		}
		else {
			//MoveUnitAIController AICon = npc.GetComponent<MoveUnitAIController>();
			if (MoveUnitAIController.AIMove != null) {
                MoveUnitAIController.AIMove (destPos);
			}
		}
	}

	public override void SwitchIn ()
	{
		
	}

	public override void SwitchOut ()
	{
		
	}
}
