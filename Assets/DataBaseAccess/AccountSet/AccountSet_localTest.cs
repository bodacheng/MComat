using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections;

namespace dataAccess
{
    public partial class AccountSet
    {
        public static IEnumerator LoadCustomerInfoViaLocalFile()
        {
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
            }
            catch (Exception e)
            {
                Debug.Log("玩家账户信息读取失败");
                Debug.Log(e.ToString());
                _AccInfo = new PlayerAccountInfo();
            }
            yield break;
        }

        public static IEnumerator OverrideAccountOnLocalFile()
        {
            OverrideLocalCustomerInfoOnLocalFile(_AccInfo);
            yield break;
        }

        public static bool OverrideLocalCustomerInfoOnLocalFile(PlayerAccountInfo refreshedPlayerAccountInfo)
        {
            try
            {
                string json = JsonConvert.SerializeObject(refreshedPlayerAccountInfo);
                LocalJson.SaveInfoToJsonFile_persistentDataPath(null, "localAccountInfo.json", json);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log("玩家信息保存失败");
                Debug.Log(e.ToString());
                return false;
            }
        }
    }
}