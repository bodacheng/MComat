using UnityEngine;

public partial class EffectsManager
{
    public static DecompositionerPool INIEffectsPool(string resource_name, string EffectsPath, int object_count)
    {
        DecompositionerPool EffectPool = null;
        if (EffectsPath != null)
        {
            if (EffectPoolsDic.ContainsKey("Effects/" + EffectsPath + "/" + resource_name))
            {
                EffectPoolsDic.TryGetValue("Effects/" + EffectsPath + "/" + resource_name, out EffectPool);
                if (EffectPool != null)
                    return EffectPool;
            }
            
            var EffectPrefab = Resources.Load("Effects/" + EffectsPath + "/" + resource_name, typeof(GameObject)) as GameObject;
            if (EffectPrefab != null)
            {
                EffectPool = ConstructEffectPoolWithPrefabAndKey(EffectPrefab, "Effects/" + EffectsPath + "/" + resource_name,object_count);
                return EffectPool;
            }
            if (EffectsPath == FightGlobalSetting.EffectPathDefine(Zokusei.Null))
            {
                return null;//防止无限循环
            }
        }
        EffectPool = INIEffectsPool(resource_name, FightGlobalSetting.EffectPathDefine(Zokusei.Null), object_count);
        return EffectPool;
    }
}
