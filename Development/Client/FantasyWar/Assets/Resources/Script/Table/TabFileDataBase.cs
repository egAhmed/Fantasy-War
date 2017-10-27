using System.Collections.Generic;
namespace Settings
{
    public interface TabFileDataBase
    {
        void LoadData(string content);
    }

    public class DefaultValue
    {
        public static List<string> ListString = new List<string>();
        public static List<int> ListInt = new List<int>();
        public static List<float> ListFloat = new List<float>();
    }
}

