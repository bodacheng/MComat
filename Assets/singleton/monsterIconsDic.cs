using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class monsterIconsDic {

    private static monsterIconsDic instance;
    public static monsterIconsDic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new monsterIconsDic();
            }
            return instance;
        }
    }

    public Sprite readingSprite;
    private AssetBundle readingBundle;
    private IDictionary<string, Sprite> characterIconDic = new Dictionary<string, Sprite>();

    public Sprite getMonsterIconSyn(string monsterid)
    {
        readingSprite = null;
        characterIconDic.TryGetValue(monsterid,out readingSprite);
        if (readingSprite == null)
            Debug.Log("没有找到对应角色的icon，monsterid："+monsterid);
        return readingSprite;
    }

    public IEnumerator findMonsterIconByCach(string resource_id)
    {
        characterIconDic.TryGetValue(resource_id, out readingSprite);
        if (readingSprite == null)
        {
            IEnumerator ienObj = CachManager.Instance.getABFromCach("monsterIcons", resource_id.ToString());
            while (ienObj.MoveNext())
            {
                // Do Nothing
            }
            if (ienObj.Current != null)
            {
                readingBundle = (UnityEngine.AssetBundle)ienObj.Current;
            }
            else
            {
                Debug.Log("角色图标读取失败：" + resource_id);
                yield break;
            }

            var resultObject = readingBundle.LoadAssetAsync<Image>(resource_id.ToString());
            yield return new WaitWhile(() => resultObject.isDone == false);

            if (resultObject.asset != null)
            {
                readingSprite = (Sprite)resultObject.asset;
                if (characterIconDic.ContainsKey(resource_id))
                    characterIconDic[resource_id] = readingSprite;
                else
                    characterIconDic.Add(resource_id, readingSprite);

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

    public IEnumerator findMonsterIconByResource(string resource_id)
    {
        characterIconDic.TryGetValue(resource_id, out readingSprite);
        if (readingSprite == null)
            readingSprite = Resources.Load<Sprite>("Sprites/monsterIcons/" + resource_id.ToString()) as Sprite;
        else
            yield return readingSprite;

        if (characterIconDic.ContainsKey(resource_id))
            characterIconDic[resource_id] = readingSprite;
        else
            characterIconDic.Add(resource_id, readingSprite);
            
        yield return readingSprite;
    }
}
