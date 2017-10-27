using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoinBehaviour : MonoBehaviour {

    private void Working()
    {
        GameObject Coin = GameObject.Instantiate(CoinCollect.Current.CoinPre, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, CoinCollect.Current.Own);
        CoinCollect.Current.Add(gameObject, Coin);
    }
    public void WorkStart(float Frequency)
    {
        //启动赚取金币动画
        InvokeRepeating("Working", 0, Frequency);
        //
    }
    public void WorkDone()
    {
        //取消赚取金币动画
        CancelInvoke();
    }
}
