using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterInputEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//TODO
		//在InputManager中
		//註冊點選方法
		//註冊框選方法
		//註冊編隊方法
		//註冊選隊方法
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Mouse0,PointSelect);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PointSelect(KeyCode k){
		//TODO
		//點選
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit))
			return;

		Interactive interactive = hit.transform.GetComponent<Interactive> ();
		if (interactive == null)
			return;

		interactive.Select ();
	}

	void RectSelect(){
		//TODO
		//框選
	}

	void MakeTeam(){
		//TODO
		//編隊方法
	}

	void SelectTeam(){
		//TODO
		//選取隊伍方法
	}
}
