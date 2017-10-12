using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Map : MonoBehaviour
{

    public RectTransform ViewPort;//小地图上的摄像机视野范围
    //public Transform Corner1, Corner2;//地形左下角和地形右上角的标记点
    public GameObject BlipPrefab;//小地图上的单位预制体
    public GameObject MaskPrefab;
    public static Map Current;
    public Camera MaskCarmera;
    private Button MapButton;
    public RectTransform Border;
    public RectTransform MaskMap;
    private Vector2 terrainSize;//地形大小
    private Vector3 terrainPos;
    private RectTransform mapRect;


    private TerrainData terrainData;
    private float scaleFactor = 1;

    public Map()
    {
        Current = this;

    }
    public float ScaleFactor
    {
        get
        {
            return scaleFactor;
        }
    }
    // Use this for initialization
    void Start()
    {
        #region 半自动计算地形大小(废弃)
        //计算地形大小, Corner1放到左下角, Corner2放到右上角
        //terrainSize = new Vector2 (
        //Corner2.position.x - Corner1.position.x,
        //Corner2.position.z - Corner1.position.z);
        #endregion

        //获取地形大小
        terrainData = GameObject.Find("Terrain").GetComponent<Terrain>().terrainData;
        terrainSize = new Vector2(terrainData.size.x, terrainData.size.z);
        terrainPos = GameObject.Find("Terrain").transform.position;

        MapButton = GetComponent<Button>();
        mapRect = GetComponent<RectTransform>();


        MapButton.onClick.AddListener(MoveTo);

    }


    /// <summary>
    /// 地形映射到小地图
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Vector2 WorldPositionToMap(Vector3 point)
    {

        Vector2 mapPos = new Vector2(
            (point.x - terrainPos.x) / terrainSize.x * mapRect.rect.width,
            (point.z - terrainPos.z) / terrainSize.y * mapRect.rect.height);
        return mapPos;
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
            0, point.y * terrainSize.y / mapRect.rect.height);
        return worldPos;
    }
    // Update is called once per frame
    void Update()
    {

        Vector2 miniMapOffset = new Vector2(Map.Current.transform.position.x, Map.Current.transform.position.y);//小地图原点相对于屏幕坐标原点的偏移值,用于自适应
        ViewPort.position = WorldPositionToMap(Camera.main.transform.position) + miniMapOffset;//摄像机在小地图上的视线范围

        //分辨率自适应
        scaleFactor = Screen.width / 1024f;
        mapRect.sizeDelta = new Vector2(256 * scaleFactor, 256 * scaleFactor);
        Border.sizeDelta = mapRect.sizeDelta;
        Border.position = mapRect.position;
        ViewPort.sizeDelta = new Vector2(80 * scaleFactor, 50 * scaleFactor);
        MaskMap.sizeDelta = mapRect.sizeDelta;
        MaskCarmera.orthographicSize = 128 * scaleFactor;
        MaskCarmera.transform.position = new Vector3(128 * scaleFactor, 128 * scaleFactor,0);
        // Debug.Log(scaleFactor);
    }
    /// <summary>
    /// 移动到摄像机到点击位置
    /// </summary>
    public void MoveTo()
    {

        //小地图坐标系转世界坐标系
        Vector3 map2world = new Vector3(MapToWorldPosition((Input.mousePosition - this.transform.position)).x, Camera.main.transform.position.y, MapToWorldPosition((Input.mousePosition - this.transform.position)).z);
        Camera.main.transform.position= map2world + terrainPos;
    }

    //private void OnGUI()
    //{
    //    GUIStyle bb = new GUIStyle();
    //    bb.normal.textColor = new Color(1, 0, 0);
    //    bb.fontSize = 30;
    //    GUILayout.Label("鼠标位置:" + Input.mousePosition, bb);
    //    GUILayout.Label("地图转世界:" + MapToWorldPosition((Input.mousePosition - this.transform.position)), bb);
    //    GUILayout.Label("小地图坐标原点:" + this.transform.position, bb);
    //    GUILayout.Label("鼠标位置与小地图原点的相对位置:" + (Input.mousePosition - this.transform.position), bb);
    //    GUILayout.Label("摄像机位置:" + Camera.main.transform.position, bb);
    //}
}
