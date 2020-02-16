using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections;

namespace dataAccess
{
    public partial class AccountSet
    {
        public IEnumerator LoadCustomerInfoViaLocalFile()
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
                else
                {
                }
                this._PlayerAccountInfo = info;
            }
            catch (Exception e)
            {
                Debug.Log("玩家账户信息读取失败");
                Debug.Log(e.ToString());
                this._PlayerAccountInfo = new PlayerAccountInfo();
            }
            yield return OverrideAccountOnLocalFile();
            yield break;
        }

        public IEnumerator OverrideAccountOnLocalFile()
        {
            OverrideLocalCustomerInfoOnLocalFile(this._PlayerAccountInfo);
            yield break;
        }

        public bool OverrideLocalCustomerInfoOnLocalFile(PlayerAccountInfo refreshedPlayerAccountInfo)
        {
            try
            {
                string json = JsonConvert.SerializeObject(refreshedPlayerAccountInfo);
                LocalJson.saveInfoToJsonFile(null, "localAccountInfo.json", json);
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