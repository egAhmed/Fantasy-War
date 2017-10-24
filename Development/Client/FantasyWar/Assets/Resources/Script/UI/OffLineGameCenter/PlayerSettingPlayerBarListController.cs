using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingPlayerBarListController : MonoBehaviour
{
    PlayerSettingPlayerBarController[] playerSettingPlayerBarControllers;
    //
    public Button loginBtn;
    public Button playerAddBtn;
    public Button playerMinusBtn;
    //
    int AddedPlayerNum
    {
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
        if (loginBtn == null)
        {
            loginBtn = GameObject.Find("Btn_start").GetComponent<Button>();
        }
        if (playerAddBtn == null)
        {
            playerAddBtn = GameObject.Find("Btn_+").GetComponent<Button>();
        }
        if (playerMinusBtn == null)
        {
            playerMinusBtn = GameObject.Find("Btn_-").GetComponent<Button>();
        }
    }
    //
    void Start()
    {
        // Debug.Log("playerSettingPlayerBarControllers => "+playerSettingPlayerBarControllers.Length);
        if (playerSettingPlayerBarControllers != null)
        {
            //
            for (int i = 0; i < playerSettingPlayerBarControllers.Length; i++)
            {
                //
                PlayerSettingPlayerBarController bc = playerSettingPlayerBarControllers[i];
                //
                if (bc == null)
                {
                    continue;
                }
                //
                if (i == 0)
                {
                    bc.IsAI = false;
                    bc.playerTeamController.SelectedTeamNumber=1;
                    bc.playerTeamController.setSelectable(false);
                    continue;
                }
                else
                {
                    bc.IsAI = true;
                    bc.gameObject.SetActive(false);
                    bc.playerTeamController.SelectedTeamNumber=2;
                    bc.playerTeamController.setSelectable(false);
                }
                //
            }
            //
        }
        //
        AddedPlayerNum = 1;
        //
        if (loginBtn != null)
        {
            loginBtn.onClick.AddListener(() =>
            {
                //
               // Debug.Log("Fucking start");
                //
                startBattle();
                //
            });
        }
        if (playerAddBtn != null)
        {
            playerAddBtn.onClick.AddListener(() =>
            {
                //
                //Debug.Log("Fucking add");
                //
                playerAdd();
                //
            });
        }
        if (playerMinusBtn != null)
        {
            playerMinusBtn.onClick.AddListener(() =>
            {
                //
               // Debug.Log("Fucking minus");
                //
                playerMinus();
                //
            });
        }
        //
    }

    void startBattle() {
        //
        RTSSceneManager.ShareInstance.loadScene(2);
        //
        if (playerSettingPlayerBarControllers != null)
        {
            //
            List<PlayerInfo> playerInfos=new List<PlayerInfo>();
            //
            for (int i = 0; i < playerSettingPlayerBarControllers.Length; i++)
            {
                //
                PlayerSettingPlayerBarController bc = playerSettingPlayerBarControllers[i];
                PlayerInfo playerInfoTemp = new PlayerInfo();
                //
                if (bc == null||!bc.gameObject.activeSelf)
                {
                    continue;
                }
                //
                playerInfoTemp.name = bc.playerNameController.PlayerName;
                playerInfoTemp.racial = bc.playerRaceController.SelectedRacial;
                playerInfoTemp.accentColor = bc.playerColorController.SelectedColor;
                //
                if (i == 0)
                {
                    playerInfoTemp.isAI = false;
                    //
                    TestingScript.currentPlayer = playerInfoTemp;
                    TestingScript.virCurrentName = playerInfoTemp.name;
                    //
                }
                else
                {
                    bc.IsAI = true;
                    playerInfoTemp.isAI = true;
                }
                //
                playerInfos.Add(playerInfoTemp);
                //
            }
            //
            TestingScript.playerInfos = playerInfos;
            //
        }
        //
    }

    void playerAdd()
    {
        //
        if (AddedPlayerNum >= playerSettingPlayerBarControllers.Length)
        {
            return;
        }
        //
        playerSettingPlayerBarControllers[AddedPlayerNum++].gameObject.SetActive(true);
        //
    }

    void playerMinus()
    {
        //
        if (AddedPlayerNum <= 1)
        {
            return;
        }
        //
        playerSettingPlayerBarControllers[--AddedPlayerNum].gameObject.SetActive(false);
        //
    }
}
