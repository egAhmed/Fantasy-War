using UnityEngine;
using System.Threading;

/// <summary>
/// 战争系统的雾需要3个组件才能工作:
/// - War系统的雾，它将创建您的场景的高度图并执行所有的更新(这个类).
/// - 战争图像对摄像机的影响，将显示战争的迷雾.
/// - 在世界上的一个或多个游戏物体上，有一个战争的迷雾.
/// </summary>

[AddComponentMenu("Fog of War/System")]
public class FOWSystem : MonoBehaviour
{
	public enum LOSChecks
	{
		None,
		OnlyOnce,
		EveryUpdate,
	}

	public class Revealer
	{
		public bool isActive = false;
		public LOSChecks los = LOSChecks.EveryUpdate ;
		public Vector3 pos = Vector3.zero;
		public float inner = 0f;
		public float outer = 0f;
		public bool[] cachedBuffer;
		public int cachedSize = 0;
		public int cachedX = 0;
		public int cachedY = 0;
	}

	public enum State
	{
		Blending,
		NeedUpdate,
		UpdateTexture0,
		UpdateTexture1,
	}

	static public FOWSystem instance;

	// 用于可见性检查的高度图。使用整数而不是浮点数作为整数检查的速度要快得多.
	protected int[,] mHeights;
	protected Transform mTrans;
	protected Vector3 mOrigin = Vector3.zero;
	protected Vector3 mSize = Vector3.one;

	// 这条线目前正在使用的护封
	static BetterList<Revealer> mRevealers = new BetterList<Revealer>();

	// 自上次更新后添加的保护程序
	static BetterList<Revealer> mAdded = new BetterList<Revealer>();

	// 自上次更新后被删除的保护程序
	static BetterList<Revealer> mRemoved = new BetterList<Revealer>();

	// 颜色缓冲区——在工作线程上编写.
	protected Color32[] mBuffer0;
	protected Color32[] mBuffer1;
	protected Color32[] mBuffer2;

	// 两种纹理——我们将在阴影中混合它们
	protected Texture2D mTexture0;
	protected Texture2D mTexture1;

	// 是否已经准备好将一些颜色缓冲区上载到VRAM中
	protected float mBlendFactor = 0f;
	protected float mNextUpdate = 0f;
	protected int mScreenHeight = 0;
	protected State mState = State.Blending;

	Thread mThread;

	/// <summary>
	/// 你的世界大小单位。例如，如果你有256x256的地形，那么就在256的时候保留这个.
	/// </summary>

	public int worldSize = 500;

	/// <summary>
	/// 战争纹理的迷雾大小。更高的分辨率将导致更精确的战争迷雾，以牺牲性能为代价.
	/// </summary>

	public int textureSize = 200;

	/// <summary>
	/// 是否经常执行可见性检查.
	/// </summary>

	public float updateFrequency = 0.1f;

	/// <summary>
	/// 织体从一个到另一个要花多长时间
	/// </summary>

	public float textureBlendTime = 0.5f;

	/// <summary>
	/// 将执行多少模糊迭代。更多的迭代会产生更平滑的边
	/// 模糊在一个单独的线程上发生，不影响性能。
	/// </summary>

	public int blurIterations = 2;

	/// <summary>
	/// 你的世界最低和最高的高度是多少?X下方的探条者不会透露任何信息，
	/// 而在Y轴上的发现将揭示周围的一切.
	/// </summary>

	public Vector2 heightRange = new Vector2(0f, 10f);

	/// <summary>
	/// 用于raycasting的掩模，以确定是否存在阻塞。
	/// </summary>

	public LayerMask raycastMask = -1;

	/// <summary>
	/// 球体的半径如果使用球播。如果0，基于线的raycasting将被使用。
	/// </summary>

	public float raycastRadius = 0f;

	/// <summary>
	///在执行视距检查时允许一些高度方差。.
	/// </summary>

	public float margin = 0.4f;

	/// <summary>
	/// 如果启用调试，计算战争迷雾所需的时间将显示在日志窗口中
	/// </summary>

	public bool debug = false;

	/// <summary>
	/// 我们混合的雾状结构。
	/// </summary>

