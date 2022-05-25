using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public partial class SkillIconsDic
{
    public GameObject FindSkillIconPrefab(string skillId)
    {
        GameObject returnValue;
        if (!SkillIconDic.ContainsKey(skillId))
        {
            var skillConfig = SkillConfigTable.GetSkillConfig(skillId);
            // 图标可以是Sprite或其他格式，只要名字对上编号就可以
            
            var op = Addressables.LoadAssetAsync<Sprite>(skillId);
            var sprite = op.WaitForCompletion();
            Addressables.Release(op);
            
            GameObject prefab;
            if (sprite != null)
            {
                prefab = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                returnValue = Object.Instantiate(prefab);
                returnValue.GetComponent<Image>().sprite = sprite;
            }
            else
            {
                var op2 = Addressables.LoadAssetAsync<GameObject>(skillId);
                prefab = op2.WaitForCompletion();
                Addressables.Release(op2);
                if (prefab == null)
                {
                    prefab = Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL);
                }
                returnValue = Object.Instantiate(prefab);
            }
            DicAdd<string, GameObject>.Add(SkillIconDic, skillId, returnValue);
        }
        else
        {
            SkillIconDic.TryGetValue(skillId, out returnValue);
            if (returnValue == null)
            {
                SkillIconDic.Remove(skillId);
                return FindSkillIconPrefab(skillId);
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
