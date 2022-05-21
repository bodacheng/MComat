using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    static IEnumerator FindByCach(string resource_id)
    {
        Dic.TryGetValue(resource_id, out readingSprite);
        if (readingSprite == null)
        {
            // IEnumerator ienObj = CachManager.Instance.getABFromCach("monsterIcons", resource_id.ToString());
            // while (ienObj.MoveNext())
            // {
            //     // Do Nothing
            // }
            // if (ienObj.Current != null)
            // {
            //     readingBundle = (UnityEngine.AssetBundle)ienObj.Current;
            // }
            // else
            // {
            //     Debug.Log("角色图标读取失败：" + resource_id);
            //     yield break;
            // }

            var resultObject = readingBundle.LoadAssetAsync<Image>(resource_id.ToString());
            yield return new WaitWhile(() => resultObject.isDone == false);

            if (resultObject.asset != null)
            {
                readingSprite = (Sprite)resultObject.asset;
                if (Dic.ContainsKey(resource_id))
                    Dic[resource_id] = readingSprite;
                else
                    Dic.Add(resource_id, readingSprite);

                Debug.Log("成功从缓存读取了以下图标：" + resource_id);
                readingBundle.Unload(false);
            }
            else
            {
                readingBundle.Unload(false);
                Debug.Log("图标提取失败"+ resource_id);
                yield break;
            }
        }
        yield return readingSprite;
    }

    static Sprite FindByResource(string monsterId)
    {
        Dic.TryGetValue(monsterId, out Sprite Sprite);
        if (Sprite == null)
            Sprite = Resources.Load<Sprite>("Sprites/monsterIcons/" + monsterId);
        else
        {
            return Sprite;
        }
        DicAdd<string, Sprite>.Add(Dic, monsterId, Sprite);            
        return Sprite;
    }
}
