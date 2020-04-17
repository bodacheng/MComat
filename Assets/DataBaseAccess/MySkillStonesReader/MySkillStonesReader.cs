using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using mainMenu;
using Api.Dto.Model;
using Skill;

// 配置文件属于资源信息，不是账户信息，应该分离开处理。
namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        static MySkillStonesReader instance;
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
        public static IDictionary<string, SkillStoneOfPlayerInfoModel> mySkillStonesDataDic = new Dictionary<string, SkillStoneOfPlayerInfoModel>();
        public static IDictionary<string, DragAndDropItem> mySkillStonesObjectsDic = new Dictionary<string, DragAndDropItem>();
        
        public static List<string> TargetStonesFromOfAccount(string type, int ExType, bool close, bool near, bool far, bool outrange)
        {
            List<string> SkillStonesOfTypeAndExType = new List<string>(); //技能石本地id
            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> keyValuePair in mySkillStonesDataDic)
            {
                SkillConfig _SkillConfigOfSkillStone = SkillConfigTable.GetSkillConfigByID(keyValuePair.Value.skillId);
                if (_SkillConfigOfSkillStone == null)
                {
                    Debug.Log("????"+ keyValuePair.Value.skillId);
                    continue;
                }
                if (_SkillConfigOfSkillStone.TYPE == type && (_SkillConfigOfSkillStone.SP_LEVEL == ExType || ExType == -1) &&
                    SkillConfigTable.RangeLimit(_SkillConfigOfSkillStone.AI_MIN_DIS,_SkillConfigOfSkillStone.AI_MAX_DIS,close, near, far, outrange))
                {
                    SkillStonesOfTypeAndExType.Add(keyValuePair.Value.skillStoneOfPlayerId);
                }
            }
            return SkillStonesOfTypeAndExType;
        }
        
        public SkillStoneOfPlayerInfoModel GetStoneOfPlayerInfoModelByMyStoneId(string id)
        {
            return id == null ? null : mySkillStonesDataDic.ContainsKey(id) ? mySkillStonesDataDic[id] : null;
        }

        public DragAndDropItem GetOneStoneModel(string localStoneid)
        {
            return mySkillStonesObjectsDic.ContainsKey(localStoneid) ? mySkillStonesObjectsDic[localStoneid] : null;
        }
        
        // 待补充
        public IEnumerator UpdateMySkillStone()
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    Instance.OverrideMySkillStoneInfosOnLocalFile(mySkillStonesDataDic.Values.ToList());//这个要改的。现在根本没有一个单独更新技能石的函数
                // 本地的技能石存档是一个文件，所以只能整体存
                break;
                case playerinfoReferenceMode.remoteTestPlayer:
                // 远程对技能石的更新操作是以技能石为单位的
                break;
                case playerinfoReferenceMode.formalVersion:
                
                break;
            }
            yield break;        
        }

        public IEnumerator LoadMySkillStones()
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    yield return LoadMySkillStonesViaLocalJsonFile();
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return LoadMySkillstonesRemote(ApiLanguage.JaJp);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }

            //IDictionary<string, List<string>> MonsterUsingStones = new Dictionary<string, List<string>>();
            //foreach (KeyValuePair<string, GetMonsterOfPlayerDetailModel> keyValuePair in AccountCharsSet.AccountCharacterInfoDictionary)
            //{
            //    List<string> usingStoneIds = RemoteAccess.getUsingStoneIDsOfAccountCharacter(keyValuePair.Value);
            //    MonsterUsingStones.Add(keyValuePair.Key, usingStoneIds);
            //}

            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> pair in mySkillStonesDataDic)
            {
                yield return SkillStonesBox.GenerateOneStoneModel(pair.Value.skillStoneOfPlayerId);
            }
            //yield return VerifyAllMyStonesUsingMonsterInfo();
            yield break;
        }
        
        // 获取某个角色装备中的技能石列表应该是在已经读取了玩家所有技能石之后，这个过程从本地内存读就可以。我们只需要确保读取技能石，和下面这个函数总实质是一前一后。
        public List<SkillStoneOfPlayerInfoModel> GetMonsterEquipingStones(string monsterOfPlayerId)
        {
            List<SkillStoneOfPlayerInfoModel> targetStones = new List<SkillStoneOfPlayerInfoModel>();
            foreach(KeyValuePair<string, SkillStoneOfPlayerInfoModel> keyValuePair in mySkillStonesDataDic)
            {
                if (keyValuePair.Value.inUsingMonsterOfPlayerId == monsterOfPlayerId)
                {
                    targetStones.Add(keyValuePair.Value);
                }
            }
            return targetStones;
        }

        public IEnumerator GenerateOneStoneInfo(SkillStoneOfPlayerInfoModel one)
        {
            SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(one.skillId);
            if (_SkillConfig == null)
            {
                Debug.Log("巨大问题,技能id似乎未定义：" + one.skillId);
                yield break;
            }
            if (MySkillStonesReader.mySkillStonesDataDic.ContainsKey(one.skillStoneOfPlayerId))
            {
                MySkillStonesReader.mySkillStonesDataDic[one.skillStoneOfPlayerId] = one;
            }
            else
            {
                MySkillStonesReader.mySkillStonesDataDic.Add(one.skillStoneOfPlayerId, one);
            }
        }

        // 这个函数的作用在于第一个英文单词：核实。 作用是根角色技能编辑存档和玩家技能石存档“相互”核实技能编辑信息。但不会造成技能石的丢失风险
        // 另一方面可以看到一个问题在新石头加入的时候显然不需要运行本函数
        //public IEnumerator VerifyAllMyStonesUsingMonsterInfo()
        //{
        //    IDictionary<string, string> usingstoneidandmonsterid = new Dictionary<string, string>();
        //    foreach(KeyValuePair<string, GetMonsterOfPlayerDetailModel> keyValuePair in AccountCharsSet.AccountCharacterInfoDictionary)
        //    {
        //        if (keyValuePair.Value.a1_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.a1_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.a1_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.a1_skill_stone_record_id);
        //                keyValuePair.Value.a1_skill_stone_record_id = "-1";
        //            }
        //        }
        //        if (keyValuePair.Value.a2_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.a2_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.a2_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.a2_skill_stone_record_id);
        //                keyValuePair.Value.a2_skill_stone_record_id = "-1";
        //            }
        //        }
        //        if (keyValuePair.Value.a3_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.a3_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.a3_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.a3_skill_stone_record_id);
        //                keyValuePair.Value.a3_skill_stone_record_id = "-1";
        //            }
        //        }

        //        if (keyValuePair.Value.b1_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.b1_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.b1_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.b1_skill_stone_record_id);
        //                keyValuePair.Value.b1_skill_stone_record_id = "-1";
        //            }
        //        }
        //        if (keyValuePair.Value.b2_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.b2_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.b2_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.b2_skill_stone_record_id);
        //                keyValuePair.Value.b2_skill_stone_record_id = "-1";
        //            }
        //        }
        //        if (keyValuePair.Value.b3_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.b3_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.b3_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.b3_skill_stone_record_id);
        //                keyValuePair.Value.b3_skill_stone_record_id = "-1";
        //            }
        //        }

        //        if (keyValuePair.Value.c1_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.c1_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.c1_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.c1_skill_stone_record_id);
        //                keyValuePair.Value.c1_skill_stone_record_id = "-1";
        //            }
        //        }
        //        if (keyValuePair.Value.c2_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.c2_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.c2_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.c2_skill_stone_record_id);
        //                keyValuePair.Value.c2_skill_stone_record_id = "-1";
        //            }
        //        }
        //        if (keyValuePair.Value.c3_skill_stone_record_id != null)
        //        {
        //            if (!usingstoneidandmonsterid.ContainsKey(keyValuePair.Value.c3_skill_stone_record_id))
        //                usingstoneidandmonsterid.Add(keyValuePair.Value.c3_skill_stone_record_id,keyValuePair.Value.monsterOfPlayerId);
        //            else{
        //                Debug.Log("致命错误。要么出现了复数个角色装备同一个技能石，要么一个角色在不同技能槽装备了同一石头.石头的id："+keyValuePair.Value.c3_skill_stone_record_id);
        //                keyValuePair.Value.c3_skill_stone_record_id = "-1";
        //            }
        //        }
        //    }
        //    foreach (KeyValuePair<string,string> keyValuePair in usingstoneidandmonsterid)
        //    {
        //        SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(keyValuePair.Key);
        //        skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = keyValuePair.Value;
        //    }
        //    yield break;
        //}

        public IEnumerator StoneGotcha()
        {
            switch (AccountSet.Instance._playerinfoReferenceMode)
            {
                case playerinfoReferenceMode.localTestSaveData:
                    break;
                case playerinfoReferenceMode.remoteTestPlayer:
                    yield return SkillStoneGotcha("POLI0000000000000002",ApiLanguage.JaJp);
                    yield return LoadMySkillstonesRemote(ApiLanguage.JaJp);
                    break;
                case playerinfoReferenceMode.formalVersion:
                    break;
            }
        }

        // 真正删除技能石头是要通过服务器的API
        // 而本地的操作与远程的财产操作是分开的，为了效率我们不希望本地的拥有技能石列表在每次更新后都通过读取数据库重新生成，
        // 所以走了一个if requeset ok，本地直接修改索引的过程。
        public static IEnumerator RemoveTheseStonesFromLocalDic(List<string> stoneSkillIDs)// 如此一来的话，参数里的这个列表是石头localid的列表。
        {
            List<SkillStoneOfPlayerInfoModel> toRemove = new List<SkillStoneOfPlayerInfoModel>();
            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> keyValuePair in mySkillStonesDataDic)
            {
                if (stoneSkillIDs.Contains(keyValuePair.Value.skillStoneOfPlayerId))
                {
                    toRemove.Add(keyValuePair.Value);
                }
            }
            for (int i = 0; i < toRemove.Count; i++)
            {
                if (mySkillStonesObjectsDic.ContainsKey(toRemove[i].skillStoneOfPlayerId))
                    Object.Destroy(mySkillStonesObjectsDic[toRemove[i].skillStoneOfPlayerId].gameObject);
                mySkillStonesObjectsDic.Remove(toRemove[i].skillStoneOfPlayerId);
                mySkillStonesDataDic.Remove(toRemove[i].skillStoneOfPlayerId);
            }
            yield break;
        }

        public static int SkillBalancePoint(string A1skillid, string A2skillid, string A3skillid,
                                                string B1skillid, string B2skillid, string B3skillid,
                                                    string C1skillid, string C2skillid, string C3skillid)
        {
            SkillConfig _SkillConfigA1 = SkillConfigTable.GetSkillConfigByID(A1skillid);
            SkillConfig _SkillConfigA2 = SkillConfigTable.GetSkillConfigByID(A2skillid);
            SkillConfig _SkillConfigA3 = SkillConfigTable.GetSkillConfigByID(A3skillid);
            SkillConfig _SkillConfigB1 = SkillConfigTable.GetSkillConfigByID(B1skillid);
            SkillConfig _SkillConfigB2 = SkillConfigTable.GetSkillConfigByID(B2skillid);
            SkillConfig _SkillConfigB3 = SkillConfigTable.GetSkillConfigByID(B3skillid);
            SkillConfig _SkillConfigC1 = SkillConfigTable.GetSkillConfigByID(C1skillid);
            SkillConfig _SkillConfigC2 = SkillConfigTable.GetSkillConfigByID(C2skillid);
            SkillConfig _SkillConfigC3 = SkillConfigTable.GetSkillConfigByID(C3skillid);
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
            for (int i = 0; i < allnineskill.Count; i++)
            {
                switch (allnineskill[i].SP_LEVEL)
                {
                    case 0:
                        wholeskillpoint += 10;
                        break;
                    case 1:
                        wholeskillpoint -= 10;
                        break;
                    case 2:
                        wholeskillpoint -= 20;
                        break;
                    case 3:
                        wholeskillpoint -= 30;
                        break;
                    case -1:
                        break;
                }
            }
            return wholeskillpoint;
        }
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