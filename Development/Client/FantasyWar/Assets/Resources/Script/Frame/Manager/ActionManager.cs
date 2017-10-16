using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//点击操作面板的按键时调用的委托
public delegate void Action ();

/// <summary>
/// 管理右下角操作面板
/// </summary>
public class ActionManager : UnitySingleton<ActionManager> {

	//9个键对应9个委托
	public Button[] Buttons=new Button[9];
	public ActionBehaviour[] ActionBehaviours = new ActionBehaviour[9]; 
	private Action[] actionCalls = new Action[9] ;

	//为按键注册委托
	public void Start () {
		for (int i = 0; i < actionCalls.Length; i++) {
			actionCalls [i] = null;
		}
		for (int i = 0; i < Buttons.Length; i++) {
			int index = i;
			Buttons[index].onClick.AddListener(
				delegate() {
					OnButtonClick (index);
				}
			);
		}

		ClearButtons ();
	}

	public void AddBtn(int index,Button btn){
		Buttons [index] = btn;
	}

	//为按钮添加图标，在委托列表注册相应事件
	public void AddButton(int index, Sprite pic, Action onClick)
	{
		//Debug.Log ("添加按键");
		Buttons [index].gameObject.SetActive (true);
		Buttons [index].GetComponent<Image> ().sprite = pic;
		//actionCalls.Add (onClick);
		actionCalls [index] = onClick;
	}

	public void RemoveDelegate(int index,Action onClick){
		actionCalls [index] -= onClick;
	}

	public void AddButtonDelegate(int index,Action onClick){
		actionCalls [index] += onClick;
	}

	public void OnButtonClick (int index)
	{
		actionCalls [index] ();
	}

	//清空委托、图标
	public void ClearButtons()
	{
		foreach (Button b in Buttons) {
			if (b == null)
				continue;
			b.gameObject.SetActive (false);
		}

		for (int i = 0; i < actionCalls.Length; i++) {
			actionCalls [i] = null;
		}
	}
}
