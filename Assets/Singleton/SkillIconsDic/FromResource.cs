using UnityEngine;
using UnityEngine.UI;
using Skill;

public partial class SkillIconsDic
{
    public GameObject FindSkillIconPrefabByResource(string skillID)
    {
        GameObject prefab;
        SkillIconDic.TryGetValue(skillID, out prefab);
        
        if (prefab == null)
        {
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
            // 图标可以是Sprite或其他格式，只要名字对上编号就可以
            Sprite sprite = Resources.Load<Sprite>("Sprites/skillIcons/" + skillID);
            if (sprite != null)
            {
                prefab = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                prefab.GetComponent<Image>().sprite = sprite;
            }
            else
            {
                prefab = Resources.Load("Sprites/skillIcons/" + skillID) as GameObject;
                if (prefab == null)
                {
                    GameObject _base = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                    prefab = Object.Instantiate(_base);
                }
            }
        }
        
        DicAdd<string, GameObject>.Add(SkillIconDic, skillID, prefab);
        return prefab;
    }

    GameObject d, ex1, ex2, ex3;
    public GameObject GetDefaultSkillIconByResource(int spLevel)
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
