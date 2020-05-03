using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EffectsManager
{
    public static DecompositionerPool IniEffectsPool(string resource_name, string EffectsPath, int object_count)
    {
        EffectPrefab = null;
        EffectPool = null;
        if (EffectsPath != null)
        {
            if (EffectPoolsDic.ContainsKey("Effects/" + EffectsPath + "/" + resource_name))
            {
                EffectPoolsDic.TryGetValue("Effects/" + EffectsPath + "/" + resource_name, out EffectPool);
                if (EffectPool != null)
                    return EffectPool;
            }

            EffectPrefab = Resources.Load("Effects/" + EffectsPath + "/" + resource_name, typeof(GameObject)) as GameObject;
            if (EffectPrefab != null)
            {
                EffectPool = ConstructEffectPoolWithPrefabAndKey(EffectPrefab, "Effects/" + EffectsPath + "/" + resource_name,object_count);
                return EffectPool;
            }
            if (EffectsPath == "defaultmagic")
            {
                return null;//防止无限循环
            }
        }
        EffectPool = IniEffectsPool(resource_name, "defaultmagic", object_count);
        return EffectPool;
    }
}
