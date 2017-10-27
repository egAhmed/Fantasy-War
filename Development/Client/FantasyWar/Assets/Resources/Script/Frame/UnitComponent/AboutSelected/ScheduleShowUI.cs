using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleShowUI : Interaction {
	
	ScheduleShow ss;
	bool isShow;
	RTSBuilding rb;

	void Start(){
		ss = ScheduleShow.Current;
		rb = gameObject.GetComponent<RTSBuilding> ();
	}

	public override void Select ()
	{
		Debug.Log ("进度条激活");
		isShow = true;
	}

	public override void Deselect ()
	{
		isShow = false;
		ss.unitIcon.fillAmount = 0;
	}

	void Update(){
		//if (gameObject.GetComponent<Interactive>().Selected)
		if (isShow && RTSGameUnitManager.ShareInstance.SelectedUnits [0].gameObject == this.gameObject)
		{
			if (rb.isProducting) {
				ss.unitIcon.fillAmount = rb.schedule;
			}
			else {
				ss.unitIcon.fillAmount = 0;
			}
		}

	}

}
