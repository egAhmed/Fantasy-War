using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class RTSBuilding : RTSGameUnit
{

	public float schedule = 0;
	public bool isProducting = false;
    MeshCollider collider;
    public float BuildTime = 3;
    public float DestoryTime = 3;

    public bool allowRepair;

    private Material[] materials;
    public bool IsNeedRepair
    {
        get
        {
            return HP < HPMax;
        }
    }

	protected override void Awake ()
	{
		base.Awake ();

		//
		maxHP = 100;
		HP = maxHP;
	}

    protected override void Start()
    {
        base.Start();
        //
        MeshCollider collider=GetComponent<MeshCollider>();
        if (collider == null) {
            collider = gameObject.AddComponent<MeshCollider>();
        }
        //
        collider.convex = true;

		IconCameraPos = new Vector3 (2000, 5005, 1.167f);
        //
        // materials = GetMaterials();
        //
        // materials[0].SetFloat("_Progress", 0);
//        
        // BuildShaderEffect();
        //
    }


    /// <summary>
    ///获取单位的材质球数组
    /// </summary>
    /// <returns></returns>
    Material[] GetMaterials()
    {
        MeshRenderer mr = transform.GetComponent<MeshRenderer>();
        if (mr == null)
            return null;
        Material[] ms = mr.materials;
        return ms;
    }
    /// <summary>
    /// 调用协同，生成建造动画
    /// </summary>
    /// <returns></returns>
    void BuildShaderEffect()
    {
        if (materials.Length != 2)
        {
            Debug.Log("建筑的原始材质球个数不为2，无法使用建造专用shader,请检查");
            return;
        }
        else
        {
            StartCoroutine(buildShaderEffect());
        }
    }

    IEnumerator buildShaderEffect()
    {
        float count = 0;
        while (count < BuildTime)
        {
            count += Time.deltaTime;
            materials[0].SetFloat("_Progress", count / BuildTime);
            yield return null;
        }
    }

    /// <summary>
    /// 摧毁建筑时，调用的协程，生成摧毁动画
    /// </summary>
    public void DestoryShaderEffect()
    {
        if (materials.Length != 2)
        {
            Debug.Log("建筑的原始材质球个数不为2，无法使用建造专用shader,请检查");
            return;
        }
        else
        {
            //注意：可能有bug，由于这个函数在其他代码中调用，当此组件被消除后，不知协程还能否继续执行
            StartCoroutine(buildShaderEffect());
        }
    }

    IEnumerator destoryShaderEffect()
    {
        float count = 0;
        while (count < BuildTime)
        {
            count += Time.deltaTime;
            materials[1].SetFloat("_Progress", count / DestoryTime);
            
            yield return null;
        }
    }
    //
    protected override void actionBehaviourInit() {
        ///
        base.actionBehaviourInit();

		Interaction scdui = gameObject.AddComponent<ScheduleShowUI>();
		interactionList.Add(scdui);
      //
    }
}
