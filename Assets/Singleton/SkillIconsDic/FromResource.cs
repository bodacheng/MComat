using UnityEngine;
using UnityEngine.UI;
using Skill;

public partial class SkillIconsDic
{
    public GameObject FindSkillIconPrefabByResource(string skillID)
    {
        GameObject prefab, returnValue;
        if (!SkillIconDic.ContainsKey(skillID))
        {
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
            // 图标可以是Sprite或其他格式，只要名字对上编号就可以
            Sprite sprite = Resources.Load<Sprite>("Sprites/skillIcons/" + skillID);
            if (sprite != null)
            {
                prefab = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                returnValue = Object.Instantiate(prefab);
                returnValue.GetComponent<Image>().sprite = sprite;
            }
            else
            {
                prefab = Resources.Load("Sprites/skillIcons/" + skillID) as GameObject;
                if (prefab == null)
                {
                    prefab = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                }
                returnValue = Object.Instantiate(prefab);
            }
            DicAdd<string, GameObject>.Add(SkillIconDic, skillID, returnValue);
        }
        else
        {
            SkillIconDic.TryGetValue(skillID, out returnValue);
            if (returnValue == null)
            {
                SkillIconDic.Remove(skillID);
                return FindSkillIconPrefabByResource(skillID);
            }
        }
        
        return returnValue;
    }

    GameObject d, ex1, ex2, ex3;
    GameObject GetDefaultSkillIconByResource(int spLevel)
    {
        switch (spLevel)
        {
            case 0:
                if (d == null)
                {
                    d = Resources.Load<GameObject>("Sprites/skillIcons/normal_default");
                }
                return d;
            case 1:
                if (ex1 == null)
                {
                    ex1 = Resources.Load<GameObject>("Sprites/skillIcons/ex1_default");
                }
                return ex1;
            case 2:
                if (ex2 == null)
                {
                    ex2 = Resources.Load<GameObject>("Sprites/skillIcons/ex2_default");
                }
                return ex2;
            case 3:
                if (ex3 == null)
                {
                    ex3 = Resources.Load<GameObject>("Sprites/skillIcons/ex3_default");
                }
                return ex3;
        }
        return null;
    }
}
