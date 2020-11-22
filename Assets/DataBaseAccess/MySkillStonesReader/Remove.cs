namespace dataAccess
{
    public partial class MySkillStonesReader
    {        
        // 删除一个技能石
        public static void RemoveStoneLocal(string stoneID)
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