using System.Collections.Generic;
using Api.Dto.Model;

namespace dataAccess
{
    //这个函数应该是个一上来就从本地。。。或数据库读取的东西，应该存在很多协程类函数，因为到时候牵扯到从数据库直接读取信息。
    public partial class MyMonsters
    {
        public static IDictionary<string, UnitInfo> Dic = new Dictionary<string, UnitInfo>();
        
        public static bool CheckExist(string key)
        {
            if (key == null)
            {
                return false;
            }
            if (Dic.ContainsKey(key))
            {
                if (Dic[key] != null)
                    return true;
            }
            return false;
        }
        
        public static UnitInfo Get(string instanceId)
        {
            if (instanceId == null)
            {
                return null;
            }
            if (Dic.ContainsKey(instanceId))
            {
                if (Dic[instanceId] != null)
                    return Dic[instanceId];
            }
            return null;
        }
    }
}