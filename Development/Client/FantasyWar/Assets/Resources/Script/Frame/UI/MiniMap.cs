using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

	public RectTransform ViewPort;	//攝像機的視口

	// Use this for initialization
	void Start () {
		
	}

	/// <summary>
	/// 根據世界坐標的X、Z軸將坐標轉換到小地圖的2D坐標上
	/// </summary>
	/// <returns>The position to map.</returns>
	/// <param name="point">Point.</param>
	public Vector2 WorldPositionToMap(Vector3 point)
	{
		//TODO
		return Vector2.zero;
	}

	/// <summary>
	/// 在這裡將主攝像機的位置用框框表示在小地圖上，表示當前屏幕的視野
	/// </summary>
	void Update (){
	}
}
