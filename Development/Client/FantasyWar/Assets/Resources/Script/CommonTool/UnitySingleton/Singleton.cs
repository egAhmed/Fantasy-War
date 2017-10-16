using System.Collections;
using System.Collections.Generic;

public class Singleton<T> where T : Singleton<T>, new()
{
    private static T _shareInstance;
    private static readonly object syslock = new object();

    public static T ShareInstance
    {
        get
        {
            if (_shareInstance == null)
            {
                //lock (syslock)
                //{
                //    if (_shareInstance == null)
                //    {
                _shareInstance = SingletonManager.AddSigleton<T>();
                //    }
                //}
            }
            return _shareInstance;
        }
    }
}