	public Texture2D texture0 { get { return mTexture0; } }

	/// <summary>
	/// 我们混合在一起的雾状结构。
	/// </summary>

	public Texture2D texture1 { get { return mTexture1; } }

	/// <summary>
	/// 用于混合两种材质的因素。
	/// </summary>

	public float blendFactor { get { return mBlendFactor; } }

	/// <summary>
	/// 创建一个新的喷雾探测器.
	/// </summary>

	static public Revealer CreateRevealer ()
	{
		Revealer rev = new Revealer();
		rev.isActive = false;
		lock (mAdded) mAdded.Add(rev);
		return rev;
	}

	/// <summary>
	/// 删除指定的启示者。
	/// </summary>

	static public void DeleteRevealer (Revealer rev) { lock (mRemoved) mRemoved.Add(rev); }

	/// <summary>
	/// 设置实例。
	/// </summary>

	void Awake () { instance = this; }

	/// <summary>
	/// 生成网格高度。
	/// </summary>

	void Start ()
	{
		mTrans = transform;
		mHeights = new int[textureSize, textureSize];
		mSize = new Vector3(worldSize, heightRange.y - heightRange.x, worldSize);
		//
		mOrigin = mTrans.position;
		mOrigin.x -= worldSize * 0.5f;
		mOrigin.z -= worldSize * 0.5f;
		//
		int size = textureSize * textureSize;
		mBuffer0 = new Color32[size];
		mBuffer1 = new Color32[size];
		mBuffer2 = new Color32[size];

		// 创建网格高度
		CreateGrid();

		// 更新战争的能见度，让它马上更新
		UpdateBuffer();
		UpdateTexture();
		mNextUpdate = Time.time + updateFrequency;

		// 添加一个线程更新函数——所有可见性检查将在一个单独的线程上执行
		mThread = new Thread(ThreadUpdate);
		mThread.Start();
	}

	/// <summary>
	///确保线程被终止。
	/// </summary>

	void OnDestroy ()
	{
		if (mThread != null)
		{
			mThread.Abort();
			while (mThread.IsAlive) Thread.Sleep(1);
			mThread = null;
		}
	}

	/// <summary>
	/// 是时候更新能见度了吗?如果是这样，切换一下开关。
	/// </summary>

	void Update ()
	{
		if (textureBlendTime > 0f)
		{
			mBlendFactor = Mathf.Clamp01(mBlendFactor + Time.deltaTime / textureBlendTime);
		}
		else mBlendFactor = 1f;

		if (mState == State.Blending)
		{
			float time = Time.time;

			if (mNextUpdate < time)
			{
				mNextUpdate = time + updateFrequency;
				mState = State.NeedUpdate;
			}
		}
		else if (mState != State.NeedUpdate)
		{
			UpdateTexture();
		}
	}

	float mElapsed = 0f;

	/// <summary>
	/// 如果是时候更新了，现在就去做。
	/// </summary>

	void ThreadUpdate ()
	{
		System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

		for (; ; )
		{
			if (mState == State.NeedUpdate)
			{
				sw.Reset();
				sw.Start();
				UpdateBuffer();
				sw.Stop();
				if (debug) Debug.Log(sw.ElapsedMilliseconds);
				mElapsed = 0.001f * (float)sw.ElapsedMilliseconds;
				mState = State.UpdateTexture0;
			}
			Thread.Sleep(1);
		}
	}

	/// <summary>
	///展示战争迷雾所覆盖的地区。
	/// </summary>

	void OnDrawGizmosSelected ()
	{
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(new Vector3(0f, (heightRange.x + heightRange.y) * 0.5f, 0f),
			new Vector3(worldSize, heightRange.y - heightRange.x, worldSize));
	}

	/// <summary>
	/// 确定指定的点是否可见，或不使用视距检查。
	/// </summary>

