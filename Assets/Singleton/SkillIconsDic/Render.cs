using UnityEngine;
using UnityEngine.UI;

public partial class SkillIconsDic
{
    public GameObject FindSkillIconPrefab(string skillId)
    {
        GameObject returnValue;
        if (!_skillIconDic.ContainsKey(skillId))
        {
            var skillConfig = SkillConfigTable.GetSkillConfig(skillId);
            // 图标可以是Sprite或其他格式，只要名字对上编号就可以

            var sprite = Cach.LoadT<Sprite>(skillId);
            GameObject prefab;
            if (sprite != null)
            {
                prefab = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                returnValue = Object.Instantiate(prefab);
                returnValue.GetComponent<Image>().sprite = sprite;
            }
            else
            {
                prefab = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                returnValue = Object.Instantiate(prefab);
            }
            DicAdd<string, GameObject>.Add(_skillIconDic, skillId, returnValue);
        }
        else
        {
            _skillIconDic.TryGetValue(skillId, out returnValue);
            if (returnValue == null)
            {
                _skillIconDic.Remove(skillId);
                return FindSkillIconPrefab(skillId);
            }
        }
        
        return returnValue;
    }
    
    GameObject _d, _ex1, _ex2, _ex3;
    GameObject GetDefaultSkillIconByResource(int spLevel)
    {
        switch (spLevel)
        {
            case 0:
                if (_d == null)
                {
                    _d = Resources.Load<GameObject>("BasicSprites/normal_default");
                }
                return _d;
            case 1:
                if (_ex1 == null)
                {
                    _ex1 = Resources.Load<GameObject>("BasicSprites/ex1_default");
                }
                return _ex1;
            case 2:
                if (_ex2 == null)
                {
                    _ex2 = Resources.Load<GameObject>("BasicSprites/ex2_default");
                }
                return _ex2;
            case 3:
                if (_ex3 == null)
                {
                    _ex3 = Resources.Load<GameObject>("BasicSprites/ex3_default");
                }
                return _ex3;
        }
        return null;
    }
}
