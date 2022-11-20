using mainMenu;
using UnityEngine;

namespace dataAccess
{
    public static partial class Stones
    {
        // 删除一个技能石
        
        public static async void RemoveStoneLocal(string instanceId)
        {
            if (RenderModelDic.ContainsKey(instanceId))
            {
                var worldPos = PosCal.GetWorldPos(PreScene.target.mainC, RenderModelDic[instanceId].GetComponent<RectTransform>(), 0f);
                var path = FightGlobalSetting.EffectPathDefine();
                var slotEffect = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion3.prefab");
                slotEffect.gameObject.name = "stoneExplosion";
                slotEffect.gameObject.transform.position = worldPos;
                slotEffect.Play(true);
                
                Object.Destroy(RenderModelDic[instanceId].gameObject);
            }
            RenderModelDic.Remove(instanceId);
            Dic.Remove(instanceId);
        }
    }
}