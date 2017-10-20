using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TableManage
{
    /// <summary>
    /// 初始化所有表格
    /// </summary>
    public static void Start()
    {
        Load(@"TabFileDataBase");
        Load(@"ResourcesTable");
        Load(@"AIhunmandevelop");
    }

    static void Load(string path)
    {
        if (File.Exists(Application.streamingAssetsPath + path))
        {
            if (Settings.ResourcesTable.idList != null)
                return;

            byte[] buffer = new byte[1024 * 1024];
            FileStream fs = File.Open(Application.streamingAssetsPath + path, FileMode.Open);
            int leng = fs.Read(buffer, 0, (int)fs.Length);
            string str = System.Text.Encoding.UTF8.GetString(buffer, 0, leng);
            Settings.ResourcesTable.Get(0).LoadData(str);

        }
    }

}
