using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoUI : Interaction {

	ObjectInfoShow showPanel;
	RTSGameUnit RTSGameUnit;
	bool isShow;

	void Start(){
		RTSGameUnit = gameObject.GetComponent<RTSGameUnit> ();
		showPanel = ObjectInfoShow.Current;
		isShow = false;
	}
	void Update(){
        //if (isShow && TeamManager.ShareInstance.currentSelections [0] == this.gameObject) {
//        Debug.Log(RTSGameUnitManager.ShareInstance.SelectedUnits.Count);
        //
        if (gameObject.GetComponent<Interactive>().Selected)
        {
            if (isShow && RTSGameUnitManager.ShareInstance.SelectedUnits[0] == this.gameObject)
            {

                showPanel.SetText(RTSGameUnit.unitName, RTSGameUnit.HP.ToString(), RTSGameUnit.playerInfo.accentColor.ToString());
            }
        }
    }

	public override void Select ()
	{
		isShow = true;
	}
	
	public override void Deselect ()
	{
		showPanel.Clear ();
		isShow = false;
	}

	void OnDestroy(){
		if (showPanel == null)
			return;
		showPanel.Clear ();
	}
}
