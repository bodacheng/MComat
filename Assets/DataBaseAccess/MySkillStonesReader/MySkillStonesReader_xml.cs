using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public partial class MySkillStonesReader {

    //读取技能石操作以后必然都是协程，因为这个是直接访问数据库。我们现在并没有用这个。
    public IEnumerator loadMySkillStonesViaXMLLocalFile()
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            List<int> mySkillStones = loadMySkillStonesViaXMLLocalFile("/Resources/Account/MySkillStones.xml").ToList();
            mySkillStonesDicByType = convertSKillStoneNumListToDic(mySkillStones);
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer
                 ||
                Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            List<int> mySkillStones = loadMySkillStonesViaXMLLocalFile("Account/MySkillStones").ToList();
            mySkillStonesDicByType = convertSKillStoneNumListToDic(mySkillStones);
        }
        yield return mySkillStonesDicByType;
        yield break;
    }

    public int[] loadMySkillStonesViaXMLLocalFile(string accountInfoPath)
    {
        FileStream FileStream = null;
        int[] info = new int[] { };
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(int[]));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                FileStream = new FileStream(Application.dataPath + accountInfoPath, FileMode.Open);
                info = XmlSerializer.Deserialize(FileStream) as int[];
                FileStream.Close();
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
                TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
                var reader = new System.IO.StringReader(xmlData.text);
                info = XmlSerializer.Deserialize(reader) as int[];
                Debug.Log("玩家拥有技能石信息读取成功");
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
                TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
                var reader = new System.IO.StringReader(xmlData.text);
                info = XmlSerializer.Deserialize(reader) as int[];
                Debug.Log("玩家拥有技能石信息读取成功");
            }
            // 那么也就是说每次程序启动，我们为玩家所拥有的所有角色添加的这个key其实都是临时给加的，方便本地索引。这么做有无风险？
            return info;
        }
        catch (Exception e)
        {
            Debug.Log("玩家拥有技能石信息读取失败，建立新技能石头本地测试文件");
            List<int> skillStonesForTest = new List<int>();
            foreach (KeyValuePair<int, SkillConfig> _keyValuePair in MySkillStonesReader.SkillConfigDicForReference)
            {
                skillStonesForTest.Add(_keyValuePair.Value.id);
            }

            overrideMySkillStonesInfoOnXMLLocalFile("/Resources/Account/MySkillStones.xml", skillStonesForTest.ToArray());
            Debug.Log(e.ToString());
            if (FileStream != null)
                FileStream.Close();
            return info;
        }
    }

    public static void overrideMySkillStoneInfosOnXMLLocalFile()
    {
        List<int> stoneIDs = new List<int>();
        foreach (KeyValuePair<string, List<int>> _keyValuePair in mySkillStonesDicByType)
        {
            for (int i = 0; i < _keyValuePair.Value.Count; i++)
            {
                stoneIDs.Add(_keyValuePair.Value[i]);
            }
        }
        overrideMySkillStonesInfoOnXMLLocalFile("/Resources/Account/MySkillStones.xml", stoneIDs.ToArray());
    }

    public static bool overrideMySkillStonesInfoOnXMLLocalFile(string accountInfoPath, int[] skillstones)
    {
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(int[]));
            FileStream FileStream = new FileStream(Application.dataPath + accountInfoPath, FileMode.Create);
            List<int> mySkillStones = skillstones.ToList();
            XmlSerializer.Serialize(FileStream, mySkillStones.ToArray());
            Debug.Log(accountInfoPath + " saved" + " 玩家技能石信息保存成功");
            FileStream.Close();
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("玩家技能石信息保存失败");
            Debug.Log(e.ToString());
            return false;
        }
    }

    public IEnumerator deleteOneStoneFromLocalFile(int SkillStoneId)
    {
        List<int> mySkillStones = new List<int>();
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            mySkillStones = loadMySkillStonesViaXMLLocalFile("/Resources/Account/MySkillStones" + ".xml").ToList();
            //mySkillStonesDicByType = convertSKillStoneNumListToDic(mySkillStones);
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer
                 ||
                Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            mySkillStones = loadMySkillStonesViaXMLLocalFile("Account/MySkillStones").ToList();
        }

        if (mySkillStones.Contains(SkillStoneId))
        {
            mySkillStones.Remove(SkillStoneId);
        }
        else
        {
            Debug.Log("账户里没有欲删除的石头");
        }
        yield break;
    }

    public void addNewSkillStoneToDebugAccountFile(int SkillStoneId)
    {
        SkillConfig skillConfig = MySkillStonesReader.getSkillConfigByID(SkillStoneId);
        if (skillConfig != null)
        {
            if (MySkillStonesReader.mySkillStonesDicByType.ContainsKey(skillConfig.type))
            {
                MySkillStonesReader.mySkillStonesDicByType[skillConfig.type].Add(SkillStoneId);
            }
            else
            {
                MySkillStonesReader.mySkillStonesDicByType.Add(skillConfig.type, new List<int>(SkillStoneId));
            }
            MySkillStonesReader.overrideMySkillStoneInfosOnXMLLocalFile();
        }
        else
            Debug.Log("寻找不到技能定义");
    }
}
