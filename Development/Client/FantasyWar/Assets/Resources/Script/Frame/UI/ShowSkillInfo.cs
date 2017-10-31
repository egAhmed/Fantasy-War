using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowSkillInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

	public GameObject UI_ItemInfo;
	public string info;

	public void OnPointerEnter (PointerEventData eventData){
		UI_ItemInfo.SetActive (true);
		//UI_ItemInfo.transform.position =new Vector3 (transform.position.x + -100,150,0);
		UI_ItemInfo.transform.GetChild (0).GetComponent<Text> ().text = info;
	}

	public void OnPointerExit (PointerEventData eventData){
		UI_ItemInfo.SetActive (false);
	}
}
