using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class TeamSet
    {
        public static MonsterTeamOfPlayerModel LoadMyTeamSetInfoViaJsonFile(string jsonFilename)
        {
            string wholepath = Application.persistentDataPath + "/" + jsonFilename;
            MonsterTeamOfPlayerModel TeamSet;
            if (File.Exists(wholepath))
            {
                try
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    TeamSet = JsonConvert.DeserializeObject<MonsterTeamOfPlayerModel>(dataAsJson);
                }
                catch (Exception e)
                {
                    Debug.Log("读取阵容配置文件："+jsonFilename+"发生异常"+ e);
                    TeamSet = new MonsterTeamOfPlayerModel();
                }
                return TeamSet;
            }
            else
            {
                Debug.Log("读取阵容配置文件："+jsonFilename+"没有找到");
                return new MonsterTeamOfPlayerModel();
            }
        }
    }
}