using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemNodeGameObject : PoolItemNode
{
    ///<summary>  
    /// 对象  
    /// </summary>  
    public GameObject gameObject;

    /// <summary>
    /// 设定对象在对象池中存在的时间
    /// </summary>
    /// <param name="_gameObject"></param>
    /// <param name="aliveTime"></param>
    public PoolItemNodeGameObject(GameObject _gameObject,PoolItem _poolItem)
    {
        this.gameObject = _gameObject;
        this.destoryStatus = false;
        this._poolItem = _poolItem;
        startActiveTime = Time.time;
    }


    /// <summary>
    /// 激活对象，将对象显示 
    /// </summary>
    /// <param name="aliveTime"></param>
    /// <returns></returns>
    public GameObject Active()
    {
        this.gameObject.SetActive(true);
        this.destoryStatus = false;
        startActiveTime = Time.time;
        return this.gameObject;

    }

    ///<summary>  
    /// 销毁对象，不是真正的销毁  
    /// </summary>  
    public void Destroy()
    {//重置对象状态  
        this.gameObject.SetActive(false);
        this.destoryStatus = true;
    }
}
