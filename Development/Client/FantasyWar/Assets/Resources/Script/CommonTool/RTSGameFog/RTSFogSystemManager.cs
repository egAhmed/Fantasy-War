using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSFogSystemManager : UnitySingleton<RTSFogSystemManager> {
    string fogEffectPrefabObjPath = @"Prefab/Fog/Fog of War";
    FOWEffect fowEffect;
    GameObject fogEffectPrefabObj;

   /// <summary>
   /// Start is called on the frame when a script is enabled just before
   /// any of the Update methods is called the first time.
   /// </summary>
   void Start()
   {
       if (fogEffectPrefabObj == null) { 
			fogEffectPrefabObj=PrefabFactory.ShareInstance.createClone(fogEffectPrefabObjPath,Vector3.zero,Quaternion.identity);
		}
   }

    public void addFogEffectToCamera() {
        if (fowEffect == null) { 
			fowEffect=RTSCameraController.RTSCamera.gameObject.AddComponent<FOWEffect>();
		}
    }
    //

}
