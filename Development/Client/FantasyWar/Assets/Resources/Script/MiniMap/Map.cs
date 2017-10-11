using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Map : MonoBehaviour {

	public RectTransform ViewPort;//小地图上的摄像机视野范围
	public Transform Corner1, Corner2;//地形左下角和地形右上角的标记点
	public GameObject BlipPrefab;//小地图上的单位预制体
	public static Map Current;
    public Button MapButton;
    private Vector2 terrainSize;//地形大小
	private RectTransform mapRect;
    public GameObject MainCramer;
    //public float scaleFactor = 1f;
    public Map()
	{
		Current = this;

	}
    private void Awake()
    {
      
        
    }
    // Use this for initialization
    void Start () {
            //计算地形大小
		    terrainSize = new Vector2 (
			Corner2.position.x - Corner1.position.x,
			Corner2.position.z - Corner1.position.z);

        MapButton= GetComponent<Button>();
        mapRect = GetComponent<RectTransform> ();
       
        MapButton.onClick.AddListener(MoveTo);
     
    }

  
    /// <summary>
    /// 地形映射到小地图
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
	public Vector2 WorldPositionToMap(Vector3 point)
	{
        var pos = point - Corner1.position;
        Vector2 mapPos = new Vector2 (
			point.x / terrainSize.x * mapRect.rect.width ,
			point.z / terrainSize.y * mapRect.rect.height );
		return mapPos ;
	}
    /// <summary>
    /// 按比例还原成世界
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Vector3 MapToWorldPosition(Vector3 point)
    {
       
        var worldPos = new Vector3(
            point.x * terrainSize.x / mapRect.rect.width,
            0,point.y * terrainSize.y / mapRect.rect.height);
        return worldPos;
    }
    // Update is called once per frame
    void Update () {
      
       
        Vector2 miniMapOffset=new Vector2(Map.Current.transform.position.x, Map.Current.transform.position.y);//小地图原点相对于屏幕坐标原点的偏移值,用于自适应

        ViewPort.position = (WorldPositionToMap(MainCramer.transform.position) + miniMapOffset);//摄像机在小地图上的视线范围

       

    }
    /// <summary>
    /// 移动到摄像机到点击位置
    /// </summary>
    public void MoveTo()
    {

        //小地图坐标系转世界坐标系
        Vector3 map2world = new Vector3(MapToWorldPosition((Input.mousePosition - this.transform.position)).x , Camera.main.transform.position.y, MapToWorldPosition((Input.mousePosition - this.transform.position)).z);
        MainCramer.transform.position = map2world ;
    }

    //private void OnGUI()
    //{
    //    GUIStyle bb = new GUIStyle();
    //    bb.normal.textColor = new Color(1, 0, 0);   
    //    bb.fontSize = 30; 
    //    GUILayout.Label("鼠标位置:"+Input.mousePosition, bb);
    //    GUILayout.Label("地图转世界:" + MapToWorldPosition((Input.mousePosition - this.transform.position)), bb);
    //    GUILayout.Label("小地图坐标原点:" + this.transform.position, bb);
    //    GUILayout.Label("鼠标位置与小地图原点的相对位置:" + (Input.mousePosition - this.transform.position), bb);
    //    GUILayout.Label("摄像机位置:" + Camera.main.transform.position, bb);
    //}
}
