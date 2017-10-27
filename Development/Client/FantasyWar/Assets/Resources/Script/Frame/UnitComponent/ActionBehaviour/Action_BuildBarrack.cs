using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_BuildBarrack : ActionBehaviour {
		//
	PlayerInfo pi;
	RTSWorker rtsw;
	public const string PATH = @"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/barracks";

	void Awake(){
		rtsw = gameObject.GetComponent<RTSWorker> ();
		pi = rtsw.playerInfo;
		index = 7;
		shortCutKey = KeyCode.R;
		actionIcon = Resources.Load<Sprite> ("Texture/BarrIcon");
		canRepeat = false;
	}
	
	public override Action GetClickAction ()
	{
		//
		return delegate() {
			//TODO
			//建造方法
			Debug.Log("加载建筑");
			//
			//Debug.Log(pi.name);
			if(pi.Resources>150){
				//
				RTSBuildingManager.ShareInstance.startBuildingMode(PATH,rtsw);
				//
			}
		};
		//
	}
}
