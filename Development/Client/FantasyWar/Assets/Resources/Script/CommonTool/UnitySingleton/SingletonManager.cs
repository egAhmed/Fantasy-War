using System.Collections;
using System.Collections.Generic;

public static class SingletonManager
{
    private static ArrayList SingletonList = new ArrayList();

    public static T AddSigleton<T>() where T:Singleton<T>,new()
    {
        T tmp = new T();
        SingletonList.Add(tmp);
        return tmp;
    }

    public static void RemoveSingleton<T>() where T : Singleton<T>, new()
    {
        int tmp = -1;
        for (int i = 0; i < SingletonList.Count; i++)
        {
            if (SingletonList[i] is T)
                tmp = i;
        }
        if(tmp>-1)
        {
            SingletonList.Remove(SingletonList[tmp]);
        }
    }
}
