using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections;
using Api.Dto.Model;
using Json;

namespace dataAccess
{
    public partial class MyMonsters
    {
        public static List<UnitInfo> LoadAll_Json(string filePath)
        {
            List<UnitInfo> charList = new List<UnitInfo>();
            UnitInfo info;
            if (Directory.Exists(filePath))
            {
                foreach (string file in Directory.GetFiles(filePath))
                {
                    try
                    {
                        string dataAsJson = File.ReadAllText(file);
                        info = JsonConvert.DeserializeObject<UnitInfo>(dataAsJson);
                        charList.Add(info);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("尝试读取以下路径角色信息结果出错："+ file);
                        Debug.Log(e.ToString());
                    }
                }
            }
            return charList;
        }
        
        public static void LoadTutorial()
        {
            List<UnitInfo> charList = new List<UnitInfo>();
            charList = LoadAll_Json(Application.persistentDataPath + "/TutorialCharacterInfos");
            Dic.Clear();
            foreach (UnitInfo one in charList)
            {
                if (!Dic.ContainsKey(one.InstanceId))
                    Dic.Add(one.InstanceId, one);
                else
                    Debug.Log("重复的角色存档id：" + one.InstanceId);
            }
        }
    }
}