	bool IsVisible (int sx, int sy, int fx, int fy, float outer, int sightHeight, int variance)
	{
		int dx = Mathf.Abs(fx - sx);
		int dy = Mathf.Abs(fy - sy);
		int ax = sx < fx ? 1 : -1;
		int ay = sy < fy ? 1 : -1;
		int dir = dx - dy;

		float sh = sightHeight;
		float fh = mHeights[fx, fy];

		float invDist = 1f / outer;
		float lerpFactor = 0f;

		for (; ; )
		{
			if (sx == fx && sy == fy) return true;

			int xd = fx - sx;
			int yd = fy - sy;
			
			// 如果采样高度高于预期，则必须模糊点
			lerpFactor = invDist * Mathf.Sqrt(xd * xd + yd * yd);
			if (mHeights[sx, sy] > Mathf.Lerp(fh, sh, lerpFactor) + variance) return false;
			
			int dir2 = dir << 1;

			if (dir2 > -dy)
			{
				dir -= dy;
				sx += ax;
			}

			if (dir2 < dx)
			{
				dir += dx;
				sy += ay;
			}
		}
	}

	/// <summary>
	/// 将指定的高度转换为内部整数表示。整数检查比浮动快得多。
	/// </summary>

	public int WorldToGridHeight (float height)
	{
		int val = Mathf.RoundToInt(height / mSize.y * 255f);
		return Mathf.Clamp(val, 0, 255);
	}

	/// <summary>
	/// 使用默认技术(raycasting)创建heightmap网格。
	/// </summary>

	protected virtual void CreateGrid ()
	{
		Vector3 pos = mOrigin;
		pos.y += mSize.y;
		float texToWorld = (float)worldSize / textureSize;
		bool useSphereCast = raycastRadius > 0f;

		for (int z = 0; z < textureSize; ++z)
		{
			pos.z = mOrigin.z + z * texToWorld;

			for (int x = 0; x < textureSize; ++x)
			{
				pos.x = mOrigin.x + x * texToWorld;

				RaycastHit hit;

				if (useSphereCast)
				{
					if (Physics.SphereCast(new Ray(pos, Vector3.down), raycastRadius, out hit, mSize.y, raycastMask))
					{
						mHeights[x, z] = WorldToGridHeight(pos.y - hit.distance - raycastRadius);
						continue;
					}
				}
				else if (Physics.Raycast(new Ray(pos, Vector3.down), out hit, mSize.y, raycastMask))
				{
					mHeights[x, z] = WorldToGridHeight(pos.y - hit.distance);
					continue;
				}
				mHeights[x, z] = 0;
			}
		}
	}

	/// <summary>
	/// 更新战争能见度的迷雾
	/// </summary>

	void UpdateBuffer ()
	{
		// 添加计划添加的所有项目
		if (mAdded.size > 0)
		{
			lock (mAdded)
			{
				while (mAdded.size > 0)
				{
					int index = mAdded.size - 1;
					mRevealers.Add(mAdded.buffer[index]);
					mAdded.RemoveAt(index);
				}
			}
		}

		// 移除所有移除的项目
		if (mRemoved.size > 0)
		{
			lock (mRemoved)
			{
				while (mRemoved.size > 0)
				{
					int index = mRemoved.size - 1;
					mRevealers.Remove(mRemoved.buffer[index]);
					mRemoved.RemoveAt(index);
				}
			}
		}

		// 使用纹理混合时间，因此估计这次更新完成的时间
		// 这样做有助于避免混合结果导致的混合结果的可见变化。
		float factor = (textureBlendTime > 0f) ? Mathf.Clamp01(mBlendFactor + mElapsed / textureBlendTime) : 1f;

		// 清除缓冲区的红色通道(用于当前可见性的通道——它将在后面更新)
		for (int i = 0, imax = mBuffer0.Length; i < imax; ++i)
		{
			mBuffer0[i] = Color32.Lerp(mBuffer0[i], mBuffer1[i], factor);
			mBuffer1[i].r = 0;
		}

		// 用于从世界坐标转换到纹理坐标
		float worldToTex = (float)textureSize / worldSize;

		// 每次更新可见度缓冲，一个探出器
		for (int i = 0; i < mRevealers.size; ++i)
		{
			Revealer rev = mRevealers[i];
			if (!rev.isActive) continue;
			
			if (rev.los == LOSChecks.None)
			{
				RevealUsingRadius(rev, worldToTex);
			}
			else if (rev.los == LOSChecks.OnlyOnce)
			{
				RevealUsingCache(rev, worldToTex);
			}
			else
			{
				RevealUsingLOS(rev, worldToTex);
			}
		}

		// 模糊最终的可见数据
		for (int i = 0; i < blurIterations; ++i) BlurVisibility();

		// Reveal the map based on what's currently visible
		RevealMap();
	}

