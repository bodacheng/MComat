using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using Skill;

// 配置文件属于资源信息，不是账户信息，应该分离开处理。
namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        public static IDictionary<string, SkillStoneOfPlayerInfoModel> Dic = new Dictionary<string, SkillStoneOfPlayerInfoModel>();
        public static IDictionary<string, SKStoneItem> RenderModelDic = new Dictionary<string, SKStoneItem>();
        
        public static SkillStoneOfPlayerInfoModel Get(string id)
        {
            return id == null ? null : Dic.ContainsKey(id) ? Dic[id] : null;
        }
        
        public static SKStoneItem GetRenderModel(string localStoneid)
        {
            return RenderModelDic.ContainsKey(localStoneid) ? RenderModelDic[localStoneid] : null;
        }
        
        public static IEnumerator Add(SkillStoneOfPlayerInfoModel one)
        {
            DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
            yield return Update(one.skillStoneOfPlayerId);
        }
        
        public static IEnumerator Update(string stoneOfPlayerID)
        {
            if (!Dic.ContainsKey(stoneOfPlayerID) || Dic[stoneOfPlayerID] == null)
            {
                Debug.Log("更新对象技能石不存在。stoneOfPlayerID :" + stoneOfPlayerID);
                yield break;
            }
            switch (AccountSet._playerinfoReferenceMode)
            {
                case playerInfoRefMode.localTestSaveData:
                    Update_Json(Dic[stoneOfPlayerID]);
                break;
                case playerInfoRefMode.remoteTestPlayer:
                break;
                case playerInfoRefMode.formalVersion:
                break;
            }
            yield break;
        }
        
        public static IEnumerator Update_Level(string skillstoneofplayerid, string targetLevel, ApiLanguage apiLanguage)
        {
            //SkillStoneOfPlayerInfoModel st = Get(skillstoneofplayerid);
            //st.level = targetLevel;
            //IEnumerator up = Update(skillstoneofplayerid);
            //yield return up;
            yield break;
        }
        
        public static IEnumerator LoadAll()
        {
            Dic.Clear();
            switch (AccountSet._playerinfoReferenceMode)
            {
                case playerInfoRefMode.localTestSaveData:
                    LoadAll_Json();
                    break;
                case playerInfoRefMode.remoteTestPlayer:
                    yield return LoadMySkillstonesRemote(ApiLanguage.JaJp);
                    break;
                case playerInfoRefMode.formalVersion:
                    break;
            }
            // 上面的步骤已经完成了Dic的适配
            RenderModelDic.Clear();
            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> pair in Dic)
            {
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(pair.Value.skillId);
                if (_SkillConfig == null)
                {
                    Debug.Log("巨大问题,技能id似乎未定义：" + pair.Value.skillId);
                    yield break;
                }
                yield return SkillStonesBox.GenerateStoneModelByAccID(pair.Value.skillStoneOfPlayerId);
            }
            yield break;
        }
        
        // 真正删除技能石头是要通过服务器的API
        // 而本地的操作与远程的财产操作是分开的，为了效率我们不希望本地的拥有技能石列表在每次更新后都通过读取数据库重新生成，
        // 所以走了一个if requeset ok，本地直接修改索引的过程。
        public static IEnumerator RemoveTheseStonesFromLocalDic(List<string> stoneSkillIDs)// 如此一来的话，参数里的这个列表是石头localid的列表。
        {
            List<SkillStoneOfPlayerInfoModel> toRemove = new List<SkillStoneOfPlayerInfoModel>();
            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> keyValuePair in Dic)
            {
                if (stoneSkillIDs.Contains(keyValuePair.Value.skillStoneOfPlayerId))
                {
                    toRemove.Add(keyValuePair.Value);
                }
            }
            for (int i = 0; i < toRemove.Count; i++)
            {
                if (RenderModelDic.ContainsKey(toRemove[i].skillStoneOfPlayerId))
                    Object.Destroy(RenderModelDic[toRemove[i].skillStoneOfPlayerId].gameObject);
                RenderModelDic.Remove(toRemove[i].skillStoneOfPlayerId);
                Dic.Remove(toRemove[i].skillStoneOfPlayerId);
            }
            yield break;
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