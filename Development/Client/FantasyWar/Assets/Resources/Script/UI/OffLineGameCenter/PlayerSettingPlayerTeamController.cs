using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingPlayerTeamController : MonoBehaviour {
	public PlayerSettingPlayerBarController barController;
	public int SelectedTeamNumber{
        get
        {
            switch (SelectedDropListIndex) { 
				default:
                    return 2;
            }
        }
        set {
            if (dropdownList != null) {
                dropdownList.value = value-1;
            }
        }
    }
	int SelectedDropListIndex{
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
            SelectedDropListIndex = x;
            //Debug.Log("SelectedTeamNumber:"+SelectedTeamNumber);
        });
    }

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
    public void setSelectable(bool flag) { 
        dropdownList.interactable = flag;
	}
}
