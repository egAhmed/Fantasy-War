using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Build : ActionBehaviour {

	PlayerInfo pi;
	RTSWorker rtsw;
	public const string PATH1 = @"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house";
	public const string PATH2 = @"Prefab/RTSBuilding/Orc/house";

	void Awake(){
		rtsw = gameObject.GetComponent<RTSWorker> ();
		pi = rtsw.playerInfo;
		index = 6;
		shortCutKey = KeyCode.B;
		actionIcon = Resources.Load<Sprite> ("Texture/BuildIcon");
		canRepeat = false;
		info = "建造主城" +"\n"+"快捷键:B";
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//建造方法
			Debug.Log("加载建筑");
			if(pi.Resources>200){
			//Debug.Log(pi.name);
				pi.Resources -= 200;
				if(pi.racial == Racial.human){
					RTSBuildingManager.ShareInstance.startBuildingMode(PATH1,rtsw);
				}
				if(pi.racial == Racial.deemo){
					RTSBuildingManager.ShareInstance.startBuildingMode(PATH2,rtsw);
				}
			//
			}
		};
	}
}
