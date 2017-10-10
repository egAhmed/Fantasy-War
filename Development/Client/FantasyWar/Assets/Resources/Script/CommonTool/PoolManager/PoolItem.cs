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
    
}  
