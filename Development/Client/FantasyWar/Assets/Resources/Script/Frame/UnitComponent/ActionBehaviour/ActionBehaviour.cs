using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBehaviour : MonoBehaviour {

	public KeyCode shortCutKey;

	public bool canRepeat;

	public abstract Action GetClickAction ();

	public int index;	//图标对应第几个按键

	public Sprite actionIcon;	//图标图片

	public void RunAction(KeyCode d){
		Action ac = GetClickAction ();
        Debug.Log(ac == null);
        ac ();
	}
}
