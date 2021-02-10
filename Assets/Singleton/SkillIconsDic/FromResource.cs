using UnityEngine;
using UnityEngine.UI;
using Skill;

public partial class SkillIconsDic
{
    public GameObject FindSkillIconByResource_P(string skillID)
    {
        GameObject readingSprite;
        SkillIconDic.TryGetValue(skillID, out readingSprite);
        if (readingSprite != null)
        {
            return readingSprite;
        }
        SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);

        // 图标可以是Sprite或其他格式，只要名字对上编号就可以
        if (!FightGlobalSetting._IgnoreStoneTexture)
        {
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
        }

        DicAdd<string, GameObject>.Add(SkillIconDic, skillID, readingSprite);
        return readingSprite;
    }

    GameObject d, ex1, ex2, ex3;
    public GameObject GetDefaultSkillIconByResource(int spLevel)
    {
        switch (spLevel)
        {
            case 0:
                if (d == null)
                {
                    d = Resources.Load<GameObject>("Sprites/skillIcons/normal_default") as GameObject;
                }
                return d;
            case 1:
                if (ex1 == null)
                {
                    ex1 = Resources.Load<GameObject>("Sprites/skillIcons/ex1_default") as GameObject;
                }
                return ex1;
            case 2:
                if (ex2 == null)
                {
                    ex2 = Resources.Load<GameObject>("Sprites/skillIcons/ex2_default") as GameObject;
                }
                return ex2;
            case 3:
                if (ex3 == null)
                {
                    ex3 = Resources.Load<GameObject>("Sprites/skillIcons/ex3_default") as GameObject;
                }
                return ex3;
        }
        return null;
    }
}
