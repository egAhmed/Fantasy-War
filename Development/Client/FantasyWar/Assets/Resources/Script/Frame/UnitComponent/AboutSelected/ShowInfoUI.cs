using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoUI : Interaction {

	ObjectInfoShow showPanel;
	RTSGameUnit RtsGameUnit;
	bool isShow;

	void Start(){
		RtsGameUnit = gameObject.GetComponent<RTSGameUnit> ();
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
                showPanel.SetText(RtsGameUnit.unitInfo.unitName, RtsGameUnit.HP.ToString(), RtsGameUnit.playerInfo.accentColor.ToString());
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
