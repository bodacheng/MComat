using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Json;

namespace dataAccess
{
    public partial class AccountSet
    {
        public static void LoadCustomerInfoViaLocalFile()
        {
            bool ok = false;
            try
            {
                PlayerAccountInfo info = new PlayerAccountInfo();
                string wholepath = Application.persistentDataPath + "/localAccountInfo.json";
                if (File.Exists(wholepath))
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    info = JsonConvert.DeserializeObject<PlayerAccountInfo>(dataAsJson);
                    Debug.Log("玩家账户信息读取成功");
                }
                _AccInfo = info;
                ok = true;
            }
            catch (Exception e)
            {
                Debug.Log("玩家账户信息读取失败");
                Debug.Log(e.ToString());
                _AccInfo = new PlayerAccountInfo();
            }
            if (!ok)
            {
                OverrideAccountOnLocalFile();
            }
        }
        
        public static void OverrideAccountOnLocalFile()
        {
            string json = JsonConvert.SerializeObject(_AccInfo);
            LocalJson.SaveToJsonFile_persistentDataPath(null, "localAccountInfo.json", json);
        }
    }
}