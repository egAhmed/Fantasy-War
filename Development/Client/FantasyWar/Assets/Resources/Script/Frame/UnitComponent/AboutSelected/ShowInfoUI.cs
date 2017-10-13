using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoUI : Interaction {

	ObjectInfoShow showPanel;
	UnitInfo unitinfo;
	bool isShow;

	void Start(){
		unitinfo = gameObject.GetComponent<UnitInfo> ();
		showPanel = ObjectInfoShow.Current;
		isShow = false;
	}

	void Update(){
		if (isShow && TeamManager.ShareInstance.currentSelections [0] == this.gameObject) {
			showPanel.SetText (unitinfo.unitName, unitinfo.HP.ToString(), unitinfo.belong.accentColor.ToString());
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
