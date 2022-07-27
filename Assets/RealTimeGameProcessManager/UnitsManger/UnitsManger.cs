using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public MultiDic<int, int, Data_Center> TeamMembers;
        
        public TeamMode TeamMode;
        public TeamConfig teamConfig;
        
        [HideInInspector]
        public Transform[] TeamStandPoints;

        Data_Center StartUnit;
        
        public MobileInputsManager InputsManager
        {
            set;
            get;
        }
        
        private bool auto;
        public bool Auto
        {
            set
            {
                auto = value;
                foreach (var dataCenter in TeamMembers.GetValues())
                {
                    dataCenter._MyBehaviorRunner.AI = auto;
                }
            }
            get => auto;
        }
        
        public async UniTask _UnitsLoad(MultiDic<int, int, UnitInfo> MembersSets, IDictionary<Data_Center, UnitInfo> UnitInfoRef)
        {
            foreach (var kv in MembersSets.mDict)
            {
                var _one = kv.Value;
                var center = TeamMembers.Get(kv.Key.Item1, kv.Key.Item2);
                if (center == null)
                {
                    center = await UnitCreator.CreateUnit(_one);
                }
                
                TeamMembers.Set(kv.Key.Item1, kv.Key.Item2, center);
                DicAdd<Data_Center, UnitInfo>.Add(UnitInfoRef, center, _one);
            }
        }
        
        public bool IfAllUnitsPreparedForBattle()
        {
            foreach (var oneMember in TeamMembers.GetValues())
            {
                if (!oneMember.IfPreparedForBattle())
                    return false;
            }
            return true;
        }
        
        public void localUpdate()
        {
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    break;
                case TeamMode.rotation:
                    WaitUnitChange();
                    break;
            }
        }
        
        public List<Transform> GetFightingUnitTs()
        {
            var transforms = new List<Transform>();
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    foreach (var unit in TeamMembers.GetValues())
                    {
                        if (unit._MyBehaviorRunner.GetNowState().StateKey != "Death")
                        {
                            transforms.Add(unit.geometryCenter);
                        }
                    }
                    return transforms;
                case TeamMode.rotation:
                    if (RMode_Unit.Value != null)
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
        
        // 全队无敌
        public void TurnAllUnitsInvincible(bool _Invincible)
        {
            foreach (var center in TeamMembers.GetValues())
            {
                center.FightDataRef.Invincible = _Invincible;
            }
        }
    }
}