using UnityEngine;
using Newtonsoft.Json;
using Api.Dto.Model;
using Json;
using System;
using System.IO;

namespace dataAccess
{
    public partial class TeamSet
    {
        public static TeamPos OverrideTeamSetInfoOnJsonFile(TeamSetGameMode mode)
        {
            string json;
            TeamPos model = null;
            switch (mode)
            {
                case TeamSetGameMode.story:
                    model = Default.ToTeamPos();
                    json = JsonConvert.SerializeObject(model);
                    LocalJson.SaveToJsonFile_persistentDataPath(null, "TeamSet.json", json);
                    break;
                case TeamSetGameMode.arena3V3:
                    model = Arena3V3.ToTeamPos();
                    json = JsonConvert.SerializeObject(model);
                    LocalJson.SaveToJsonFile_persistentDataPath(null, "arena3V3TeamSet.json", json);
                    break;
            }
            return model;
        }

        public static TeamPos LoadMyTeamSetInfoViaJsonFile(string jsonFilename)
        {
            string wholepath = Application.persistentDataPath + "/" + jsonFilename;
            TeamPos TeamSet;
            if (File.Exists(wholepath))
            {
                try
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    TeamSet = JsonConvert.DeserializeObject<TeamPos>(dataAsJson);
                }
                catch (Exception e)
                {
                    Debug.Log("读取阵容配置文件："+jsonFilename+"发生异常"+ e);
                    TeamSet = new TeamPos();
                }
                return TeamSet;
            }
            else
            {
                Debug.Log("读取阵容配置文件："+jsonFilename+"没有找到");
                return new TeamPos();
            }
        }
    }
}