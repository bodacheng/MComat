using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZObjectPools;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class SkillIDsAndNames
{
    public int[] IDs;
    public string[] SkillNames;

    public SkillIDsAndNames(int[] IDs, string[] SkillNames)
    {
        this.IDs = IDs;
        this.SkillNames = SkillNames;
    }
}

public partial class MySkillStonesReader {
    private static MySkillStonesReader instance;
    public static MySkillStonesReader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MySkillStonesReader();
            }
            return instance;
        }
    }

    public static SkillConfigTable skillConfigTable = new SkillConfigTable();
    public static IDictionary<int, SkillConfig> SkillConfigDicForReference;
    //mySkillStonesDicByType 仅仅是从数据库读取了我拥有的全部石头后，为了本地处理便利而转成的一个临时索引，
    // 在有技能石删除或添加的情况下，为了处理速度我们没有必要让它重新根据数据库信息来全部生成，只需要直接编辑
    // 但这样的操作则必须在服务器返回数据表更新成功的情况下执行，否则一旦数据库并没有成功更新，这个本地索引确更新了，
    // 则会产生了不一致的问题。
    // 更新数据库的API必须要基于实际财产信息。
    public static IDictionary<string, List<int>> mySkillStonesDicByType = new Dictionary<string, List<int>>();

    public IEnumerator loadMySkillStones()
    {
        switch (AccountSet.Instance._playerinfoReferenceMode)
        {
            case playerinfoReferenceMode.localTestSaveData:
                //yield return loadMySkillstonesRemote(1);
                yield return loadMySkillStonesViaXMLLocalFile();
                break;
            case playerinfoReferenceMode.remoteTestPlayer:
                yield return loadMySkillstonesRemote(1);
                break;
            case playerinfoReferenceMode.formalVersion:
                break;
        }
        yield break;
    }

    public IEnumerator saveMySkillStones()
    {
        switch (AccountSet.Instance._playerinfoReferenceMode)
        {
            case playerinfoReferenceMode.localTestSaveData:
                overrideMySkillStoneInfosOnXMLLocalFile();
                break;
            case playerinfoReferenceMode.remoteTestPlayer:
                break;
            case playerinfoReferenceMode.formalVersion:
                break;
        }
        yield break;
    }

    public static void RemoveTheseStonesFromLocalDic(List<int> stoneSkillIDs)
    {
        foreach (int skillid in stoneSkillIDs)
        {
            SkillConfig _SkillConfig = getSkillConfigByID(skillid);
            if (mySkillStonesDicByType.ContainsKey(_SkillConfig.type))
                mySkillStonesDicByType[_SkillConfig.type].Remove(skillid);
            else
                Debug.Log("严重bug。试图删除不存在的技能石头");
        }
    }

    private IDictionary<string, List<int>> convertSKillStoneNumListToDic(List<int> mystones)
    {
        IDictionary<string, List<int>> Dic = new Dictionary<string, List<int>>();
        for (int i = 0; i < mystones.Count; i++)
        {
            SkillConfig _SkillConfig = getSkillConfigByID(mystones[i]);
            if (_SkillConfig == null)
            {
                Debug.Log("巨大问题");
                continue;
            }
            if (Dic.ContainsKey(_SkillConfig.type))
            {
                if (Dic[_SkillConfig.type] != null)
                {
                    Dic[_SkillConfig.type].Add(mystones[i]);
                }
                else
                    Dic[_SkillConfig.type] = new List<int>(mystones[i]);
            }
            else
            {
                Dic.Add(new KeyValuePair<string, List<int>>(_SkillConfig.type, new List<int>(mystones[i])));
            }
        }
        return Dic;
    }

    public static SkillConfig getSkillConfigByID(int ID)//注意，角色存档charDatainfo中也有记录角色技能的SkillConfig成员，但那里面的信息不完整（只有ID没名字等等）
    {
        if (MySkillStonesReader.SkillConfigDicForReference != null)
        {
            SkillConfig skillConfig = null;
            MySkillStonesReader.SkillConfigDicForReference.TryGetValue(ID, out skillConfig);
            return skillConfig;
        }
        else
        {
            Debug.Log("技能字典未初始化");
        }
        return null;
    }

    public static SkillIDsAndNames getSkillIDAndNameArray(string type, bool[] ranges, int rarelevel)// close, near, far.rarelevel = -1代表全部，0代表无星级技能
    {
        List<SkillConfig> list = getSkillConfigsOfType(type);
        List<int> IDs = new List<int>();
        List<string> KeyNames = new List<string>();

        foreach (SkillConfig one in list)
        {
            if (one.rangeLimit(ranges[0], ranges[1], ranges[2],ranges[3]) && (one.rarelevel == rarelevel|| rarelevel == -1))
            {
                IDs.Add(one.id);
                KeyNames.Add(one.keyName);
            }
        }

        IDs.Add(-1);
        KeyNames.Add("null");

        SkillIDsAndNames _SkillIDsAndNames = new SkillIDsAndNames(IDs.ToArray(), KeyNames.ToArray());
        return _SkillIDsAndNames;
    }

    public static List<SkillConfig> getSkillConfigsOfType(string type) // 2
    {
        List<SkillConfig> SkillConfigsOfType = new List<SkillConfig>();
        if (MySkillStonesReader.SkillConfigDicForReference == null)
        {
            return null;
        }
        foreach (KeyValuePair<int, SkillConfig> one in MySkillStonesReader.SkillConfigDicForReference)
        {
            if (one.Value.type == type)
                SkillConfigsOfType.Add(one.Value);
        }
        return SkillConfigsOfType;
    }

    public static void loadAllSkillConfigFromLocalConfigFile()//1
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor
            ||
            Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            TextAsset csv = Resources.Load("Account/skillsConfig") as TextAsset;
            if (csv != null)
                skillConfigTable.Load(csv);
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            TextAsset csv = Resources.Load("Account/skillsConfig") as TextAsset;//未定，这个瞎写的。
            if (csv != null)
                skillConfigTable.Load(csv);
        }
    }
    
    public static void refreshSkillConfigDicForReference()
    {
        MySkillStonesReader.SkillConfigDicForReference = skillConfigTable.getSkillConfigDic();
    }

    public static int skillsetValidation(int A1skillid,int A2skillid,int A3skillid,
                                            int B1skillid,int B2skillid,int B3skillid,
                                            int C1skillid,int C2skillid,int C3skillid)
    {
        SkillConfig _SkillConfigA1 = getSkillConfigByID(A1skillid);
        SkillConfig _SkillConfigA2 = getSkillConfigByID(A2skillid);
        SkillConfig _SkillConfigA3 = getSkillConfigByID(A3skillid);
        SkillConfig _SkillConfigB1 = getSkillConfigByID(B1skillid);
        SkillConfig _SkillConfigB2 = getSkillConfigByID(B2skillid);
        SkillConfig _SkillConfigB3 = getSkillConfigByID(B3skillid);
        SkillConfig _SkillConfigC1 = getSkillConfigByID(C1skillid);
        SkillConfig _SkillConfigC2 = getSkillConfigByID(C2skillid);
        SkillConfig _SkillConfigC3 = getSkillConfigByID(C3skillid);
        List<SkillConfig> allnineskill = new List<SkillConfig>();
        
        if (_SkillConfigA1 != null)
            allnineskill.Add(_SkillConfigA1);
        if (_SkillConfigA2 != null)
            allnineskill.Add(_SkillConfigA2);
        if (_SkillConfigA3 != null)
            allnineskill.Add(_SkillConfigA3);
        if (_SkillConfigB1 != null)
            allnineskill.Add(_SkillConfigB1);
        if (_SkillConfigB2 != null)
            allnineskill.Add(_SkillConfigB2);
        if (_SkillConfigB3 != null)
            allnineskill.Add(_SkillConfigB3);
        if (_SkillConfigC1 != null)
            allnineskill.Add(_SkillConfigC1);
        if (_SkillConfigC2 != null)
            allnineskill.Add(_SkillConfigC2);
        if (_SkillConfigC3 != null)
            allnineskill.Add(_SkillConfigC3);

        int wholeskillpoint = 0;
        for (int i = 0; i < allnineskill.Count;i++)
        {
            switch(allnineskill[i].SPLevel)
            {
                case EX.normal:
                    wholeskillpoint += 10;
                    break;
                case EX.EX1:
                    wholeskillpoint -= 10;
                    break;
                case EX.EX2:
                    wholeskillpoint -= 20;
                    break;
                case EX.EX3:
                    wholeskillpoint -= 30;
                    break;
                case EX.NULL:
                    break;
                default:
                    break;
            }
        }
        return wholeskillpoint;
    }
}

