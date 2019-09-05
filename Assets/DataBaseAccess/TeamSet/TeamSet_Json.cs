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
        public IEnumerator loadMyTeamSetInfoViaJsonFile(string jsonFilename)
        {
            string wholepath = Application.persistentDataPath + "/" + jsonFilename;
            positionLocalCharKeySet TeamSet;
            if (File.Exists(wholepath))
            {
                try 
                {
                    string dataAsJson = File.ReadAllText(wholepath);
                    TeamSet = JsonConvert.DeserializeObject<positionLocalCharKeySet>(dataAsJson);
                    List<PosNumWithLocalKey> posNumWithLocalKeys = TeamSet.PosNumsWithLocalKeys.ToList();
                    List<PosNum> shouldhave = new List<PosNum>();
                    foreach(PosNumWithLocalKey posNumWithLocalKey in posNumWithLocalKeys)
                    {
                        if (!shouldhave.Contains(posNumWithLocalKey.posNum))
                            shouldhave.Add(posNumWithLocalKey.posNum);
                    }
                    if (!shouldhave.Contains(PosNum.back))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(PosNum.back,null));
                    }
                    if (!shouldhave.Contains(PosNum.front))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(PosNum.front,null));
                    }
                    if (!shouldhave.Contains(PosNum.left))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(PosNum.left,null));
                    }
                    if (!shouldhave.Contains(PosNum.right))
                    {
                        posNumWithLocalKeys.Add(new PosNumWithLocalKey(PosNum.right,null));
                    }
                    TeamSet.PosNumsWithLocalKeys = posNumWithLocalKeys.ToArray(); 
                }catch (Exception e)
                {
                    Debug.Log("读取阵容配置文件："+jsonFilename+"发生异常"+ e);
                    TeamSet = new positionLocalCharKeySet();
                }
                yield return TeamSet;
                yield break;
            }
            else
            {
                Debug.Log("读取阵容配置文件："+jsonFilename+"没有找到");
                yield return new positionLocalCharKeySet();
                yield break;
            }
        }

        public void overrideTeamSetInfoOnJsonFile(TeamSetGameMode teamSetGameMode)
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