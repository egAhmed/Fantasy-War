using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBehaviorTreeBase : MonoBehaviour
{
    bool TF = true;

    public delegate IEnumerator DelBehaviorTree(List<DelBehaviorTree> del);
    /// <summary>
    /// 串行的AND
    /// </summary>
    /// <returns></returns>
    public IEnumerator Sequence(List<DelBehaviorTree> del)
    {
        foreach (var item in del)
        {
            item(del);
            if (TF)
               yield return null;
        }
    }

    /// <summary>
    /// 串行的OR
    /// </summary>
    /// <returns></returns>
    public bool Selector(List<DelBehaviorTree> del)
    {
        foreach (var item in del)
        {
            
        }
        return true;
    }

    /// <summary>
    /// 并行的AND,需要暂时弃用
    /// </summary>
    /// <returns></returns>
    public bool ParallelSequence(List<DelBehaviorTree> del)
    {
        return true;
    }

    /// <summary>
    /// 并行的OR，需要用到多线程暂时弃用
    /// </summary>
    /// <returns></returns>
    public bool ParallelSelector(List<DelBehaviorTree> del)
    {
        return true;
    }

    //private AIBehaviorTreeBase()
    //{

    //}

    //public AIBehaviorTreeBase(AIBehaviorTreeBase AIBT)
    //{
    //    Application.LoadLevelAsync
    //}
    
}
