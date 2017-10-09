using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour where T : UnitySingleton<T>
{
    private static T _shareInstance;
    public static T ShareInstance
    {
        get
        {
            if (_shareInstance == null)
            {
                shareInstanceInit();
            }
            return _shareInstance;
        }
    }
   //
    private static void shareInstanceInit() { 
        _shareInstance = UnitySingletonManager.addUnitySingletonComponent<T>();
    }
}
