using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeaguesXF.Services
{
    public class NavParams
    {

        Dictionary<string, object> paraList = new Dictionary<string, object>();

        public void Add(string key, object value)
        {
            paraList[key] = value;
        }
        public T Get<T>(string key)
        {
            if (paraList.ContainsKey(key))
                return (T)paraList[key];
            else
                return default(T);
        }
    }
}
