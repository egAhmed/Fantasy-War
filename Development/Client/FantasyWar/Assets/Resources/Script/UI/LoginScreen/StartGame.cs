using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG .Tweening ;

public class StartGame : MonoBehaviour {
	RectTransform _rect;
	Tweener _tweener;
	public GameObject model;
	// Use this for initialization
	void Awake () {
		_rect = transform as RectTransform;
		_tweener =  _rect.DOLocalMoveY (-220, 1f);
		_tweener.SetEase (Ease.Linear);
		_tweener.SetAutoKill (false);
		_tweener.Pause ();
	}

	// Update is called once per frame
	void Update () {
		if (!model.activeSelf ) {
			Rect ();
		}
	}
	public void Rect()
	{

		//		_rect.DOLocalMoveX (0, 1f,true);
		_tweener.PlayForward();
		//		_tweener.PlayBackwards();
	}
}
