using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolItemNode {  

	///<summary>  
	/// 对象  
	/// </summary>  
	public GameObject gameObject;  

	///<summary>  
	/// 存取时间  
	/// </summary>  
	public float aliveTime;  

	///<summary>  
	/// 销毁状态  
	/// </summary>  
	public bool destoryStatus;  

	public PoolItemNode(GameObject _gameObject){  
		this.gameObject = _gameObject;  
		this.destoryStatus = false;  
	}  
    

	///<summary>  
	/// 激活对象，将对象显示  
	/// </summary>  
	public GameObject Active(){  
		this.gameObject.SetActive (true);  
		this.destoryStatus = false;  
		aliveTime = 0;  
		return this.gameObject;  

	}  

	///<summary>  
	/// 销毁对象，不是真正的销毁  
	/// </summary>  
	public void Destroy(){//重置对象状态  
		this.gameObject.SetActive (false);  
		this.destoryStatus = true;  
		this.aliveTime = Time.time;  
	}  

	///<summary>  
	/// 检测是否超时，返回true或false，没有其他的操作，具体操作在Poolitem中  
	/// </summary>  
	public bool IsBeyondAliveTime(){  
		if (!this.destoryStatus)  
			return false;  
		if (Time.time - this.aliveTime >= PoolManager.Alive_Time) {  
			Debug.Log ("已超时!!!!!!");  
			return true;  
		}  
		return false;  
	}  
}  