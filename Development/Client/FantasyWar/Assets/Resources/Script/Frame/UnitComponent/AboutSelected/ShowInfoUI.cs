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
//        if (gameObject.GetComponent<Interactive>().Selected)
//        {
		//Debug.Log("循环");
		if (isShow && RTSGameUnitManager.ShareInstance.SelectedUnits [0].gameObject == this.gameObject) {
			//Debug.Log ("显示信息");
			showPanel.gameObject.SetActive (true);
			showPanel.Camerax.transform.position = RtsGameUnit.IconCameraPos;
			Action_Attack atkf = gameObject.GetComponent<Action_Attack> ();
			if (atkf == null) {
				showPanel.SetText (RtsGameUnit.playerInfo.name, RtsGameUnit.HP.ToString (), RtsGameUnit.maxHP.ToString (), null);
			}
			else {
				showPanel.SetText (RtsGameUnit.playerInfo.name, RtsGameUnit.HP.ToString (), RtsGameUnit.maxHP.ToString (), "10");
			}
		}

        //}
    }

	public override void Select ()
	{
		//Debug.Log ("信息面板激活");
		showPanel.theUI.gameObject.SetActive (true);
		isShow = true;
	}
	
	public override void Deselect ()
	{
		isShow = false;
		if (RTSGameUnitManager.ShareInstance.SelectedUnits != null && RTSGameUnitManager.ShareInstance.SelectedUnits.Count == 0) {
			showPanel.Clear ();
			showPanel.theUI.gameObject.SetActive (false);
		}
	}

	void OnDestroy(){
		if (showPanel == null)
			return;
		showPanel.Clear ();
	}
}
