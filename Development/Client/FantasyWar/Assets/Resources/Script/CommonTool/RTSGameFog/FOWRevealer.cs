using UnityEngine;

/// <summary>
/// 为了工作，战争系统的雾需要3个组件:
/// - 战争系统的迷雾将创建你的场景的高度图并执行所有的更新。
/// - 战争图像对摄像机的影响，将显示战争的迷雾。
/// - 在世界上的一个或多个游戏对象上，有一个或多个游戏对象的迷雾(这类)。
/// </summary>

[AddComponentMenu("Fog of War/Revealer")]
public class FOWRevealer : MonoBehaviour
{
	
	Transform mTrans;

	/// <summary>
	/// 该区域的半径被揭示。X下面的内容总是显示出来的。所有的事情都可能发生，也可能不会被揭露。
	/// </summary>

	public Vector2 range = new Vector2(1f, 15f);

	/// <summary>
	/// 将会进行什么样的视力检查。
	/// - “None”指的是视线检查的范围，以及半径范围内的整个区域。y将被揭示。
	/// - “only once”表示只执行一次查看检查行，结果将被缓存。
	/// - “EveryUpdate”意思是每次更新都要执行查看检查。适合移动对象。
	/// </summary>

	public FOWSystem.LOSChecks lineOfSightCheck = FOWSystem.LOSChecks.EveryUpdate ;

	/// <summary>
	///探宝器是否真的活跃。如果你想要额外的检查，比如“单位死亡吗?”
	/// 然后简单地从这个类派生并相应地改变“isActive”值。
	/// </summary>

	public bool isActive = true;

	FOWSystem.Revealer mRevealer;

	void Awake ()
	{
		mTrans = transform;
		mRevealer = FOWSystem.CreateRevealer();
	}

	void OnDisable ()
	{
		mRevealer.isActive = false;
	}

	void OnDestroy ()
	{
		FOWSystem.DeleteRevealer(mRevealer);
		mRevealer = null;
	}
	
	void LateUpdate ()
	{
		if (isActive)
		{
			if (lineOfSightCheck != FOWSystem.LOSChecks.OnlyOnce) mRevealer.cachedBuffer = null;

			mRevealer.pos = mTrans.position;
			mRevealer.inner = range.x;
			mRevealer.outer = range.y;
			mRevealer.los = lineOfSightCheck;
			mRevealer.isActive = true;
		}
		else
		{
			mRevealer.isActive = false;
			mRevealer.cachedBuffer = null;
		}
	}

	void OnDrawGizmosSelected ()
	{
		if (lineOfSightCheck != FOWSystem.LOSChecks.None && range.x > 0f)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, range.x);
		}
		Gizmos.color = Color.black ;
		Gizmos.DrawWireSphere(transform.position, range.y);
	}

	/// <summary>
	///想要强制重建缓存缓冲区吗?调用这个函数。
	/// </summary>

	public void Rebuild () { mRevealer.cachedBuffer = null; }
}