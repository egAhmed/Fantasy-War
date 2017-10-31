using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_BuildBarrack : ActionBehaviour {
		//
	PlayerInfo pi;
	RTSWorker rtsw;
	public const string PATH1 = @"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/barracks";
	public const string PATH2 = @"Prefab/RTSBuilding/Orc/factory";

	void Awake(){
		rtsw = gameObject.GetComponent<RTSWorker> ();
		pi = rtsw.playerInfo;
		index = 7;
		shortCutKey = KeyCode.R;
		actionIcon = Resources.Load<Sprite> ("Texture/BarrIcon");
		canRepeat = false;
		info = "建造兵营" +"\n"+"快捷键:R";
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
				if(pi.racial == Racial.human){
					RTSBuildingManager.ShareInstance.startBuildingMode(PATH1,rtsw);
				}
				if(pi.racial == Racial.deemo){
					RTSBuildingManager.ShareInstance.startBuildingMode(PATH2,rtsw);
				}
				//
			}
		};
		//
	}
}
