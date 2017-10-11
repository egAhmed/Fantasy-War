using UnityEngine;

/// <summary>
/// 为了工作，战争系统的雾需要3个组件:
/// - 战争系统的迷雾将创建你的场景的高度图并执行所有的更新。
/// - 战争图像对摄像机的影响，将显示战争的迷雾(this class)。
/// - 在世界上的一个或多个游戏物体上，有一个战争的迷雾。
/// </summary>

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Fog of War/Image Effect")]
public class FOWEffect : MonoBehaviour
{
	/// <summary>
	/// 着色器用于创建效果。应该参考“战争的图像效果/迷雾”。
	/// </summary>

	public Shader shader;

	/// <summary>
	/// 为未开发的像素着色。.
	/// </summary>
	
	public Color unexploredColor = new Color(0.05f, 0.05f, 0.05f, 1f);

	/// <summary>
	/// 用于探索(但不可见)像素的颜色。
	/// </summary>

	public Color exploredColor = new Color(0.2f, 0.2f, 0.2f, 1f);

	FOWSystem mFog;
	Camera mCam;
	Matrix4x4 mInverseMVP;
	Material mMat;

	/// <summary>
	/// 我们正在处理的相机需要深度。
	/// </summary>

	void OnEnable ()
	{
		mCam = GetComponent<Camera>();
		mCam.depthTextureMode = DepthTextureMode.Depth;
		if (shader == null) shader = Shader.Find("Image Effects/Fog of War");
	}

	/// <summary>
	/// 残疾时销毁材料。
	/// </summary>

	void OnDisable () { if (mMat) DestroyImmediate(mMat); }

	/// <summary>
	/// 如果着色器不支持，自动禁用效果。
	/// </summary>

	void Start ()
	{
		if (!SystemInfo.supportsImageEffects || !shader || !shader.isSupported)
		{
			enabled = false;
		}
	}

	// 用相机呼叫来应用图像效果
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (mFog == null)
		{
			mFog = FOWSystem.instance;
			if (mFog == null) mFog = FindObjectOfType(typeof(FOWSystem)) as FOWSystem;
		}

		if (mFog == null || !mFog.enabled)
		{
			enabled = false;
			return;
		}

		// 计算逆模型-投影矩阵，将屏幕坐标转换为世界坐标
		mInverseMVP = (mCam.projectionMatrix * mCam.worldToCameraMatrix).inverse;

		float invScale = 1f / mFog.worldSize;
		Transform t = mFog.transform;
		float x = t.position.x - mFog.worldSize * 0.5f;
		float z = t.position.z - mFog.worldSize * 0.5f;

		if (mMat == null)
		{
			mMat = new Material(shader);
			mMat.hideFlags = HideFlags.HideAndDontSave;
		}

		Vector4 camPos = mCam.transform.position;

		// 这是反锯齿的窗口翻转的深度UV坐标
		// 尽管有官方文件，以下方法根本不起作用:
		// http://docs.unity3d.com/Documentation/Components/SL-PlatformDifferences.html

		if (QualitySettings.antiAliasing > 0)
		{
			RuntimePlatform pl = Application.platform;

			if (pl == RuntimePlatform.WindowsEditor ||
				pl == RuntimePlatform.WindowsPlayer ||
				pl == RuntimePlatform.WindowsWebPlayer)
			{
				camPos.w = 1f;
			}
		}

		Vector4 p = new Vector4(-x * invScale, -z * invScale, invScale, mFog.blendFactor);
		mMat.SetColor("_Unexplored", unexploredColor);
		mMat.SetColor("_Explored", exploredColor);
		mMat.SetVector("_CamPos", camPos);
		mMat.SetVector("_Params", p);
		mMat.SetMatrix("_InverseMVP", mInverseMVP);
		mMat.SetTexture("_FogTex0", mFog.texture0);
		mMat.SetTexture("_FogTex1", mFog.texture1);

		Graphics.Blit(source, destination, mMat);
	}
}