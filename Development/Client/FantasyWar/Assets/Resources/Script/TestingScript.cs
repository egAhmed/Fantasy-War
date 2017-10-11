using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.B,buildingTesting);
        //
        RTSFogSystemManager.ShareInstance.addFogEffectToCamera();
    }

    private void buildingTesting(KeyCode key) {
        RTSGamePlayManager.ShareInstance.build();
    }

}
