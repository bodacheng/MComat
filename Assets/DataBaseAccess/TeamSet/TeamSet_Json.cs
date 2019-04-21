using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class TeamSet
{
    public IEnumerator loadMyTeamSetInfoViaJsonFile(string jsonFilename)
    {
        string wholepath = Application.persistentDataPath + "/" + jsonFilename;
        if (File.Exists(wholepath))
        {
            string dataAsJson = File.ReadAllText(wholepath);
            _positionLocalCharKeySet4V4Mode = JsonConvert.DeserializeObject<positionLocalCharKeySet>(dataAsJson);
            yield break;
        }
        else
        {
            Debug.Log("开始新建json格式打架阵容存档");
            File.Create(wholepath);
            yield break;
        }
    }

    public void overrideTeamSetInfoOnJsonFile()
    {
        string json = JsonConvert.SerializeObject(_positionLocalCharKeySet4V4Mode);
        saveInfoToJsonFile("TeamSet.json", json);
    }

    public void saveInfoToJsonFile(string subpathWithfilename, string json)
    {
        //string wholepath = Path.Combine(Application.persistentDataPath, subpath);
        string wholepath = Application.persistentDataPath + "/" + subpathWithfilename;
        File.WriteAllText(wholepath, json, System.Text.Encoding.UTF8);
    }
}
