using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemNodeMonoBehavior : PoolItemNode {
    ///<summary>  
    /// 对象  
    /// </summary>  
    public MonoBehaviour _mono;

    /// <summary>
    /// 设定对象在对象池的存在时间
    /// </summary>
    /// <param name="_mono"></param>
    /// <param name="aliveTime"></param>
    public PoolItemNodeMonoBehavior(MonoBehaviour _mono, PoolItem _poolItem)
    {
        this._mono = _mono;
        this.destoryStatus = false;
        this._poolItem = _poolItem;
        startActiveTime = Time.time;
    }


    ///<summary>  
    /// 激活对象，将对象显示  
    /// </summary>  
    public MonoBehaviour Active()
    {
        this._mono.enabled = true;
        this.destoryStatus = false;
        startActiveTime = Time.time;
        return this._mono;
    }

    ///<summary>  
    /// 销毁对象，不是真正的销毁  
    /// </summary>  
    public void Destroy()
    {//重置对象状态  
        this._mono.enabled = false;
        this.destoryStatus = true;
    }
}