	/// <summary>
	/// 最快的可见性更新——基于辐射的，没有视线检查。
	/// </summary>

	void RevealUsingRadius (Revealer r, float worldToTex)
	{
		// 相对于战争迷雾的位置
		Vector3 pos = r.pos - mOrigin;

		// 我们要处理的坐标
		int xmin = Mathf.RoundToInt((pos.x - r.outer) * worldToTex);
		int ymin = Mathf.RoundToInt((pos.z - r.outer) * worldToTex);
		int xmax = Mathf.RoundToInt((pos.x + r.outer) * worldToTex);
		int ymax = Mathf.RoundToInt((pos.z + r.outer) * worldToTex);

		int cx = Mathf.RoundToInt(pos.x * worldToTex);
		int cy = Mathf.RoundToInt(pos.z * worldToTex);

		cx = Mathf.Clamp(cx, 0, textureSize - 1);
		cy = Mathf.Clamp(cy, 0, textureSize - 1);

		int radius = Mathf.RoundToInt(r.outer * r.outer * worldToTex * worldToTex);

		for (int y = ymin; y < ymax; ++y)
		{
			if (y > -1 && y < textureSize)
			{
				int yw = y * textureSize;

				for (int x = xmin; x < xmax; ++x)
				{
					if (x > -1 && x < textureSize)
					{
						int xd = x - cx;
						int yd = y - cy;
						int dist = xd * xd + yd * yd;

						// 揭示这个像素
						if (dist < radius) mBuffer1[x + yw].r = 255;
					}
				}
			}
		}
	}

	/// <summary>
	/// 在探宝器周围显示地图，执行视距检查
	/// </summary>

