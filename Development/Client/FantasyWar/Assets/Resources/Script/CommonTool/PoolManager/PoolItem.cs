using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem{  

	/// <summary>  
	/// 名称，作为标识  
	/// </summary>  
	public string name;  

	/// <summary>
	/// 超时时间
	/// </summary>
	public int Alive_Time;

    /// <summary>
    /// 对象最大上限数量
    /// </summary>
    public int PoolItemMaxNum;
	/// <summary>  
	/// 对象列表，存储同一个名称的所有对象  
	/// </summary>  
	public Dictionary<int, PoolItemNode> objectList;  

	/// <summary>
	/// 使用默认超时时间和默认最大对象池数量初始化
	/// </summary>
	/// <param name="_name">Name.</param>
	public PoolItem(string _name)  
	{  
		this.name = _name;
		this.Alive_Time = PoolManager.Alive_Time;
        this.PoolItemMaxNum = PoolManager.PoolItemMaxNum;
		this.objectList = new Dictionary<int, PoolItemNode> ();  
	}  

    /// <summary>
    /// 使用自定义超时时间和默认最大对象池数量
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
	public PoolItem(string _name,int Alive_Time)
    {
        this.name = _name;
        this.Alive_Time = Alive_Time;
        this.PoolItemMaxNum = PoolManager.PoolItemMaxNum;
        this.objectList = new Dictionary<int, PoolItemNode>();
    }

    /// <summary>
    /// 使用默认超时时间和自定义对象池上限
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItem(int PoolItemMaxNum,string _name)
    {
        this.name = _name;
        this.Alive_Time = PoolManager.Alive_Time;
        this.PoolItemMaxNum =PoolItemMaxNum;
        this.objectList = new Dictionary<int, PoolItemNode>();
    }

    /// <summary>
    /// 使用自定义超时时间和自定义对象池上限
    /// </summary>
    /// <param name="PoolItemMaxNum"></param>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItem(int PoolItemMaxNum, string _name, int Alive_Time)
    {
        this.name = _name;
        this.Alive_Time = Alive_Time;
        this.PoolItemMaxNum = PoolItemMaxNum;
        this.objectList = new Dictionary<int, PoolItemNode>();
    }


    /// <summary>  
    /// 添加对象，往同意对象池里添加对象  
    /// </summary>  
    public void PushObject(GameObject _gameObject)  
	{  
		int hashKey = _gameObject.GetHashCode ();  
		if (!this.objectList.ContainsKey (hashKey)) {  
			this.objectList.Add (hashKey, new PoolItemNode (_gameObject));
            if (this.objectList.Count > PoolItemMaxNum)
                Debug.LogWarning("已超出对象池最大容量");  
		} else {  
			this.objectList [hashKey].Active ();  
		}  
	}  

	/// <summary>  
	/// 销毁对象，调用PoolItemTime中的destroy，即也没有真正销毁  
	/// </summary>  
	public void DestoryObject(GameObject _gameObject){  
		int hashKey = _gameObject.GetHashCode ();  
		if (this.objectList.ContainsKey (hashKey)) {  
			this.objectList [hashKey].Destroy ();  
		}  
	}  

	/// <summary>  
	/// 返回没有真正销毁的第一个对象（即池中的destoryStatus为true的对象）  
	/// </summary>  
	public GameObject GetObject(){  
		if (this.objectList == null || this.objectList.Count == 0) {  
			return null;  
		}  
		foreach (PoolItemNode poolIT in this.objectList.Values) {  
			if (poolIT.destoryStatus) {  
				return poolIT.Active ();  
			}  
		}  
		return null;  
	}  

	/// <summary>  
	/// 移除并销毁单个对象，真正的销毁对象!!  
	/// </summary>  
	public void RemoveObject(GameObject _gameObject){  
		int hashKey = _gameObject.GetHashCode ();  
		if (this.objectList.ContainsKey (hashKey)) {  
			GameObject.Destroy (_gameObject);  
			this.objectList.Remove (hashKey);  
		}  
	}  

	/// <summary>  
	/// 销毁对象，把所有的同类对象全部删除，真正的销毁对象!!  
	/// </summary>  
	public void Destory(){  
		IList<PoolItemNode> poolIList = new List<PoolItemNode> ();  
		foreach (PoolItemNode poolIT in this.objectList.Values) {  
			poolIList.Add (poolIT);  
		}  
		while (poolIList.Count > 0) {  
			if (poolIList [0] != null && poolIList [0].gameObject != null) {  
				GameObject.Destroy (poolIList [0].gameObject);  
				poolIList.RemoveAt (0);  
			}  
		}  
		this.objectList = new Dictionary<int, PoolItemNode> ();  
	}  

	/// <summary>  
	/// 超时检测，超时的就直接删除了，真正的删除!!  
	/// </summary>  
	public void BeyondObject(){  
		IList<PoolItemNode> beyondTimeList = new List<PoolItemNode> ();  
		foreach (PoolItemNode poolIT in this.objectList.Values) {  
			if (poolIT.IsBeyondAliveTime ()) {  
				beyondTimeList.Add (poolIT);  
			}  
		}  
		int beyondTimeCount = beyondTimeList.Count;  
		for (int i = 0; i < beyondTimeCount; i++) {  
			this.RemoveObject (beyondTimeList [i].gameObject);  
		}  
	}  
}  
