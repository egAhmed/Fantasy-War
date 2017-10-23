using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingPlayerBarController : MonoBehaviour {
    //
    public Color PlayerColor{
        get;
        set;
    }
    public string PlayerName{
		get;
        set;
	}
	public int PlayerGroup{
		get;
        set;
	}

    public Racial PlayerRaceType{
        get;
        set;
    }

    //
    PlayerSettingPlayerNameController playerNameController;
    PlayerSettingPlayerRaceController playerRaceController;
    PlayerSettingPlayerTeamController playerTeamController;
    PlayerSettingPlayerColorController playerColorController;
	
	void Awake()
	{
        playerNameController = GetComponentInChildren<PlayerSettingPlayerNameController>();
        playerRaceController = GetComponentInChildren<PlayerSettingPlayerRaceController>();
        playerTeamController = GetComponentInChildren<PlayerSettingPlayerTeamController>();
        playerColorController = GetComponentInChildren<PlayerSettingPlayerColorController>();
    }
}
