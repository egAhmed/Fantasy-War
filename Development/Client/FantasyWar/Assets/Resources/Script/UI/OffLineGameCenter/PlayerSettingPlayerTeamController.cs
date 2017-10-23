using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingPlayerTeamController : MonoBehaviour {
	public int SelectedTeamNumber{
        get;
        set;
    }
    Dropdown dropdownList;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
	{
       dropdownList= GetComponentInChildren<Dropdown>();
        dropdownList.onValueChanged.AddListener((x)=>{
            SelectedTeamNumber = x;
            Debug.Log("SelectedTeamNumber:"+SelectedTeamNumber);
        });
    }
}
