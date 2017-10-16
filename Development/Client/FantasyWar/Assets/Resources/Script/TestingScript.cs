using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.B,buildingTesting);
        //

		// RTSGamePlayManager.ShareInstance.build();
        //RTSFogSystemManager.ShareInstance.addFogEffectToCamera();
        //
        RTSManager.ShareInstance.testingCreatePlayInfo();
        //
        // InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.C,open);
        // InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.V,close);
        //InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.C, poolManagerTesting);
    }

    private void poolManagerTesting(KeyCode key) {
        //
        GameObject obj = new GameObject();
        // PoolManager.ShareInstanceance.PushObject("testing",1,obj);
        //
    }

    private void buildingTesting(KeyCode key) {
        RTSGamePlayManager.ShareInstance.build();
    }

    // private void open(KeyCode key) {
    //     InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.P,p);
    //      Debug.LogError("注册了P");
    // }
    // private void close(KeyCode key) {
    //     InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.P,p);
    //     Debug.LogError("注销了P");
    // }

    // private void p(KeyCode key) {
    //     Debug.LogError("按下了P");
    // }

    // void Update()
    // {
    //     Dictionary<PlayerInfo,List<GameObject>> dic=UnitManager.ShareInstance.Armys;
    //     if (dic == null) {
    //         Debug.LogError("dic null");
    //         return;
    //     }

    //     foreach (var key in dic.Keys) {
    //         List<GameObject> list=dic[key];
    //         if (list == null) {
    //         Debug.LogError("list null");
    //         return;
    //         }else { 
    //              Debug.LogError("list count :"+list.Count);
    //         }
    //     }

    // }
}
