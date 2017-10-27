using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSOperations {

	public static void PointSelect(KeyCode k){
		//TODO
		//點選
//		if (TeamManager.ShareInstance.currentSelections.Count > 0) {
//			for (int i = 0; i < TeamManager.ShareInstance.currentSelections.Count; i++) {
//				if (TeamManager.ShareInstance.currentSelections [i] != null) {
//					TeamManager.ShareInstance.currentSelections [i].GetComponent<Interactive> ().Deselect ();
//				}
//			}
//			TeamManager.ShareInstance.currentSelections.Clear ();
//		
//		}

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit))
			return;

		Interactive interactive = hit.transform.GetComponent<Interactive> ();
		if (interactive == null)
			return;

		interactive.Select ();
	}

	public static void RectSelect(){
		//TODO
		//框選
	}

	public static void MakeTeam(){
		//TODO
		//編隊方法
	}

	public static void SelectTeam(){
		//TODO
		//選取隊伍方法
	}




}
