using System;
using System.Collections.Generic;
namespace Settings
{
    public partial struct BuildingTable : TabFileDataBase
    {

		public int id;
		public string type;
		public int attack;
		public int HP;
		public float Range;
		public float atkSpeed;
		public float moveSpeed;
		public string path;

		private static Dictionary<int, BuildingTable> datas = null;
		public static List<int> idList = null;
		
		public static BuildingTable Get(int id)
		{
			if (datas.ContainsKey(id))
				return datas[id];
			BuildingTable nullValue = new BuildingTable();
			return nullValue;
		}
		
		public bool isNull{ get { return id == 0; } }
		public void LoadData(string context)
		{
			string[] lines = context.Split('\n');
			int lineCount = lines.Length;
			datas = new Dictionary<int, BuildingTable>(lineCount);
			idList = new List<int>(lineCount);
			for (int i = 0; i < lineCount; i++)
			{
				string line = lines[i];
				if (string.IsNullOrEmpty(line))
					continue;
				string[] paramList = line.Split('\t');
				BuildingTable data = new BuildingTable();
				int paramIndex = 0;
				int.TryParse(paramList[paramIndex++], out data.id);
				data.type = paramList[paramIndex++];
				int.TryParse(paramList[paramIndex++], out data.attack);
				int.TryParse(paramList[paramIndex++], out data.HP);
				float.TryParse(paramList[paramIndex++], out data.Range);
				float.TryParse(paramList[paramIndex++], out data.atkSpeed);
				float.TryParse(paramList[paramIndex++], out data.moveSpeed);
				data.path = paramList[paramIndex++];

				datas[data.id] = data;
				idList.Add(data.id);
			}
		}
	}
}