//曾经的XML技能配置文件
//public IDictionary<int, SkillConfig> loadAllSkillConfigFromConfigFile(string accountInfoPath)//假设到时候要是全部从配置文件读取这个信息，那这方面东西写成同步函数应该也不是太大的问题。但这个信息原则上要一直在程序内。
//{
//    try
//    {
//        List<SkillConfig> list = new List<SkillConfig>();
//        //Debug.Log("开始尝试读取技能列表");
//        XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<SkillConfig>));
//        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
//        {
//            FileStream FileStream = new FileStream(Application.dataPath + accountInfoPath, FileMode.Open);
//            list = XmlSerializer.Deserialize(FileStream) as List<SkillConfig>;
//            FileStream.Close();
//        }
//        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
//        {
//            accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
//            TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
//            XmlSerializer = new XmlSerializer(typeof(List<SkillConfig>));
//            var reader = new System.IO.StringReader(xmlData.text);
//            list = XmlSerializer.Deserialize(reader) as List<SkillConfig>;
//            //Debug.Log("技能适配信息读取成功");
//        }
//        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
//        {
//            accountInfoPath = accountInfoPath.Replace(Environment.NewLine, "");
//            TextAsset xmlData = Resources.Load(accountInfoPath) as TextAsset;
//            XmlSerializer = new XmlSerializer(typeof(List<SkillConfig>));
//            var reader = new System.IO.StringReader(xmlData.text);
//            list = XmlSerializer.Deserialize(reader) as List<SkillConfig>;
//            //Debug.Log("技能适配信息读取成功");
//        }

//        // 那么也就是说每次程序启动，我们为玩家所拥有的所有角色添加的这个key其实都是临时给加的，方便本地索引。这么做有无风险？
//        MySkillStonesReader.SkillConfigDicForReference = new Dictionary<int, SkillConfig>();
//        foreach (SkillConfig _SkillConfig in list)
//        {
//            if (!MySkillStonesReader.SkillConfigDicForReference.ContainsKey(_SkillConfig.id))
//            {
//                MySkillStonesReader.SkillConfigDicForReference.Add(_SkillConfig.id, _SkillConfig);
//            }
//        }
//        return MySkillStonesReader.SkillConfigDicForReference;
//    }
//    catch (Exception e)
//    {
//        Debug.Log("技能总列表读取失败");
//        Debug.Log(e.ToString());
//        return null;
//    }
//}