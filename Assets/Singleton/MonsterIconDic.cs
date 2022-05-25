using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public static class MonsterIconDic {

    public static Sprite readingSprite;
    static AssetBundle readingBundle;
    static readonly IDictionary<string, Sprite> Dic = new Dictionary<string, Sprite>();
    
    public static Sprite Get(string rId)
    {
        switch (ResourceLoadingSetting.IconLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                //coroutine = FindByCach(monsterId);
                //yield return coroutine;
                //yield return coroutine.Current;
                break;
            case ResourceLoadMode.Resource:
                return FindByResource(rId);
            case ResourceLoadMode.StreamingAssetAB:
                break;
        }
        return null;
    }
    
    static Sprite FindByResource(string unit_id)
    {
        Dic.TryGetValue(unit_id, out Sprite Sprite);
        if (Sprite == null)
        {
            var op = Addressables.LoadAssetAsync<Sprite>("unit/"+unit_id);
            Sprite = op.WaitForCompletion();
            Addressables.Release(op);
        }
        else
        {
            return Sprite;
        }
        DicAdd<string, Sprite>.Add(Dic, unit_id, Sprite);            
        return Sprite;
    }
}
