using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBloodBar : MonoBehaviour {
    public GameObject bloodBar;
    private Scrollbar bloodScrollbar;
    public float _maxHp=100f;
    public float _curHp=100f;

    // Use this for initialization
    void Start () {
        bloodBar = GameObject.Instantiate(RTSGameUnitBloodBarManager.ShareInstance.BloodBarPref);
        bloodBar.transform.SetParent(gameObject.transform);
        bloodScrollbar = bloodBar.GetComponent<Scrollbar>();
        if (transform.GetComponent<CapsuleCollider>())
        {
            bloodBar.transform.localPosition = new Vector3(0, transform.GetComponent<CapsuleCollider>().height + 1f, 0);
        }
        else if (transform.GetComponent<BoxCollider>())
        {
            bloodBar.transform.localPosition = new Vector3(0, transform.GetComponent<BoxCollider>().size.y + 1f, 0);
        }
        else
        {
            bloodBar.transform.localPosition = new Vector3(0, transform.GetComponent<MeshFilter>().mesh.bounds.size.y + 1f, 0);
            
        }
       
       
    }
	
	// Update is called once per frame
	void Update () {
        bloodBar.transform.rotation = Camera.main.transform.rotation;
        bloodScrollbar.size = SetHp(_curHp, _maxHp);
    }

    public float SetHp(float CurHp, float MaxHp)
    {
        _maxHp = MaxHp;
        _curHp = CurHp;
        return _curHp / _maxHp;
    }


}
