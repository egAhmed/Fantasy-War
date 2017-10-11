using UnityEngine;

/// <summary>
///将战争渲染器添加到任何游戏对象将会隐藏对象的渲染器，如果它们不能根据战争的迷雾可见。
/// </summary>

[AddComponentMenu("Fog of War/Renderers")]
public class FOWRenderers : MonoBehaviour
{
	Transform mTrans;
	Renderer[] mRenderers;
	float mNextUpdate = 0f;
	bool mIsVisible = true;
	bool mUpdate = true;

	/// <summary>
	/// 渲染器是否当前可见。
	/// </summary>

	public bool isVisible { get { return mIsVisible; } }

	/// <summary>
	///重建渲染器列表并立即更新它们的可见状态。
	/// </summary>

	public void Rebuild () { mUpdate = true; }

	void Awake () { mTrans = transform; }
	void LateUpdate () { if (mNextUpdate < Time.time) UpdateNow(); }

	void UpdateNow ()
	{
		mNextUpdate = Time.time + 0.075f + Random.value * 0.05f;

		if (FOWSystem.instance == null)
		{
			enabled = false;
			return;
		}

		if (mUpdate) mRenderers = GetComponentsInChildren<Renderer>();

		bool visible = FOWSystem.instance.IsVisible(mTrans.position);

		if (mUpdate || mIsVisible != visible)
		{
			mUpdate = false;
			mIsVisible = visible;

			for (int i = 0, imax = mRenderers.Length; i < imax; ++i)
			{
				Renderer ren = mRenderers[i];

				if (ren)
				{
					ren.enabled = mIsVisible;
				}
				else
				{
					mUpdate = true;
					mNextUpdate = Time.time;
				}
			}
		}
	}
}