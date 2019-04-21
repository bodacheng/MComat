using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class skillIconsDic {

    private static skillIconsDic instance;
    public static skillIconsDic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new skillIconsDic();
            }
            return instance;
        }
    }

    private Sprite readingSprite;
    private IDictionary<int, Sprite> skillIconDic = new Dictionary<int, Sprite>();

    public Sprite getSkillIconSyn(int skillID)
    {
        skillIconDic.TryGetValue(skillID, out readingSprite);
        return readingSprite;
    }

    public IEnumerator findSkillIconByCach(int skillID)
    {
        skillIconDic.TryGetValue(skillID, out readingSprite);
        if (readingSprite == null)
        {
            IEnumerator ienObj = defaultPools.Instance.getABFromCach("skillIcons", skillID.ToString());
            while (ienObj.MoveNext())
            {
                // Do Nothing
            }
            AssetBundle readingBundle = null;
            if (ienObj.Current != null)
            {
                readingBundle = (UnityEngine.AssetBundle)ienObj.Current;
            }
            else
            {
                Debug.Log("技能图标读取失败：" + skillID);
                yield break;
            }

            var resultObject = readingBundle.LoadAssetAsync<Image>(skillID.ToString());
            yield return new WaitWhile(() => resultObject.isDone == false);

            if (resultObject.asset != null)
            {
                readingSprite = (Sprite)resultObject.asset;
                if (skillIconDic.ContainsKey(skillID))
                    skillIconDic[skillID] = readingSprite;
                else
                    skillIconDic.Add(skillID, readingSprite);

                Debug.Log("成功从缓存读取了以下图标：" + skillID);
                readingBundle.Unload(false);
            }
            else
            {
                readingBundle.Unload(false);
                Debug.Log("图标提取失败"+ skillID);
                yield break;
            }
        }
        yield return readingSprite;
    }

    public IEnumerator findSkillIconByResource(int skillID)
    {
        skillIconDic.TryGetValue(skillID, out readingSprite);
        if (readingSprite == null)
            readingSprite = Resources.Load<Sprite>("Sprites/skillIcons/" + skillID.ToString()) as Sprite;
        else
            yield return readingSprite;

        if (skillIconDic.ContainsKey(skillID))
            skillIconDic[skillID] = readingSprite;
        else
            skillIconDic.Add(skillID, readingSprite);
            
        yield return readingSprite;
    }

    public Sprite getDefaultSkillIconByResource(EX spLevel)
    {
        switch (spLevel)
        {
            case EX.normal:
                return Resources.Load<Sprite>("Sprites/skillIcons/" + "normal_default") as Sprite;
            case EX.EX1:
                return Resources.Load<Sprite>("Sprites/skillIcons/" + "ex1_default") as Sprite;
            case EX.EX2:
                return Resources.Load<Sprite>("Sprites/skillIcons/" + "ex2_default") as Sprite;
            case EX.EX3:
                return Resources.Load<Sprite>("Sprites/skillIcons/" + "ex3_default") as Sprite;
        }
        return null;
    }
}
