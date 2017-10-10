using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoUI : Interaction {

	ObjectInfoShow showPanel;
	bool isShow;

	void Start(){
		showPanel = ObjectInfoShow.Current;
		isShow = false;
	}

	void Update(){
		if (isShow) {
			//在UI面板上显示单位信息
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
}
