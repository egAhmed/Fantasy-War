using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG .Tweening ;
using UnityEngine.UI ;

public class Tips : MonoBehaviour {
	private Text text;
	//[Range(0.2f,2f)]
	float fadeSpeed=0.8f; 
	// Use this for initialization
	void Start () {
		text = GetComponent <Text > ();
		//text.DOFade (1, 3);
		//
		text.color=new Color(text.color.r,text.color.g,text.color.b,0);
		//
		text.gameObject.SetActive(true );
	}
	
	// Update is called once per frame
	void Update () {
		if (text.color.a >= 1) {
			text.DOFade (0, fadeSpeed);
		} else if(text.color.a <= 0){
			text.DOFade (1, fadeSpeed);
		}
		if (Input .GetKeyDown (KeyCode .Space )) {
			text.gameObject.SetActive (false);
		}
	}
}
