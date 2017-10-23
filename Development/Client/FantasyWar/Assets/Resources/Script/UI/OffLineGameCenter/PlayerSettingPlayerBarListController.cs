using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingPlayerBarListController : MonoBehaviour {
	PlayerSettingPlayerBarController[] playerSettingPlayerBarControllers;
	//
	//
    // Use this for initialization
	//
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
        playerSettingPlayerBarControllers = GetComponentsInChildren<PlayerSettingPlayerBarController>();
    }
}
