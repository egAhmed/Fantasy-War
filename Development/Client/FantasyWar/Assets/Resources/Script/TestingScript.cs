﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

	// Use this for initialization

	public static string virCurrentName = "xxixxxlx";
	public static PlayerInfo currentPlayer = null;


	void Start () {
		//加载所有表格
		Settings.TableManage.Start ();
		//InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.V,AIFunction.BuildTest);

		// RTSGamePlayManager.ShareInstance.build();
        //RTSFogSystemManager.ShareInstance.addFogEffectToCamera();
        //
        //RTSManager.ShareInstance.testingCreatePlayInfo();
        RTSGameUnitBloodBarManager.ShareInstance.hideBloodbar();
        //
        // InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.C,open);
        // InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.V,close);
        //InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.C, poolManagerTesting);
        InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.C, getHurtTesting);
        //
        //testingGetHurtUnit = GameObject.Find("testingGetHurtUnit").GetComponent<RTSGameUnit>();

//		PlayerInfo p1 = new PlayerInfo ();
//		p1.isAI = true;
//		p1.name = "p1";
//		//p1.location =
//		p1.accentColor = Color.cyan;
		LoadPlayer();

		foreach (PlayerInfo item in PlayerInfoManager.ShareInstance.Players) {
			if (item.name == virCurrentName) {
				PlayerInfoManager.ShareInstance.currentPlayer = item;
			}
		}

		PlayerAIController pac = gameObject.AddComponent<PlayerAIController> ();
		pac.playerInfo = PlayerInfoManager.ShareInstance.Players[0];
    }

	void Update(){
	}

	void LoadPlayer(){
		PlayerInfoManager pim = PlayerInfoManager.ShareInstance;
		pim.AddPlayer ("xxixxxlx", Color.cyan);
		pim.AddAIPlayerInfo ();
		pim.InitPlayers ();
		pim.LoadInitBuild ();
		pim.LoadAIController ();
	}


    RTSGameUnit testingGetHurtUnit;
    private void getHurtTesting(KeyCode key) {
        //
        testingGetHurtUnit.getHurt(null);
        // PoolManager.ShareInstanceance.PushObject("testing",1,obj);
        //
    }

    private void poolManagerTesting(KeyCode key) {
        //
        GameObject obj = new GameObject();
        // PoolManager.ShareInstanceance.PushObject("testing",1,obj);
        //
    }

//    private void buildingTesting(KeyCode key) {
//        RTSGamePlayManager.ShareInstance.build();
//    }

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
