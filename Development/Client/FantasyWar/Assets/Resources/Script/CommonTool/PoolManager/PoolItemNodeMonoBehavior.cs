using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemNodeMonoBehavior : PoolItemNode {
    ///<summary>  
    /// 对象  
    /// </summary>  
    public MonoBehaviour _mono;
    public PoolItemNodeMonoBehavior(MonoBehaviour _mono) 
    {
        this._mono = _mono;
        this.destoryStatus = false;
    }

    ///<summary>  
    /// 激活对象，将对象显示  
    /// </summary>  
    public MonoBehaviour Active()
    {
        this._mono.enabled = true;
        this.destoryStatus = false;
        aliveTime = 0;
        return this._mono;

    }

    ///<summary>  
    /// 销毁对象，不是真正的销毁  
    /// </summary>  
    public void Destroy()
    {//重置对象状态  
        this._mono.enabled = false;
        this.destoryStatus = true;
        this.aliveTime = Time.time;
    }
}
