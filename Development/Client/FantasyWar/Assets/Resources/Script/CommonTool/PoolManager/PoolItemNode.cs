using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolItemNode {

    /// <summary>
    /// 所属的对象池
    /// </summary>
    public PoolItem _poolItem;
    
    /// <summary>
    /// 激活时间
    /// </summary>
    public float startActiveTime;

	///<summary>  
	/// 销毁状态  
	/// </summary>  
	public bool destoryStatus;  


    





	///<summary>  
	/// 检测是否超时，返回true或false，没有其他的操作，具体操作在Poolitem中  
	/// </summary>  
	public bool IsBeyondAliveTime(){
		if (this.destoryStatus)  
			return false;  
		if (Time.time - startActiveTime >= _poolItem.Alive_Time) {  
			Debug.Log ("已超时!!!!!!");  
			return true;  
		}  
		return false;  
	}  
}  