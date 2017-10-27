using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem
{

    /// <summary>  
    /// 名称，作为标识  
    /// </summary>  
    public string name;

    private float aliveTime = -1;
    /// <summary>
    /// 超时时间
    /// </summary>
    public float Alive_Time
    {
        set
        {
            //在poolmanager中启动相关协程
            if (aliveTime < 0 && value > 0)
            {
                aliveTime = value;
                PoolManager.ShareInstance.StartCoroutine(PoolManager.ShareInstance.CheckBeyondTime(this));
            }

            aliveTime = value;
        }
        get
        {
            return aliveTime;
        }
    }

    /// <summary>
    /// 对象最大上限数量
    /// </summary>
    public int PoolItemMaxNum;
    /// <summary>  
    /// 对象列表，存储同一个名称的所有对象  
    /// </summary>  
    private Dictionary<int, PoolItemNode> objectList;

    public Dictionary<int, PoolItemNode> ObjectList
    {
        get
        {
            if (objectList == null)
                objectList = new Dictionary<int, PoolItemNode>();
            return objectList;
        }
        set
        {
            objectList = value;
        }
    }

    /// <summary>
    /// 使用默认超时时间和默认最大对象池数量初始化
    /// </summary>
    /// <param name="_name">Name.</param>
    public PoolItem(string _name)
    {
        this.name = _name;
        this.Alive_Time = PoolManager.Alive_Time;
        this.PoolItemMaxNum = PoolManager.PoolItemMaxNum;
    }

    /// <summary>
    /// 使用自定义超时时间和默认最大对象池数量
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItem(string _name, float Alive_Time)
    {
        this.name = _name;
        this.Alive_Time = Alive_Time;
        this.PoolItemMaxNum = PoolManager.PoolItemMaxNum;
    }

    /// <summary>
    /// 使用默认超时时间和自定义对象池上限
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItem(int PoolItemMaxNum, string _name)
    {
        this.name = _name;
        this.Alive_Time = PoolManager.Alive_Time;
        this.PoolItemMaxNum = PoolItemMaxNum;
    }

    /// <summary>
    /// 使用自定义超时时间和自定义对象池上限
    /// </summary>
    /// <param name="PoolItemMaxNum"></param>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItem(int PoolItemMaxNum, string _name, float Alive_Time)
    {
        this.name = _name;
        this.Alive_Time = Alive_Time;
        this.PoolItemMaxNum = PoolItemMaxNum;
    }

}
