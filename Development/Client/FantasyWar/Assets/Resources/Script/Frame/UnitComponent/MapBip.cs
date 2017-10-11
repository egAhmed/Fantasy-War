using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在小地圖上顯示这个单位的位置，
/// </summary>
public class MapBip : MonoBehaviour {

	private GameObject blip;	//在小地图上面的图标

	void Start () {
		//TODO
		//初始化blip并放到小地图下
	}
	
	// Update is called once per frame
	void Update () {
		//即时更新位置
	}

	void OnDestroy(){
		//销毁或者失活blip
	}
}
