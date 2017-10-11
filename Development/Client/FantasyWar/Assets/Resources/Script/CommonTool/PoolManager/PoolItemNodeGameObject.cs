using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemNodeGameObject : PoolItemNode
{
    ///<summary>  
    /// 对象  
    /// </summary>  
    public GameObject gameObject;

    public PoolItemNodeGameObject(GameObject _gameObject)
    {
        this.gameObject = _gameObject;
        this.destoryStatus = false;
    }

    ///<summary>  
    /// 激活对象，将对象显示  
    /// </summary>  
    public GameObject Active()
    {
        this.gameObject.SetActive(true);
        this.destoryStatus = false;
        aliveTime = 0;
        return this.gameObject;

    }

    ///<summary>  
    /// 销毁对象，不是真正的销毁  
    /// </summary>  
    public void Destroy()
    {//重置对象状态  
        this.gameObject.SetActive(false);
        this.destoryStatus = true;
        this.aliveTime = Time.time;
    }
}
