using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingPlayerBarListController : MonoBehaviour {
	PlayerSettingPlayerBarController[] playerSettingPlayerBarControllers;
    //
    public Button loginBtn;
    public Button playerAddBtn;
    public Button playerMinusBtn;
	//
	int AddedPlayerNum{
        get;
        set;
    }
    //
    // Use this for initialization
    //
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
	{
        playerSettingPlayerBarControllers = GetComponentsInChildren<PlayerSettingPlayerBarController>();
        if (loginBtn == null) {
            loginBtn = GameObject.Find("Btn_start").GetComponent<Button>();
        }
		if (playerAddBtn == null) {
            playerAddBtn = GameObject.Find("Btn_+").GetComponent<Button>();
        }
		if (playerMinusBtn == null) {
            playerMinusBtn = GameObject.Find("Btn_-").GetComponent<Button>();
        }
    }
	//
	void Start()
	{
        // Debug.Log("playerSettingPlayerBarControllers => "+playerSettingPlayerBarControllers.Length);
        if (playerSettingPlayerBarControllers != null) { 
			//
        for(int i=0;i < playerSettingPlayerBarControllers.Length;i++) {
                //
                if (i == 0) {
                    continue;
                }
                PlayerSettingPlayerBarController bc = playerSettingPlayerBarControllers[i];
                bc.gameObject.SetActive(false);
				//
            }
			//
		}
        //
        AddedPlayerNum = 1;
        //
        if (loginBtn != null) {
            loginBtn.onClick.AddListener(()=>{
                //
                Debug.Log("Fuck start");
                //
            });
        }
		if (playerAddBtn != null) {
            playerAddBtn.onClick.AddListener(()=>{
				//
                Debug.Log("Fuck add");
                //
                playerAdd();
				//
            });
        }
		if (playerMinusBtn != null) {
             playerMinusBtn.onClick.AddListener(()=>{
				//
                Debug.Log("Fuck minus");
                 //
                 playerMinus();
                 //
             });
        }
		//
    }

	void playerAdd() {
        if (AddedPlayerNum >=playerSettingPlayerBarControllers.Length) {
            return;
        }
        //
        playerSettingPlayerBarControllers[AddedPlayerNum++].gameObject.SetActive(true);
		//
    }

	void playerMinus() {
        if (AddedPlayerNum <=1) {
            return;
        }
        //
        playerSettingPlayerBarControllers[--AddedPlayerNum].gameObject.SetActive(false);
		//
    }
}
