using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBloodBar : MonoBehaviour {
    public GameObject bloodBar;
    private Scrollbar bloodScrollbar;
    public float _maxHp=100f;
    public float _curHp=100f;
    public Color _backBloodColor = Color.black;
    public Color _fillBloodColor= Color.green;

    private void Awake()
    {
        bloodBar = GameObject.Instantiate(RTSGameUnitBloodBarManager.ShareInstance.BloodBarPref);
        bloodBar.transform.SetParent(gameObject.transform);
        bloodScrollbar = bloodBar.GetComponent<Scrollbar>();
    }
    // Use this for initialization
    void Start () {

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
           bloodBar.transform.localPosition = new Vector3(0, 5f, 0);           
        }
       
       
    }
	
	// Update is called once per frame
	void LateUpdate () {
        bloodBar.transform.rotation = Camera.main.transform.rotation;
        bloodScrollbar.size = SetHp(_curHp, _maxHp);
    }

    public float SetHp(float CurHp, float MaxHp)
    {
        _maxHp = MaxHp;
        _curHp = CurHp;
        return _curHp / _maxHp;
    }
    public void SetHide(bool state)
    {
        if (state == true)
        {
            _backBloodColor.a = 0;
            _fillBloodColor.a = 0;
        }
        else
        {
            _backBloodColor.a = 1;
            _fillBloodColor.a = 1;
        }
        SetColor(_fillBloodColor, _backBloodColor);
    }
    public void SetColor(Color fillBloodColor)
    {
        _fillBloodColor = fillBloodColor;
        bloodScrollbar.transform.GetChild(0).GetComponent<Image>().color = _fillBloodColor;
    }
    public void SetColor(Color fillBloodColor, Color backBloodColor)
    {
        _fillBloodColor = fillBloodColor;
        bloodScrollbar.transform.GetChild(0).GetComponent<Image>().color = _fillBloodColor;
        _backBloodColor = backBloodColor;
        bloodScrollbar.GetComponent<Image>().color = _backBloodColor;
    }

}
