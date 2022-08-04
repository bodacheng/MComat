using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using Skill;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        [Header("Order Button")]
        public Text orderButtonText;
        
        int ordertype = 0;
        
        // 功能本身直接放按钮上，但text要适配到SkillStonesBox上。
        public void SwitchOrder()
        {
            ordertype++;
            if (ordertype == 5)
            {
                ordertype = 0;
            }
            _Selected.gameObject.SetActive(false);
            RestFilter();
        }
              
        List<string> Order(List<string> targets)
        {
            switch (ordertype)
            {
                case 4: // 等级降序
                    orderButtonText.text = "Level ASC";
                return ByLevel(targets, 1);
                case 3: // 等级升序
                    orderButtonText.text = "Level DES";
                return ByLevel(targets,0);
                case 2: // 稀有度降序
                    orderButtonText.text = "Rarity ASC";
                return ByRareLevel(targets,1);
                case 1: // 稀有度升序
                    orderButtonText.text = "Rarity DES";
                return ByRareLevel(targets,0);
                case 0: // 以技能ID
                    orderButtonText.text = "開発番号";
                return ByDevID(targets, 1);
            }
            return targets;
        }

        List<string> ByDevID(List<string> targets, int order) //1:升序 0:降序 
        {
            for (int i = 0; i < targets.Count - 1; i++)
            {
                for (int j = 0; j < targets.Count - 1 - i; j++)
                {
                    StoneOfPlayerInfo myStone1 = Stones.Get(targets[j]);
                    StoneOfPlayerInfo myStone2 = Stones.Get(targets[j + 1]);
                    SkillConfig skillConfig1 = SkillConfigTable.GetSkillConfig(myStone1.skillId);
                    SkillConfig skillConfig2 = SkillConfigTable.GetSkillConfig(myStone2.skillId);

                    if (order == 1 ? int.Parse(skillConfig1.RECORD_ID) > int.Parse(skillConfig2.RECORD_ID) : int.Parse(skillConfig2.RECORD_ID) < int.Parse(skillConfig1.RECORD_ID))
                    {
                        string temp = targets[j];
                        targets[j] = targets[j + 1];
                        targets[j + 1] = temp;
                    }
                }
            }
            return targets;
        }
        
        // 等级升序降序
        List<string> ByLevel(List<string> targets, int order) //1:升序 0:降序 
        {
            for (int i = 0; i < targets.Count - 1; i++)
            {
                for (int j = 0; j < targets.Count - 1 - i; j++)
                {
                    StoneOfPlayerInfo myStone1 = Stones.Get(targets[j]);
                    StoneOfPlayerInfo myStone2 = Stones.Get(targets[j+1]);
                    
                    if (order == 1 ? myStone1.level > myStone2.level : myStone1.level < myStone2.level)
                    {
                        string temp = targets[j];
                        targets[j] = targets[j + 1];
                        targets[j + 1] = temp;
                    }
                }
            }
            return targets;
        }
        
        List<string> ByRareLevel(List<string> targets, int order) //1:升序 0:降序 
        {
            targets = ByLevel(targets,1);
            for (int i = 0; i < targets.Count - 1; i++)
            {
                for (int j = 0; j < targets.Count - 1 - i; j++)
                {
                    StoneOfPlayerInfo myStone1 = Stones.Get(targets[j]);
                    StoneOfPlayerInfo myStone2 = Stones.Get(targets[j+1]);
                    SkillConfig skillConfig1 = SkillConfigTable.GetSkillConfig(myStone1.skillId);
                    SkillConfig skillConfig2 = SkillConfigTable.GetSkillConfig(myStone2.skillId);
                    
                    if (order == 1 ? skillConfig1.RARITY_LEVEL > skillConfig2.RARITY_LEVEL : skillConfig2.RARITY_LEVEL < skillConfig1.RARITY_LEVEL)
                    {
                        string temp = targets[j];
                        targets[j] = targets[j + 1];
                        targets[j + 1] = temp;
                    }
                }
            }
            return targets;
        }
    }
}