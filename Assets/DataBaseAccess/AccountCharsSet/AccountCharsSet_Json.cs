using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class AccountCharsSet
{
    public void loadMyOwnedCharsInfoViaJsonFile(string jsonFilename)
    {
        try
        {
            CharacterDataInfo[] info = new CharacterDataInfo[0];
            string wholepath = Application.persistentDataPath + "/" + jsonFilename;
            if (File.Exists(wholepath))
            {
                string dataAsJson = File.ReadAllText(wholepath);
                info = JsonConvert.DeserializeObject<CharacterDataInfo[]>(dataAsJson);
                ownedChars = info;

                //CharsManager.loadMonsterDataBaseFileByResource();
                //List<CharacterResourceInfo> characterList = CharsManager._monstersConfigTable.RowToCharacterResourceInfoList(CharsManager._monstersConfigTable.rowList);
                //foreach (CharacterResourceInfo _CharacterResourceInfo in characterList)
                //{
                //    CharacterDataInfo _CharacterDataInfo = new CharacterDataInfo(
                //        _CharacterResourceInfo.charResouceNum, _CharacterResourceInfo.type, _CharacterResourceInfo.charResouceNum, new NineAndTwo()
                //    );
                //    addNewCharToJsonSaveData(_CharacterDataInfo);
                //}
                //overrideMyCharsInfoOnJsonFile();
            }
            else
            {
                localSaveDataGetAllCharacters(wholepath);
            }
        }catch(Exception e)
        {
            Debug.Log(e+" 角色读取过程有些问题。测试用json文档格式？");
            string wholepath = Application.persistentDataPath + "/" + jsonFilename;
            localSaveDataGetAllCharacters(wholepath);
        }
    }

    public void localSaveDataGetAllCharacters(string wholepath)
    {
        Debug.Log("开始新建json格式存档");
        AccountCharsSet.ownedChars = new CharacterDataInfo[0];
        File.Create(wholepath).Dispose();
        CharsManager.loadMonsterDataBaseFileByResource();
        List<CharacterResourceInfo> characterList = CharsManager._monstersConfigTable.RowToCharacterResourceInfoList(CharsManager._monstersConfigTable.rowList);
        foreach (CharacterResourceInfo _CharacterResourceInfo in characterList)
        {
            CharacterDataInfo _CharacterDataInfo = new CharacterDataInfo(
                _CharacterResourceInfo.charResouceNum, _CharacterResourceInfo.charResouceNum, new NineAndTwo()
            );
            Debug.Log("将角色" + _CharacterResourceInfo.prefabName + "加入了存档");
            addNewCharToJsonSaveData(_CharacterDataInfo);
        }
        overrideMyCharsInfoOnJsonFile();
    }

    public void overrideMyCharsInfoOnJsonFile()
    {
        string json = JsonConvert.SerializeObject(ownedChars);
        saveInfoToJsonFile("myownedCharsJson.json", json);
    }

    public static void addNewCharToJsonSaveData(CharacterDataInfo _CharacterDataInfo)
    {
        CharacterDataInfo[] myOwnedChars = AccountCharsSet.ownedChars;
        List<int> currentLocalIDList = new List<int>();
        if (myOwnedChars == null)
            myOwnedChars = new CharacterDataInfo[] { };

        foreach (CharacterDataInfo one in myOwnedChars)
        {
            currentLocalIDList.Add(one.localID);
        }

        currentLocalIDList.Sort((a, b) => AccountCharsSet.Instance.intCompare(a, b));

        List<CharacterDataInfo> newOwnedChars = myOwnedChars.ToList();

        int i = 0;
        for (i = 0; i < currentLocalIDList.Count; i++)
        {
            if (i + 1 < currentLocalIDList.Count)
            {
                if (currentLocalIDList[i + 1] - currentLocalIDList[i] > 1)
                {
                    _CharacterDataInfo.localID = currentLocalIDList[i] + 1;
                    newOwnedChars.Add(_CharacterDataInfo);
                    AccountCharsSet.ownedChars = newOwnedChars.ToArray();
                    //AccountCharsSet.Instance.overrideMyCharsInfoOnJsonFile();
                }
            }
        }

        if (currentLocalIDList.Count > 0)
            _CharacterDataInfo.localID = currentLocalIDList[currentLocalIDList.Count - 1] + 1;
        else
            _CharacterDataInfo.localID = 0;
        newOwnedChars.Add(_CharacterDataInfo);
        AccountCharsSet.ownedChars = newOwnedChars.ToArray();
        //AccountCharsSet.Instance.overrideMyCharsInfoOnJsonFile();
    }

    public void saveInfoToJsonFile(string subpathWithfilename, string json)
    {
        //string wholepath = Path.Combine(Application.persistentDataPath, subpath);
        string wholepath = Application.persistentDataPath + "/" + subpathWithfilename;
        File.WriteAllText(wholepath, json, System.Text.Encoding.UTF8);
    }
}
