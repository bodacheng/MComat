using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace dataAccess
{
    public partial class TeamSet
    {
        public IEnumerator LoadMyTeamSetInfoViaJsonFile(string jsonFilename)
        {
            string wholepath = Application.persistentDataPath + "/" + jsonFilename;
            PositionLocalCharKeySet TeamSet;
            if (File.Exists(wholepath))
            {
                try 
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    TeamSet = JsonConvert.DeserializeObject<PositionLocalCharKeySet>(dataAsJson);
                    List<PosNumWithLocalKey> posNumWithLocalKeys = TeamSet.PosNumsWithLocalKeys.ToList();
                    List<int> shouldhave = new List<int>();
                    foreach(PosNumWithLocalKey posNumWithLocalKey in posNumWithLocalKeys)
                    {
                        if (!shouldhave.Contains(posNumWithLocalKey.posNum))
                            shouldhave.Add(posNumWithLocalKey.posNum);
                    }
                    if (!shouldhave.Contains(0))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(0,null));
                    }
                    if (!shouldhave.Contains(2))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(2,null));
                    }
                    if (!shouldhave.Contains(1))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(1,null));
                    }
                    if (!shouldhave.Contains(3))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(3,null));
                    }
                    TeamSet.PosNumsWithLocalKeys = posNumWithLocalKeys.ToArray(); 
                }catch (Exception e)
                {
                    Debug.Log("读取阵容配置文件："+jsonFilename+"发生异常"+ e);
                    TeamSet = new PositionLocalCharKeySet();
                }
                yield return TeamSet;
                yield break;
            }
            else
            {
                Debug.Log("读取阵容配置文件："+jsonFilename+"没有找到");
                yield return new PositionLocalCharKeySet();
                yield break;
            }
        }

        public void OverrideTeamSetInfoOnJsonFile(TeamSetGameMode teamSetGameMode)
        {
            switch (teamSetGameMode)
            {
                case TeamSetGameMode.story:
                    string json = JsonConvert.SerializeObject(storyModeTeamSet);
                    LocalJson.saveInfoToJsonFile(null, "TeamSet.json", json);
                    break;
                case TeamSetGameMode.arena3V3:
                    string json1 = JsonConvert.SerializeObject(Arena3V3);
                    LocalJson.saveInfoToJsonFile(null, "arena3V3TeamSet.json", json1);
                    break;
            }
        }
    }
}