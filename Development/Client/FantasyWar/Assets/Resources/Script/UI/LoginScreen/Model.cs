using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Model : MonoBehaviour {

	RectTransform _rect;
	Tweener _tweener;

	void Awake(){
		_rect = transform as RectTransform;
		_tweener = _rect.DOLocalMoveX (0, 1f);
		_tweener.SetEase (Ease.Linear);
		_tweener.SetAutoKill (false);
		_tweener.Pause ();
	}
	// Use this for initialization
//	void Start () {
//		_rect = transform as RectTransform;
//		_tweener = _rect.DOLocalMoveX (0, 1f);
//		_tweener.SetEase (Ease.Linear);
//		_tweener.SetAutoKill (false);
//		_tweener.Pause ();
//	}
	
	// Update is called once per frame
	void Update () {
		if (Input .GetKeyDown (KeyCode.Space )) {
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