	void RevealUsingLOS (Revealer r, float worldToTex)
	{
		// 相对于战争迷雾的位置
		Vector3 pos = r.pos - mOrigin;

		// 我们要处理的坐标
		int xmin = Mathf.RoundToInt((pos.x - r.outer) * worldToTex);
		int ymin = Mathf.RoundToInt((pos.z - r.outer) * worldToTex);
		int xmax = Mathf.RoundToInt((pos.x + r.outer) * worldToTex);
		int ymax = Mathf.RoundToInt((pos.z + r.outer) * worldToTex);

		int cx = Mathf.RoundToInt(pos.x * worldToTex);
		int cy = Mathf.RoundToInt(pos.z * worldToTex);

		cx = Mathf.Clamp(cx, 0, textureSize - 1);
		cy = Mathf.Clamp(cy, 0, textureSize - 1);

		int minRange = Mathf.RoundToInt(r.inner * r.inner * worldToTex * worldToTex);
		int maxRange = Mathf.RoundToInt(r.outer * r.outer * worldToTex * worldToTex);
		int gh = WorldToGridHeight(r.pos.y);
		int variance = Mathf.RoundToInt(Mathf.Clamp01(margin / (heightRange.y - heightRange.x)) * 255);
		Color32 white = new Color32(255, 255, 255, 255);

		for (int y = ymin; y < ymax; ++y)
		{
			if (y > -1 && y < textureSize)
			{
				for (int x = xmin; x < xmax; ++x)
				{
					if (x > -1 && x < textureSize)
					{
						int xd = x - cx;
						int yd = y - cy;
						int dist = xd * xd + yd * yd;
						int index = x + y * textureSize;

						if (dist < minRange || (cx == x && cy == y))
						{
							mBuffer1[index] = white;
						}
						else if (dist < maxRange)
						{
							Vector2 v = new Vector2(xd, yd);
							v.Normalize();
							v *= r.inner;

							int sx = cx + Mathf.RoundToInt(v.x);
							int sy = cy + Mathf.RoundToInt(v.y);

							if (sx > -1 && sx < textureSize &&
								sy > -1 && sy < textureSize &&
								IsVisible(sx, sy, x, y, Mathf.Sqrt(dist), gh, variance))
							{
								mBuffer1[index] = white;
							}
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// 使用缓存的结果显示地图。.
	/// </summary>

	void RevealUsingCache (Revealer r, float worldToTex)
	{
		if (r.cachedBuffer == null) RevealIntoCache(r, worldToTex);

		Color32 white = new Color32(255, 255, 255, 255);

		for (int y = r.cachedY, ymax = r.cachedY + r.cachedSize; y < ymax; ++y)
		{
			if (y > -1 && y < textureSize)
			{
				int by = y * textureSize;
				int cy = (y - r.cachedY) * r.cachedSize;

				for (int x = r.cachedX, xmax = r.cachedX + r.cachedSize; x < xmax; ++x)
				{
					if (x > -1 && x < textureSize)
					{
						int cachedIndex = x - r.cachedX + cy;

						if (r.cachedBuffer[cachedIndex])
						{
							mBuffer1[x + by] = white;
						}
					}
				}
			}
		}
	}

	/// <summary>
	///创建缓存的可见结果。
	/// </summary>

	void RevealIntoCache (Revealer r, float worldToTex)
	{
		// 相对于战争迷雾的位置
		Vector3 pos = r.pos - mOrigin;

		// 我们要处理的坐标
		int xmin = Mathf.RoundToInt((pos.x - r.outer) * worldToTex);
		int ymin = Mathf.RoundToInt((pos.z - r.outer) * worldToTex);
		int xmax = Mathf.RoundToInt((pos.x + r.outer) * worldToTex);
		int ymax = Mathf.RoundToInt((pos.z + r.outer) * worldToTex);

		int cx = Mathf.RoundToInt(pos.x * worldToTex);
		int cy = Mathf.RoundToInt(pos.z * worldToTex);

		cx = Mathf.Clamp(cx, 0, textureSize - 1);
		cy = Mathf.Clamp(cy, 0, textureSize - 1);

		// 创建缓冲区以显示
		int size = Mathf.RoundToInt(xmax - xmin);		
		r.cachedBuffer = new bool[size * size];
		r.cachedSize = size;
		r.cachedX = xmin;
		r.cachedY = ymin;

		// 缓冲区应该从clear开始
		for (int i = 0, imax = size * size; i < imax; ++i) r.cachedBuffer[i] = false;

		int minRange = Mathf.RoundToInt(r.inner * r.inner * worldToTex * worldToTex);
		int maxRange = Mathf.RoundToInt(r.outer * r.outer * worldToTex * worldToTex);
		int variance = Mathf.RoundToInt(Mathf.Clamp01(margin / (heightRange.y - heightRange.x)) * 255);
		int gh = WorldToGridHeight(r.pos.y);

		for (int y = ymin; y < ymax; ++y)
		{
			if (y > -1 && y < textureSize)
			{
				for (int x = xmin; x < xmax; ++x)
				{
					if (x > -1 && x < textureSize)
					{
						int xd = x - cx;
						int yd = y - cy;
						int dist = xd * xd + yd * yd;

						if (dist < minRange || (cx == x && cy == y))
						{
							r.cachedBuffer[(x - xmin) + (y - ymin) * size] = true;
						}
						else if (dist < maxRange)
						{
							Vector2 v = new Vector2(xd, yd);
							v.Normalize();
							v *= r.inner;

							int sx = cx + Mathf.RoundToInt(v.x);
							int sy = cy + Mathf.RoundToInt(v.y);

							if (sx > -1 && sx < textureSize &&
								sy > -1 && sy < textureSize &&
								IsVisible(sx, sy, x, y, Mathf.Sqrt(dist), gh, variance))
							{
								r.cachedBuffer[(x - xmin) + (y - ymin) * size] = true;
							}
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// 模糊的可见性数据。
	/// </summary>

	void BlurVisibility ()
	{
		Color32 c;

		for (int y = 0; y < textureSize; ++y)
		{
			int yw = y * textureSize;
			int yw0 = (y - 1);
			if (yw0 < 0) yw0 = 0;
			int yw1 = (y + 1);
			if (yw1 == textureSize) yw1 = y;

			yw0 *= textureSize;
			yw1 *= textureSize;

			for (int x = 0; x < textureSize; ++x)
			{
				int x0 = (x - 1);
				if (x0 < 0) x0 = 0;
				int x1 = (x + 1);
				if (x1 == textureSize) x1 = x;

				int index = x + yw;
				int val = mBuffer1[index].r;

				val += mBuffer1[x0 + yw].r;
				val += mBuffer1[x1 + yw].r;
				val += mBuffer1[x + yw0].r;
				val += mBuffer1[x + yw1].r;

				val += mBuffer1[x0 + yw0].r;
				val += mBuffer1[x1 + yw0].r;
				val += mBuffer1[x0 + yw1].r;
				val += mBuffer1[x1 + yw1].r;

				c = mBuffer2[index];
				c.r = (byte)(val / 9);
				mBuffer2[index] = c;
			}
		}

		// 交换缓冲区，使模糊的缓冲区被使用
		Color32[] temp = mBuffer1;
		mBuffer1 = mBuffer2;
		mBuffer2 = temp;
	}

	/// <summary>
	///通过更新绿色通道来显示地图最大的红色通道。
	/// </summary>

	void RevealMap ()
	{
		for (int y = 0; y < textureSize; ++y)
		{
			int yw = y * textureSize;

			for (int x = 0; x < textureSize; ++x)
			{
				int index = x + yw;
				Color32 c = mBuffer1[index];
				
				if (c.g < c.r)
				{
					c.g = c.r;
					mBuffer1[index] = c;
				}
			}
		}
	}

	/// <summary>
	/// 使用新的颜色缓冲区更新指定的纹理。
	/// </summary>

	void UpdateTexture ()
	{
		if (mScreenHeight != Screen.height || mTexture0 == null)
		{
			mScreenHeight = Screen.height;

			if (mTexture0 != null) Destroy(mTexture0);
			if (mTexture1 != null) Destroy(mTexture1);

			// 原生ARGB格式是最快的，因为它不涉及数据转换
			mTexture0 = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
			mTexture1 = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);

			mTexture0.wrapMode = TextureWrapMode.Clamp;
			mTexture1.wrapMode = TextureWrapMode.Clamp;

			mTexture0.SetPixels32(mBuffer0);
			mTexture0.Apply();
			mTexture1.SetPixels32(mBuffer1);
			mTexture1.Apply();
			mState = State.Blending;
		}
		else if (mState == State.UpdateTexture0)
		{
			// 纹理更新在两个帧之间传播，使其在得到更新时变得更不明显
			mTexture0.SetPixels32(mBuffer0);
			mTexture0.Apply();
			mState = State.UpdateTexture1;
			mBlendFactor = 0f;
		}
		else if (mState == State.UpdateTexture1)
		{
			mTexture1.SetPixels32(mBuffer1);
			mTexture1.Apply();
			mState = State.Blending;
		}
	}

	/// <summary>
	/// 检查指定位置是否当前可见。
	/// </summary>

	public bool IsVisible (Vector3 pos)
	{
		if (mBuffer0 == null) return false;
		pos -= mOrigin;

		float worldToTex = (float)textureSize / worldSize;
		int cx = Mathf.RoundToInt(pos.x * worldToTex);
		int cy = Mathf.RoundToInt(pos.z * worldToTex);

		cx = Mathf.Clamp(cx, 0, textureSize - 1);
		cy = Mathf.Clamp(cy, 0, textureSize - 1);
		int index = cx + cy * textureSize;
		return mBuffer1[index].r > 0 || mBuffer0[index].r > 0;
	}

	/// <summary>
	/// 检查指定的位置是否已经被探索。
	/// </summary>

	public bool IsExplored (Vector3 pos)
	{
		if (mBuffer0 == null) return false;
		pos -= mOrigin;

		float worldToTex = (float)textureSize / worldSize;
		int cx = Mathf.RoundToInt(pos.x * worldToTex);
		int cy = Mathf.RoundToInt(pos.z * worldToTex);

		cx = Mathf.Clamp(cx, 0, textureSize - 1);
		cy = Mathf.Clamp(cy, 0, textureSize - 1);
		return mBuffer0[cx + cy * textureSize].g > 0;
	}
}