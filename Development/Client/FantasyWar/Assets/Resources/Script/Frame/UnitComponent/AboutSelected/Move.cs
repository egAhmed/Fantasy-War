using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Interaction {

	private bool selected = false;

	public override void Deselect ()
	{
		selected = false;
		//TODO
		//被取消選擇后，註銷鼠標右鍵移動事件
	}

	public override void Select ()
	{
		selected = true;
		//TODO
		//被選擇后，註冊鼠標右鍵移動事件
	}

	public void MoveTo(){
		if (selected) {
			Debug.Log ("假装移动");
		}
		//TODO
		//移動到鼠標右鍵點擊的的位置
	}
}
