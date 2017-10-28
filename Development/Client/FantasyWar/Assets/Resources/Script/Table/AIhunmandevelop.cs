using System;
using System.Collections.Generic;
namespace Settings
{
    public partial struct AIhunmandevelop : TabFileDataBase
    {

		public int id;
		public int resid;
		public int nums;

		private static Dictionary<int, AIhunmandevelop> datas = null;
		public static List<int> idList = null;
		
		public static AIhunmandevelop Get(int id)
		{
			if (datas.ContainsKey(id))
				return datas[id];
			AIhunmandevelop nullValue = new AIhunmandevelop();
			return nullValue;
		}
		
		public bool isNull{ get { return id == 0; } }
		public void LoadData(string context)
		{
			string[] lines = context.Split('\n');
			int lineCount = lines.Length;
			datas = new Dictionary<int, AIhunmandevelop>(lineCount);
			idList = new List<int>(lineCount);
			for (int i = 0; i < lineCount; i++)
			{
				string line = lines[i];
				if (string.IsNullOrEmpty(line))
					continue;
				string[] paramList = line.Split('\t');
				AIhunmandevelop data = new AIhunmandevelop();
				int paramIndex = 0;
				int.TryParse(paramList[paramIndex++], out data.id);
				int.TryParse(paramList[paramIndex++], out data.resid);
				int.TryParse(paramList[paramIndex++], out data.nums);

				datas[data.id] = data;
				idList.Add(data.id);
			}
		}
	}
}