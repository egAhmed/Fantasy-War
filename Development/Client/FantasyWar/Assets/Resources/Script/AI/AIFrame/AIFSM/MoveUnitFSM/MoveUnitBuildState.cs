using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitBuildState : MoveUnitFSMState {

	public MoveUnitBuildState()
	{
		StateID = MoveUnitFSMStateID.Building;
	}

	public override void Reason(Transform enemy, Transform myself)
	{
		return;
	}
		
	public override void Act (Transform player, Transform npc)
	{
		if (Vector3.Distance (npc.position, destPos) < 3) {
			RTSBuilding gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuilding> (@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house", destPos, Quaternion.identity);
			MeshCollider collider = gameUnit.gameObject.AddComponent<MeshCollider> ();
			collider.convex = true;
			//isFinish = true;
		}
		else {
			MoveUnitAIController AICon = npc.GetComponent<MoveUnitAIController>();
			if (AICon.AIMove != null) {
				AICon.AIMove (destPos);
				npc.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.GetCollectCommand);
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
