using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapBlip : MonoBehaviour {

	private GameObject blip;

	public GameObject Blip { get { return blip; } }
    private Color unitColor = Color.red;
    private Image BlipImage;
    private float scaleFactor;
    // Use this for initialization


    void Start() {
      
        blip = GameObject.Instantiate (Map.Current.BlipPrefab);      
        blip.transform.SetParent(Map.Current.transform);

        BlipImage = blip.GetComponent<Image>();
        BlipImage.color = unitColor;
       
      
    }
  
	public Color UnitColor
    {
        get
        {
            return unitColor;
        }
        set
        {
            unitColor = value;
        }
    }

    /// <summary>
    /// blip是否可见
    /// </summary>
    /// <param name="isVisible"></param>
    public void SetVisible(bool isVisible)
    {
        if (isVisible)
        {
            Color temp = UnitColor;
            temp.a = 1;
            UnitColor = temp;
        }
        else
        {
            Color temp = UnitColor;
            temp.a = 0;
            UnitColor = temp;
        }
        if (blip!=null)
        BlipImage.color = unitColor;
    }
    // Update is called once per frame
    void Update () {
       
        Vector2 miniMapOffset = new Vector2(Map.Current.transform.position.x, Map.Current.transform.position.y);//小地图原点相对于屏幕坐标原点的偏移值

        blip.transform.position = Map.Current.WorldPositionToMap(transform.position) + miniMapOffset;
        

    }

	void OnDestroy()
	{
		GameObject.Destroy (blip);
	}
}
