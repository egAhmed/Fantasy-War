using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Build : ActionBehaviour {

	PlayerInfo pi;
	RTSWorker rtsw;
	public const string PATH = @"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house";

	void Awake(){
		rtsw = gameObject.GetComponent<RTSWorker> ();
		pi = rtsw.playerInfo;
		index = 6;
		shortCutKey = KeyCode.B;
		actionIcon = Resources.Load<Sprite> ("Texture/BuildIcon");
		canRepeat = false;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//建造方法
			Debug.Log("加载建筑");
			if(pi.Resources>200){
			//Debug.Log(pi.name);
			RTSBuildingManager.ShareInstance.startBuildingMode(PATH,rtsw);
			//
			}
		};
	}
}
