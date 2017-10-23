using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingPlayerRaceController : MonoBehaviour {
	public PlayerSettingPlayerBarController barController;
	int SelectedDropListIndex{
        get;
        set;
    }
	public Racial SelectedRacial{
        get {
			//hard code here
            return Racial.human;
        }
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
            Debug.Log("SelectedDropListIndex:"+SelectedDropListIndex);
        });
    }
}
