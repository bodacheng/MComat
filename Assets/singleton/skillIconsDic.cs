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

    private GameObject readingSprite;
    private IDictionary<string, GameObject> skillIconDic = new Dictionary<string, GameObject>();

    public GameObject getSkillIconSyn(string skillID)
    {
        skillIconDic.TryGetValue(skillID, out readingSprite);
        return readingSprite;
    }

    public IEnumerator findSkillIconByCach(string skillID)
    {
        skillIconDic.TryGetValue(skillID, out readingSprite);
        if (readingSprite == null)
        {
            IEnumerator ienObj = CachManager.Instance.getABFromCach("skillIcons", skillID.ToString());
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
                GameObject pretab = (GameObject)resultObject.asset;
                if (pretab != null)
                    readingSprite = GameObject.Instantiate(pretab) as GameObject;
                else
                {
                    yield return null; yield break;
                }
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

    public IEnumerator findSkillIconByResource(string skillID)
    {
        skillIconDic.TryGetValue(skillID, out readingSprite);
        if (readingSprite != null)
            yield return readingSprite;
           
        GameObject pretab = Resources.Load("Sprites/skillIcons/" + skillID.ToString()) as GameObject;
        if (pretab != null)
            readingSprite = GameObject.Instantiate(pretab) as GameObject;
        else{
            yield return null;yield break;
        }
        if (skillIconDic.ContainsKey(skillID))
            skillIconDic[skillID] = readingSprite;
        else
            skillIconDic.Add(skillID, readingSprite);
        yield return readingSprite;
    }

    public GameObject getDefaultSkillIconByResource(int spLevel)
    {
        switch (spLevel)
        {
            case 0:
                return Resources.Load<GameObject>("Sprites/skillIcons/" + "normal_default") as GameObject;
            case 1:
                return Resources.Load<GameObject>("Sprites/skillIcons/" + "ex1_default") as GameObject;
            case 2:
                return Resources.Load<GameObject>("Sprites/skillIcons/" + "ex2_default") as GameObject;
            case 3:
                return Resources.Load<GameObject>("Sprites/skillIcons/" + "ex3_default") as GameObject;
        }
        return null;
    }
}
