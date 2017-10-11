using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void Action ();

public class ActionManager : MonoBehaviour {

	public static ActionManager Current;
	public Button[] Buttons;
	//private List<Action> actionCalls = new List<Action>();
	private Action[] actionCalls = new Action[9] ;

	void Awake(){
		Current = this;
	}

	void Start () {
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

	public void AddButton(int index, Sprite pic, Action onClick)
	{
		Buttons [index].gameObject.SetActive (true);
		Buttons [index].GetComponent<Image> ().sprite = pic;
		//actionCalls.Add (onClick);
		actionCalls [index] = onClick;
	}

	public void OnButtonClick (int index)
	{
		actionCalls [index] ();
	}

	public void ClearButtons()
	{
		foreach (Button b in Buttons)
			b.gameObject.SetActive (false);

		for (int i = 0; i < actionCalls.Length; i++) {
			actionCalls [i] = null;
		}
	}
}
