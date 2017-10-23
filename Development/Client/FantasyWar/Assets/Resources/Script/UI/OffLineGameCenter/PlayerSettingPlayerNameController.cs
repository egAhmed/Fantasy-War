using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingPlayerNameController : MonoBehaviour {
    public PlayerSettingPlayerBarController barController;
    Text playerNameText;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
        playerNameText = GetComponent<Text>();
    }
    // Use this for initialization
    public string PlayerName{
        get {
            if (playerNameText == null) {
                return "";
            }
            return playerNameText.text;
        }
        set { 
			if (playerNameText != null) {
                playerNameText.text=value;
            }
		}
    }
}
