using System.Collections.Generic;
using System.Linq;
using MCombat.Shared.Combat;

namespace FightScene
{
    public sealed class SubUnitSupport
    {
        readonly IDictionary<string, string> _subUnitRecordIds = new Dictionary<string, string>();
        bool _definitionsLoaded;

        public void RefreshDefinitions()
        {
            _subUnitRecordIds.Clear();
            _definitionsLoaded = true;
            if (!Units.IsLoaded())
            {
                _definitionsLoaded = false;
                return;
            }

            foreach (var pair in Units.Dic)
            {
                var config = pair.Value;
                if (!string.IsNullOrEmpty(config.SubUnitRecordId))
                {
                    _subUnitRecordIds[pair.Key] = config.SubUnitRecordId;
                }
            }
        }

        public void EnsureDefinitionsLoaded()
        {
            if (!_definitionsLoaded)
            {
                RefreshDefinitions();
            }
        }

        public bool HasSubUnit(Data_Center center)
        {
            EnsureDefinitionsLoaded();
            return center?.UnitInfo != null && _subUnitRecordIds.ContainsKey(center.UnitInfo.r_id);
        }

        public bool CanSelectAsRotationMember(Data_Center center)
        {
            return center != null && !center.IsSubUnit;
        }

        public void AddSubUnits(FightInfo info)
        {
            EnsureDefinitionsLoaded();
            if (info == null)
            {
                return;
            }

            if (!SubUnitUtility.SupportsSubUnitRoster(info.FightMode))
            {
                return;
            }

            AddSubUnits(info.FightMembers.HeroSets);
            AddSubUnits(info.FightMembers.EnemySets);
        }

        public Data_Center FindSubUnit(Data_Center center, IDictionary<Data_Center, UnitInfo> unitInfoRef)
        {
            if (center?.UnitInfo == null || unitInfoRef == null)
            {
                return null;
            }

            var subUnitId = SubUnitUtility.GetSubUnitId(center.UnitInfo.Guid);
            if (string.IsNullOrEmpty(subUnitId))
            {
                return null;
            }

            var subUnit = unitInfoRef.FirstOrDefault(x =>
                x.Key != null
                && x.Value != null
                && x.Value.id == subUnitId
                && x.Key._TeamConfig.myTeam == center._TeamConfig.myTeam);
            return subUnit.Key;
        }

        void AddSubUnits(MultiDic<int, int, UnitInfo> roster)
        {
            var supportIndex = 0;
            foreach (var unitInfo in roster.GetValues().ToList())
            {
                supportIndex++;
                AddSubUnit(roster, unitInfo, supportIndex);
            }
        }

        void AddSubUnit(MultiDic<int, int, UnitInfo> roster, UnitInfo unitInfo, int supportIndex)
        {
            if (unitInfo == null || !_subUnitRecordIds.TryGetValue(unitInfo.r_id, out var subUnitRid))
            {
                return;
            }

            var subUnitInfo = unitInfo.Clone();
            subUnitInfo.r_id = subUnitRid;
            subUnitInfo.id = SubUnitUtility.GetSubUnitId(unitInfo.Guid);
            if (string.IsNullOrEmpty(subUnitInfo.id))
            {
                return;
            }

            if (roster.GetValues().Any(x => x?.id == subUnitInfo.id))
            {
                return;
            }

            roster.Set(0, (supportIndex + 1) * 10 + 1, subUnitInfo);
        }
    }
}
