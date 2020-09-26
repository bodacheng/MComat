using System.Collections;
using System.Collections.Generic;
using Api.Dto.Model;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
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
                RemoveStone(toRemove[i].skillStoneOfPlayerId);
            }
            yield break;
        }
        
        // 删除一个技能石
        public static void RemoveStone(string stoneID)
        {
            if (RenderModelDic.ContainsKey(stoneID))
            {
                UnityEngine.Object.Destroy(RenderModelDic[stoneID].gameObject);
            }
            RenderModelDic.Remove(stoneID);
            Dic.Remove(stoneID);
        }
    }
}