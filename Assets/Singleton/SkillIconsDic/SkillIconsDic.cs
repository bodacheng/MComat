using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Skill;

public partial class SkillIconsDic {

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
    
    IDictionary<string, GameObject> SkillIconDic = new Dictionary<string, GameObject>();
    
    public IEnumerator FindSkillIconByResource(string skillID)
    {
        SkillIconDic.TryGetValue(skillID, out GameObject readingSprite);
        if (readingSprite != null)
        {
            yield return readingSprite;
            yield break;
        }
        SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
        
        // 图标可以是Sprite或其他格式，只要名字对上编号就可以
        Sprite sprite = Resources.Load<Sprite>("Sprites/skillIcons/" + skillID);
        if (sprite != null)
        {
            GameObject _base = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
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
                GameObject _base = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                readingSprite = Object.Instantiate(_base);
            }
        }
        DicAdd<string, GameObject>.Add(SkillIconDic, skillID, readingSprite);
        yield return readingSprite;
    }
    
    public GameObject GetDefaultSkillIconByResource(int spLevel)
    {
        switch (spLevel)
        {
            case 0:
                return Resources.Load<GameObject>("Sprites/skillIcons/normal_default") as GameObject;
            case 1:
                return Resources.Load<GameObject>("Sprites/skillIcons/ex1_default") as GameObject;
            case 2:
                return Resources.Load<GameObject>("Sprites/skillIcons/ex2_default") as GameObject;
            case 3:
                return Resources.Load<GameObject>("Sprites/skillIcons/ex3_default") as GameObject;
        }
        return null;
    }
}
