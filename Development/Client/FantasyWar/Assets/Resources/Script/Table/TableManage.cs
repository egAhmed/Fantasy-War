using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Settings
{

    public class TableManage
    {
        /// <summary>
        /// 初始化所有表格
        /// </summary>
        public static void Start()
        {
            Debug.Log("start");
            LoadResourcesTable(@"/ResourcesTable.txt");
            LoadAIhunmandevelop(@"/AIhunmandevelop.txt");
        }

        static void LoadResourcesTable(string path)
        {
            Debug.Log(Application.streamingAssetsPath + path);
            Debug.Log("start   " + path +"    "+ (File.Exists(Application.streamingAssetsPath + path)));
            if (File.Exists(Application.streamingAssetsPath + path))
            {
                if (Settings.ResourcesTable.idList != null)
                    return;

                byte[] buffer = new byte[1024 * 1024];
                FileStream fs = File.Open(Application.streamingAssetsPath + path, FileMode.Open);
                int leng = fs.Read(buffer, 0, (int)fs.Length);
                string str = System.Text.Encoding.UTF8.GetString(buffer, 0, leng);
                new Settings.ResourcesTable().LoadData(str);

            }
        }

        static void LoadAIhunmandevelop(string path)
        {
            Debug.Log(Application.streamingAssetsPath + path);
            Debug.Log("start   " + path + "    " + (File.Exists(Application.streamingAssetsPath + path)));
            if (File.Exists(Application.streamingAssetsPath + path))
            {
                if (Settings.AIhunmandevelop.idList != null)
                    return;

                byte[] buffer = new byte[1024 * 1024];
                FileStream fs = File.Open(Application.streamingAssetsPath + path, FileMode.Open);
                int leng = fs.Read(buffer, 0, (int)fs.Length);
                string str = System.Text.Encoding.UTF8.GetString(buffer, 0, leng);
                new Settings.AIhunmandevelop().LoadData(str);

            }
        }
    }
}
