using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class FSMState
{
    //用来确定是否需要转换到其他状态
    public abstract void Reason(Transform player, Transform npc);
    //本状态的角色行为
    public abstract void Act(Transform player, Transform npc);
}
