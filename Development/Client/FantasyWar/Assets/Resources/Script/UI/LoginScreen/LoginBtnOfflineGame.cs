using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBtnOfflineGame : MonoBehaviour {

	// public GameObject online;

    Button loginBtn;

    private void Awake()
    {
        loginBtn = GetComponent<Button>();
        loginBtn.onClick.AddListener(()=> {
            //
            RTSSceneManager.ShareInstance.loadScene(1);
            //
        });
    }
    //
	// void Update()
	// {
	// 	if (!online .activeSelf ) {
	// 		loginBtn.gameObject.SetActive (false);
	// 	}
	// }
}
