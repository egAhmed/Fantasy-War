using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//金币/资源收集完成的动画
public class CoinTweenMove : MonoBehaviour {
    GameObject GameResource_Coin, CoinCount;
    public GameObject worker;
    // Use this for initialization
    private void Awake()
    {       
        GetComponent<CoinTween>().enabled = false;
    }
    void Start () {
        transform.position = Camera.main.WorldToScreenPoint(worker.transform.position);
        GameResource_Coin = GameObject.Find("GameResource_Coin");
        CoinCount = GameObject.Find("CoinCount");
        Sequence mySequence = DOTween.Sequence();
        transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Tween t = transform.DOMove(GameResource_Coin.transform.position, 1);
        Tween t2 = transform.DORotate(new Vector3(0,270,0),0.1f).SetLoops(10).OnComplete(OnComplete);
        t.SetEase(Ease.InExpo);
        t2.SetEase(Ease.Linear);
        mySequence.Append(t);
        mySequence.Join(t2);
      
    }

    private void OnComplete()
    {
        CoinCollect.Current.Money += CoinCollect.Current.CoinValue;
        CoinCount.GetComponent<Text>().text = CoinCollect.Current.Money + "";
        CoinCollect.Current.coin[worker].Remove(gameObject);
        Destroy(gameObject);
        
    }

}
