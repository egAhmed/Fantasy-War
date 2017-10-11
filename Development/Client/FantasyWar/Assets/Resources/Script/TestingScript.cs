using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.B,buildingTesting);
        //
        RTSFogSystemManager.ShareInstance.addFogEffectToCamera();

        // InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.C,open);
        // InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.V,close);
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
}
