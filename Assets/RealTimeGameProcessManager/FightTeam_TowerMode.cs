using System.Collections.Generic;
using UnityEngine;

namespace FightScene
{
    public class FightTeam_TowerMode : FightTeam
    {
        public IDictionary<Data_Center, bool> characterAliveDic = new Dictionary<Data_Center, bool>();
        public IDictionary<Data_Center, int> characterDieCount = new Dictionary<Data_Center, int>();
        public IDictionary<Data_Center, float> turnCoolDownDic = new Dictionary<Data_Center, float>();

        public void towerModeLocalUpdate()
        {
            foreach (KeyValuePair<Data_Center, bool> keyValuePair in characterAliveDic)
            {
                if (!keyValuePair.Value)//如果角色死亡中
                {
                    turnCoolDownDic[keyValuePair.Key] -= Time.deltaTime;
                    if (!(turnCoolDownDic[keyValuePair.Key] > 0))
                    {
                        characterRevive(keyValuePair.Key);
                    }
                }
            }
        }

        public void characterRevive(Data_Center character)
        {
            characterAliveDic[character] = true;
        }

        public void characterDieOnce(Data_Center character)
        {
            characterDieCount[character] += 1;
            characterAliveDic[character] = false;
            turnCoolDownDic[character] = coolDownTime(characterDieCount[character]);
            character._MyBehaviorRunner.ChangeState("Empty");
        }

        float coolDownTime(int deathCount)
        {
            switch (deathCount)
            {
                case 1:
                    return 10f;
                case 2:
                    return 15f;
                case 3:
                    return 20f;
                case 4:
                    return 25f;
                case 5:
                    return 30f;
                case 6:
                    return 35f;
                case 7:
                    return 40f;
                case 8:
                    return 45f;
                default:
                    return 50f;
            }
        }

    }
}