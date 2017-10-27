using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingPlayerBarController : MonoBehaviour
{
    //
    public bool IsAI
    {
        get;
        set;
    }
    //
    public PlayerSettingPlayerNameController playerNameController;
    public PlayerSettingPlayerRaceController playerRaceController;
    public PlayerSettingPlayerTeamController playerTeamController;
    public PlayerSettingPlayerColorController playerColorController;

    void Awake()
    {
        playerNameController = GetComponentInChildren<PlayerSettingPlayerNameController>();
        playerRaceController = GetComponentInChildren<PlayerSettingPlayerRaceController>();
        playerTeamController = GetComponentInChildren<PlayerSettingPlayerTeamController>();
        playerColorController = GetComponentInChildren<PlayerSettingPlayerColorController>();
    }
}
