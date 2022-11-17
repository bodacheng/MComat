namespace dataAccess
{
    public static partial class Stones
    {
        // 删除一个技能石
        public static void RemoveStoneLocal(string instanceId)
        {
            if (RenderModelDic.ContainsKey(instanceId))
            {
                UnityEngine.Object.Destroy(RenderModelDic[instanceId].gameObject);
            }
            RenderModelDic.Remove(instanceId);
            Dic.Remove(instanceId);
        }
    }
}