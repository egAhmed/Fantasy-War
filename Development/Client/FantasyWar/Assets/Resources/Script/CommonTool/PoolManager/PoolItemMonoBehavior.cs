using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemMonoBehavior : PoolItem {

    /// <summary>
    /// 使用默认超时时间和默认最大对象池数量初始化
    /// </summary>
    /// <param name="_name">Name.</param>
    public PoolItemMonoBehavior(string _name):base(_name)
    {

    }

    /// <summary>
    /// 使用自定义超时时间和默认最大对象池数量
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItemMonoBehavior(string _name, int Alive_Time):base(_name,Alive_Time)
    {

    }

    /// <summary>
    /// 使用默认超时时间和自定义对象池上限
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItemMonoBehavior(int PoolItemMaxNum, string _name):base(PoolItemMaxNum,_name)
    {

    }

    /// <summary>
    /// 使用自定义超时时间和自定义对象池上限
    /// </summary>
    /// <param name="PoolItemMaxNum"></param>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public PoolItemMonoBehavior(int PoolItemMaxNum, string _name, int Alive_Time):base(PoolItemMaxNum,_name,Alive_Time)
    {

    }

    /// <summary>  
    /// 添加对象，往同意对象池里添加对象  
    /// </summary>  
    public void PushObject(MonoBehaviour _mono)
    {
        int hashKey = _mono.GetHashCode();
        if (!this.objectList.ContainsKey(hashKey))
        {
            this.objectList.Add(hashKey, new PoolItemNodeMonoBehavior(_mono));
            if (this.objectList.Count > PoolItemMaxNum)
                Debug.LogWarning("已超出对象池最大容量");
        }
        else
        {
            ((PoolItemNodeMonoBehavior)this.objectList[hashKey]).Active();
        }
    }

    /// <summary>  
	/// 销毁对象，调用PoolItemTime中的destroy，即也没有真正销毁  
	/// </summary>  
	public void DestoryObject(MonoBehaviour _mono)
    {
        int hashKey = _mono.GetHashCode();
        if (this.objectList.ContainsKey(hashKey))
        {
            ((PoolItemNodeMonoBehavior)this.objectList[hashKey]).Destroy();
        }
    }

    /// <summary>  
    /// 返回没有真正销毁的第一个对象（即池中的destoryStatus为true的对象）  
    /// </summary>  
    public MonoBehaviour GetObject()
    {
        if (this.objectList == null || this.objectList.Count == 0)
        {
            return null;
        }
        foreach (PoolItemNode poolIT in this.objectList.Values)
        {
            if (poolIT.destoryStatus)
            {
                return ((PoolItemNodeMonoBehavior)poolIT).Active();
            }
        }
        return null;
    }

    /// <summary>  
    /// 移除并销毁单个对象，真正的销毁对象!!  
    /// </summary>  
    public void RemoveObject(Object obj)
    {
        int hashKey = obj.GetHashCode();
        if (this.objectList.ContainsKey(hashKey))
        {
            Object.Destroy(obj);
            this.objectList.Remove(hashKey);
        }
    }

    /// <summary>  
    /// 销毁对象，把所有的同类对象全部删除，真正的销毁对象!!  
    /// </summary>  
    public void Destory()
    {
        IList<PoolItemNode> poolIList = new List<PoolItemNode>();
        foreach (PoolItemNode poolIT in this.objectList.Values)
        {
            poolIList.Add(poolIT);
        }
        while (poolIList.Count > 0)
        {
            if (poolIList[0] != null && ((PoolItemNodeMonoBehavior)poolIList[0])._mono != null)
            {
                GameObject.Destroy(((PoolItemNodeMonoBehavior)poolIList[0])._mono);
                poolIList.RemoveAt(0);
            }
        }
        this.objectList = new Dictionary<int, PoolItemNode>();
    }


    /// <summary>  
    /// 超时检测，超时的就直接删除了，真正的删除!!  
    /// </summary>  
    public void BeyondObject()
    {
        IList<PoolItemNode> beyondTimeList = new List<PoolItemNode>();
        foreach (PoolItemNode poolIT in this.objectList.Values)
        {
            if (poolIT.IsBeyondAliveTime())
            {
                beyondTimeList.Add(poolIT);
            }
        }
        int beyondTimeCount = beyondTimeList.Count;
        for (int i = 0; i < beyondTimeCount; i++)
        {
            this.RemoveObject(((PoolItemNodeMonoBehavior)beyondTimeList[i])._mono);
        }
    }
}
