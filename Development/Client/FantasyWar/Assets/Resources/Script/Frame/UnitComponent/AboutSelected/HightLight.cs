using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight : Interaction {

	public override void Select ()
	{
		//TODO
		//顯示被選框
	}

	public override void Deselect ()
	{
		//TODO
		//取消被選框顯示
	}

	public void Start(){
		Deselect ();
	}
}
