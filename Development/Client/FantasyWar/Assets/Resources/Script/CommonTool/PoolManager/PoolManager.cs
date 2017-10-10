using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {  

	/// <summary>  
	/// 默认超时时间  
	/// </summary>  
	public const int Alive_Time = 1 * 60;

    /// <summary>
    /// 默认对象池上限
    /// </summary>
    public const int PoolItemMaxNum = 100;
	
    /// <summary>  
	/// 对象池  
	/// </summary>  
	public static Dictionary<string, PoolItem> itemList;  

	/// <summary>  
	/// 添加一个对象组,使用默认超时时间,和默认对象池上限  
	/// </summary>  
	public static void PushData(string _name){  
		if (itemList == null)  
			itemList = new Dictionary<string, PoolItem> ();  
		if (!itemList.ContainsKey (_name))  
			itemList.Add (_name, new PoolItem (_name));  
	}

    /// <summary>
    /// 添加一个对象组，使用自定义超时时间,和默认对象池上限  
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="Alive_Time"></param>
    public static void PushData(string _name,int Alive_Time)
    {
        if (itemList == null)
            itemList = new Dictionary<string, PoolItem>();
        if (!itemList.ContainsKey(_name))
            itemList.Add(_name, new PoolItem(_name,Alive_Time));
    }

    /// <summary>  
    /// 添加一个对象组,使用默认超时时间,和自定义对象池上限  
    /// </summary>  
    public static void PushData(int PoolItemMaxNum, string _name)
    {
        if (itemList == null)
            itemList = new Dictionary<string, PoolItem>();
        if (!itemList.ContainsKey(_name))
            itemList.Add(_name, new PoolItem(PoolItemMaxNum,_name));
    }

    /// <summary>  
    /// 添加一个对象组,使用自定义超时时间,自定义对象池上限  
    /// </summary>  
    public static void PushData(int PoolItemMaxNum, string _name,int Alive_Time)
    {
        if (itemList == null)
            itemList = new Dictionary<string, PoolItem>();
        if (!itemList.ContainsKey(_name))
            itemList.Add(_name, new PoolItem(PoolItemMaxNum,_name,Alive_Time));
    }

    /// <summary>  
    /// 添加单个对象（首先寻找对象组->添加单个对象）  
    /// </summary>  
    public static void PushObject(string _name, GameObject _gameObject){  
		if (itemList == null || !itemList.ContainsKey (_name))  
			PushData (_name);//添加对象组  
		//添加对象  
		((PoolItemGameObject)itemList [_name]).PushObject (_gameObject);  
	}

    /// <summary>  
    /// 添加单个对象（首先寻找对象组->添加单个对象）  
    /// </summary>  
    public static void PushObject(string _name, MonoBehaviour _mono)
    {
        if (itemList == null || !itemList.ContainsKey(_name))
            PushData(_name);//添加对象组  
                            //添加对象  
        ((PoolItemMonoBehavior)itemList[_name]).PushObject(_mono);
    }

    /// <summary>  
    /// 移除单个对象，真正的销毁!!  
    /// </summary>  
    public static void RemoveObject(string _name, GameObject _gameObject){  
		if (itemList == null || !itemList.ContainsKey (_name))  
			return;
        ((PoolItemGameObject)itemList[_name]).RemoveObject (_gameObject);  
	}

    /// <summary>  
    /// 移除单个对象，真正的销毁!!  
    /// </summary>  
    public static void RemoveObject(string _name, MonoBehaviour _mono)
    {
        if (itemList == null || !itemList.ContainsKey(_name))
            return;
        ((PoolItemMonoBehavior)itemList[_name]).RemoveObject(_mono);
    }

    /// <summary>  
    /// 获取缓存中的对象  
    /// </summary>  
    public static object GetObject(string _name){  
		if (itemList == null || !itemList.ContainsKey (_name)) {  
			return null;  
		}  
        if(itemList[_name] is PoolItemGameObject)
		return ((PoolItemGameObject)itemList[_name]).GetObject ();
        if(itemList[_name] is PoolItemMonoBehavior)
        return ((PoolItemMonoBehavior)itemList[_name]).GetObject();
        return null;
	}



    /// <summary>  
    /// 销毁对象，没有真正的销毁!!  
    /// </summary>  
    public static void DestroyActiveObject(string _name, GameObject _gameObject){  
		if (itemList == null || !itemList.ContainsKey (_name)) {  
			return;  
		}
        ((PoolItemGameObject)itemList[_name]).DestoryObject (_gameObject);  
	}

    /// <summary>  
    /// 销毁对象，没有真正的销毁!!  
    /// </summary>  
    public static void DestroyActiveObject(string _name, MonoBehaviour _mono)
    {
        if (itemList == null || !itemList.ContainsKey(_name))
        {
            return;
        }
        ((PoolItemMonoBehavior)itemList[_name]).DestoryObject(_mono);
    }

    /// <summary>  
    /// 处理超时对象  
    /// </summary>  
    public static void BeyondTimeObject(){  
		if (itemList == null) {  
			return;  
		}  
		foreach (PoolItem poolI in itemList.Values) {
            if (poolI is PoolItemGameObject)
            {
                ((PoolItemGameObject)poolI).BeyondObject();
                return;
            }
            if (poolI is PoolItemMonoBehavior)
            {
                ((PoolItemMonoBehavior)poolI).BeyondObject();
                return;
            }
              
		}  
	}

    /// <summary>  
    /// 销毁全部对象，真正的销毁!!  
    /// </summary>  
    public static void Destroy(){  
		if (itemList == null) {  
			return;  
		}  
		foreach (PoolItem poolI in itemList.Values) {
            if (poolI is PoolItemGameObject)
            {
                ((PoolItemGameObject)poolI).Destory();
                return;
            }
            if (poolI is PoolItemMonoBehavior)
            {
                ((PoolItemMonoBehavior)poolI).Destory();
                return;
            }
        }  
		itemList = null;  
	}  



}  
