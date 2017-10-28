using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour {
    public Dictionary<GameObject,List<GameObject>> coin = new Dictionary<GameObject, List<GameObject>>();
    public static CoinCollect Current;
    GameObject coinPre;
    Transform own;
    // private float money = 0f;
    private float coinValue = 60f;//每个金币的结算价格

    public int Money
    {
        // get
        // {
        //     return money;
        // }

        // set
        // {
        //     money = value;
        // }
        get;
        set;
    }

    public GameObject CoinPre
    {
        get
        {
            return coinPre;
        }
    }

    public Transform Own
    {
        get
        {
            return own;
        }

    }

    public float CoinValue
    {
        get
        {
            return coinValue;
        }

        set
        {
            coinValue = value;
        }
    }

    //public GameObject p;
    public void Start()
    {
        coinPre = Resources.Load<GameObject>(@"Prefab/GetRes/_Coin");
        own = GameObject.Find("ownCoin").transform;
    }
    public void Add(GameObject worker,GameObject momney)
    {
        if (coin.ContainsKey(worker)==false)
        {
            coin.Add(worker, new List<GameObject>());
        }
       
        coin[worker].Add(momney);
       
    }
    public CoinCollect()
    {
        Current=this;
    }

    public void Done(GameObject worker)
    {

        foreach (var item in coin[worker])
        {
            if (item.GetComponent<CoinTweenMove>()==null)
            {
                item.AddComponent<CoinTweenMove>().worker= worker;
            }
           
        }
    }

}
