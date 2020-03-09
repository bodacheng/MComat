using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconsDic {

    static SkillIconsDic instance;
    public static SkillIconsDic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SkillIconsDic();
            }
            return instance;
        }
    }

    GameObject readingSprite;
    IDictionary<string, GameObject> SkillIconDic = new Dictionary<string, GameObject>();
    
    public GameObject GetSkillIconSyn(string skillID)
    {
        SkillIconDic.TryGetValue(skillID, out readingSprite);
        return readingSprite;
    }

    public IEnumerator FindSkillIconByCach(string skillID)
    {
        SkillIconDic.TryGetValue(skillID, out readingSprite);
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
                    readingSprite = UnityEngine.Object.Instantiate(pretab) as GameObject;
                else
                {
                    yield return null; yield break;
                }
                if (SkillIconDic.ContainsKey(skillID))
                    SkillIconDic[skillID] = readingSprite;
                else
                    SkillIconDic.Add(skillID, readingSprite);
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

    public IEnumerator FindSkillIconByResource(string skillID)
    {
        SkillIconDic.TryGetValue(skillID, out readingSprite);
        if (readingSprite != null)
        {
            yield return readingSprite;
        }
        
        Sprite sprite = Resources.Load<Sprite>("Sprites/skillIcons/" + skillID);
        if (sprite != null)
        {
            GameObject _base = Instance.GetDefaultSkillIconByResource(0);
            readingSprite = Object.Instantiate(_base);
            readingSprite.GetComponent<Image>().sprite = sprite;
        }
        else
        {
            GameObject ICON = Resources.Load("Sprites/skillIcons/" + skillID) as GameObject;
            if (ICON != null)
            {
                readingSprite = Object.Instantiate(ICON);
            }
            else
            {
                GameObject _base = Instance.GetDefaultSkillIconByResource(0);
                readingSprite = Object.Instantiate(_base);
            }
        }
        
        if (SkillIconDic.ContainsKey(skillID))
        {
            SkillIconDic[skillID] = readingSprite;
        }
        else
        {
            SkillIconDic.Add(skillID, readingSprite);
        }
        yield return readingSprite;
    }
    
    public GameObject GetDefaultSkillIconByResource(int spLevel)
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
