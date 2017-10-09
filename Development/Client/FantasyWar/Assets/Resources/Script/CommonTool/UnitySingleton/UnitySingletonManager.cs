using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitySingletonManager
{
    private static GameObject _unitySingletonLoader;
    private static GameObject UnitySingletonLoader
    {
        get
        {
            if (_unitySingletonLoader == null) {
                _unitySingletonLoader = new GameObject();
                _unitySingletonLoader.name = "UnitySingletonManager";
                GameObject.DontDestroyOnLoad(_unitySingletonLoader);
            }
            return _unitySingletonLoader;
        }
    }
    public static T addUnitySingletonComponent<T>()where T:UnitySingleton<T>{
        return UnitySingletonLoader.AddComponent<T>();
    }

    public static void removeUnitySingletonComponent<T>()where T:UnitySingleton<T>{
        Component dc=UnitySingletonLoader.GetComponentInChildren<T>();
        if (dc != null) { 
            GameObject.Destroy(dc);
            dc = null;
        }
    }
}
