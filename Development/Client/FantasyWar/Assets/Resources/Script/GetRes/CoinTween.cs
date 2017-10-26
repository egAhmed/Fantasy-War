using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CoinTween : MonoBehaviour {
    private float m;
    Tween t1, t2;
    public List<GameObject> coin = new List<GameObject>();
    // Use this for initialization
    void Start () {
        m = transform.position.y;
        DOTween.Init();
        t1 = transform.DOMoveY(m + 200f, 3);
        t2 = transform.GetComponent<Image>().DOFade(0, 3);
        t1.Play();
        t2.Play();
    }
   

}
