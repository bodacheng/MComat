using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.IO;

// localFight这个类描述的是一场战斗本身
// 这样的话这个类包括的信息应该有这些：位置1的角色是什么类型，什么角色，什么AI脚本，什么等级，
// 位置4.。。当然这个敌人数量还不一定。。比较犹豫的一个地方就是关于角色AI脚本和等级的表。其实这个类可能在后面还会牵扯到
// 和一个比较直观的关卡编辑器进行联动什么的。这样的话尤其像脚本等级这种东西，可能需要在这个类里需要一个脚本等级的值，然后由一些别的环节负责把等级转成
// 相应经验值，因为我们现在AI信息里对脚本等级这个东西的设置比较隐晦
// 另外这个。。。锁死了游戏只有这样一种模式。如果要搞一些敌人会不停出现的模式的话这个类就需要大改。

[System.Serializable]
public class LocalFight
{
    public int BattleGroundID = 0;

    [System.NonSerialized]
    public MultiDictionary<int, int, CharacterDataInfo> HeroSets = new MultiDictionary<int, int, CharacterDataInfo>();
    public MultiDictionary<int, int, CharacterDataInfo> EnemySets = new MultiDictionary<int, int, CharacterDataInfo>();
        
    //public positionLocalCharKeySet4V4Mode _positionLocalCharKeySet4V4Mode;// 这个变量的另外一个存在地点是玩家存档，用以代表，保存所设置的角色在战斗中的站位信息，
    //在敌人队伍的战斗适配信息中这个东西和玩家那边是保持一致。为了保证站位正确，首先LocalFight中Enemies的localid要保证不重复，并且要求_positionLocalCharKeySet4V4Mode也不出错
    //其实这个东西可能有点。。让未来战斗编辑器的编写麻烦了些，但这个信息存在的形式本身没什么问题。必须走一个先决定有哪些角色再摆位置的过程。
    // 6.27日这个信息被删除了，原因如下：权衡了大量问题我们决定把战斗信息中规中矩保存到数据库，而这个站位信息的存在让数据库变得极其烦冗，其实本地角色的localID就可以处理站位问题
    // 玩家角色的站位信息里仍然有是这个信息不会进数据库，而是一个本地文件，每次客户端启动这个本地文件会来与玩家存档进行一个校对。

    public LocalFight()
    {
    }

    //现在的localFight模块里包含了一个在localID不连续情况下强制将其扭转为从0开始连续自然数的过程。
    public LocalFight(int BattleGroundID,CharacterDataInfo[] Enemies)
    {
        this.BattleGroundID = BattleGroundID;
        this.HeroSets = new MultiDictionary<int, int, CharacterDataInfo>();
        this.EnemySets = new MultiDictionary<int, int, CharacterDataInfo>();
    }
    
    public static LocalFight loadOneLocalFightByScript(TextAsset Script)
    {
        LocalFight _LocalFight;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LocalFight));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    _LocalFight = serializer.Deserialize(textReader) as LocalFight;
                }
            }
            else
            {
                var reader = new System.IO.StringReader(Script.text);
                _LocalFight = serializer.Deserialize(reader) as LocalFight;
            }
            _LocalFight.EnemySets.ConvertSerializableArrayToDictionary();
            _LocalFight.HeroSets.ConvertSerializableArrayToDictionary();
            return _LocalFight;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }

    //public void generate4V4FightPosSet()
    //{
    //    List<PosNumWithLocalKey> PosNumsWithLocalKeyList = new List<PosNumWithLocalKey>();
    //    for (int i = 0; i < Enemies.Length; i++)
    //    {
    //        Enemies[i].localID = i;
    //        PosNumsWithLocalKeyList.Add(new PosNumWithLocalKey(i, Enemies[i].localID));
    //    }
    //    _positionLocalCharKeySet4V4Mode = new positionLocalCharKeySet4V4Mode(PosNumsWithLocalKeyList.ToArray());
    //}

    //_positionLocalCharKeySet4V4Mode的内容和本地各个角色的localid是不是匹配非常重要，我们建立一个检查兼自我修复的函数
    //public void checkAndFixLocalFight4V4ModePosInfo()
    //{
    //    List<int> localIDs = new List<int>();
    //    foreach (CharacterDataInfo _CharacterDataInfo in Enemies)
    //    {
    //        if (!localIDs.Contains(_CharacterDataInfo.localID))
    //        {
    //            localIDs.Add(_CharacterDataInfo.localID);
    //        }else{
    //            Debug.Log("localfight信息存在严重问，角色出现重复的localid，尝试进行修复");
    //            int newLocalID = _CharacterDataInfo.localID;
    //            while (!localIDs.Contains(newLocalID))
    //            {
    //                newLocalID += 1;
    //            }
    //            _CharacterDataInfo.localID = newLocalID;
    //            localIDs.Add(_CharacterDataInfo.localID);
    //            Debug.Log("角色新id为："+ _CharacterDataInfo.localID);
    //        }
    //    }

    //    bool hasBug = false;
    //    _positionLocalCharKeySet4V4Mode.refreshDic();
    //    List<int> PosNums = new List<int>(){0,1,2,3};//4v4模式默认就是这四个编码
    //    foreach (PosNumWithLocalKey _PosNumWithLocalKey in _positionLocalCharKeySet4V4Mode.PosNumsWithLocalKey)
    //    {
    //        if (PosNums.Contains(_PosNumWithLocalKey.posNum))
    //        {
    //            if(!localIDs.Contains(_PosNumWithLocalKey.LocalID))
    //            {
    //                Debug.Log("localfight信息存在严重问题，队伍阵容出现了不存在的localid");
    //                hasBug = true;
    //            }
    //        }else{
    //            Debug.Log("localfight信息存在严重问题，出现了0，1，2，3之外的位置号码");
    //            hasBug = true;
    //        }
    //    }
    //    if (hasBug)//这个地方如果有bug的存在那说明的这个队伍配置需要的是直接基于队伍人员而重新生成
    //    {
    //        generate4V4FightPosSet();
    //    }
    //}
}