using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//用来显示小地图上的点,脚本挂到单位上即可生效
public class MapBlip : MonoBehaviour
{


    private GameObject blip;
    private GameObject mask;
    private float radius = 20;
    public GameObject Blip { get { return blip; } }
    private Color unitColor = Color.red;
    private Image BlipImage;
    private RectTransform BlipRect;

    void Awake()
    {
        blip = GameObject.Instantiate(Map.Current.BlipPrefab);
        mask = GameObject.Instantiate(Map.Current.MaskPrefab);
        blip.transform.SetParent(Map.Current.transform);
        mask.transform.SetParent(Map.Current.transform);
       

        BlipImage = blip.GetComponent<Image>();
        BlipImage.color = unitColor;
        BlipRect = blip.GetComponent<RectTransform>();
    }

    // void Start()
    // {


    //     blip = GameObject.Instantiate(Map.Current.BlipPrefab);
    //     mask = GameObject.Instantiate(Map.Current.MaskPrefab);
    //     blip.transform.SetParent(Map.Current.transform);
    //     mask.transform.SetParent(Map.Current.transform);
       

    //     BlipImage = blip.GetComponent<Image>();
    //     BlipImage.color = unitColor;
    //     BlipRect = blip.GetComponent<RectTransform>();
    // }

    //单位颜色
    public Color UnitColor
    {
        get
        {
            return unitColor;
        }
        set
        {
            unitColor = value;
            BlipImage.color = unitColor;
        }
    }

    //单位半径
    public float UnitRadius
    {
        get
        {
            return radius;
        }
        set
        {
            if (radius<=0)
            {
                radius = 1;
            }
            else
            {
                radius = value;
            }
          
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
        if (blip != null)
            BlipImage.color = unitColor;
    }

    void Update()
    {

        Vector2 miniMapOffset = new Vector2(Map.Current.transform.position.x, Map.Current.transform.position.y);//小地图原点相对于屏幕坐标原点的偏移值
        blip.transform.position = Map.Current.WorldPositionToMap(transform.position) + miniMapOffset;
        mask.transform.position = Map.Current.WorldPositionToMap(transform.position) + miniMapOffset;
        mask.transform.localScale = new Vector3(radius * Map.Current.ScaleFactor, radius * Map.Current.ScaleFactor, 0);
        BlipRect.sizeDelta = new Vector2(1*Map.Current.ScaleFactor, 1 * Map.Current.ScaleFactor);//分辨率自适应
    }

    void OnDestroy()
    {
        GameObject.Destroy(blip);
    }
}
