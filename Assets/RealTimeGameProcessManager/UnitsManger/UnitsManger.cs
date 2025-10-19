using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public MultiDic<int, int, Data_Center> teamMembers;
        
        public FightMode FightMode;
        public TeamConfig teamConfig;
        public Transform[] TeamStandPoints;
        
        public MobileInputsManager InputsManager
        {
            set;
            get;
        }
        
        private bool _auto;
        public bool Auto
        {
            set
            {
                _auto = value;
                foreach (var dataCenter in teamMembers.GetValues())
                {
                    if (FightMode is FightMode.Rotate or FightMode.Evolve)
                        dataCenter._MyBehaviorRunner.AI = _auto;
                    else if (FightMode is FightMode.Multi or FightMode.Group)
                    {
                        if (this.teamConfig.myTeam == RTFightManager.playerTeam)
                        {
                            if (InputsManager != null && InputsManager.CurrentFocus != null && InputsManager.CurrentFocus.Value == dataCenter)
                            {
                                dataCenter._MyBehaviorRunner.AI = _auto;
                            }
                            else
                            {
                                dataCenter._MyBehaviorRunner.AI = true;
                            }
                        }
                        else
                        {
                            dataCenter._MyBehaviorRunner.AI = _auto;
                        }
                    }
                }
            }
            get => _auto;
        }
        
        public async UniTask _UnitsLoad(MultiDic<int, int, UnitInfo> membersSets, IDictionary<Data_Center, UnitInfo> unitInfoRef)
        {
            async UniTask LoadOneUnit(int key1, int key2, UnitInfo info, int preloadCount)
            {
                var center = teamMembers.Get(key1, key2);
                if (center == null)
                {
                    center = await UnitCreator.CreateUnit(info, preloadCount);
                }
                teamMembers.Set(key1, key2, center);
                DicAdd<Data_Center, UnitInfo>.Add(unitInfoRef, center, info);
            }
            var tasks = new List<UniTask>();
            foreach (var kv in membersSets.mDict)
            {
                var sameUnits = membersSets.mDict.Values.ToList().FindAll(x => x.r_id == kv.Value.r_id && x.level == kv.Value.level);
                // 上面给的这个统计相同种类角色的逻辑并不精确。如果一个队伍里有两个同masterid角色，等级还一样，问题就出来了，但现在我们的代码构造造成没有别的做法。
                tasks.Add(LoadOneUnit(kv.Key.Item1, kv.Key.Item2, kv.Value, sameUnits.Count));
            }
            await UniTask.WhenAll(tasks);
        }
        
        public bool IfAllUnitsPreparedForBattle()
        {
            foreach (var oneMember in teamMembers.GetValues())
            {
                if (!oneMember.IfPreparedForBattle())
                    return false;
            }
            return true;
        }
        
        public void LocalUpdate()
        {
            switch (FightMode)
            {
                case FightMode.Evolve:
                case FightMode.Rotate:
                    WaitUnitChange();
                    break;
                default:
                    break;
            }
        }
        
        public List<Transform> GetFightingUnitTs()
        {
            var transforms = new List<Transform>();
            switch (FightMode)
            {
                case FightMode.Multi:
                case FightMode.Group:
                    foreach (var unit in teamMembers.GetValues())
                    {
                        if (unit._MyBehaviorRunner.GetNowState().StateKey != "Death")
                        {
                            transforms.Add(unit.geometryCenter);
                        }
                    }
                    return transforms;
                case FightMode.Evolve:
                case FightMode.Rotate:
                    if (RMode_Unit.Value != null && RMode_Unit.Value._MyBehaviorRunner.GetNowState().StateKey != "Death")
                    {
                        transforms = new List<Transform>
                        {
                            RMode_Unit.Value.geometryCenter
                        };
                    }
                    return transforms;
            }
            return transforms;
        }

        public Transform GetRModeUnitT()
        {
            if (RMode_Unit.Value != null)
            {
                return RMode_Unit.Value.geometryCenter;
            }
            return null;
        }

        public Transform GetPrimaryStandPoint()
        {
            if (TeamStandPoints == null || TeamStandPoints.Length == 0)
            {
                return null;
            }

            foreach (var point in TeamStandPoints)
            {
                if (point != null)
                {
                    return point;
                }
            }

            return null;
        }
        
        // 全队无敌
        public void TurnAllUnitsInvincible(bool _Invincible)
        {
            foreach (var center in teamMembers.GetValues())
            {
                center.FightDataRef.Invincible = _Invincible;
            }
        }
        
        public void Clear()
        {
            foreach (var one in teamMembers.GetValues())
            {
                Destroy(one.WholeT.gameObject);
            }
            teamMembers.Clear();
        }
    }
}
