using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RTSSceneManager : UnitySingleton<RTSSceneManager> {
    public void loadScene(int sceneIndex) {
		//
		SceneManager.LoadScene(sceneIndex);
		//
        // switch(sceneIndex){ 
		// 	case 1:
        //         break;
        //     default:
               
        //         break;
        // }
    }
}
