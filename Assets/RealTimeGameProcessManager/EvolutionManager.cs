using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using FightScene;
using mainMenu;

public class EvolutionManager
{
    private int _costLimit;
    public int EvolutionCount { get; set; }
    
    public string[] RandomSkillList(string type, SkillSet skillSet)
    {
        int[] exType;

        switch (EvolutionCount)
        {
            case 1:
                exType = new[] {1};
                break;
            case 2:
                exType = new[] {2};
                break;
            case 3:
                exType = new[] {2, 3};
                break;
            default:
                exType = new[] {skillSet.GetLowestSpLevel()+1}; // temp logic
                break;
        }
        
        var filterForm = new SkillStonesBox.StoneFilterForm
        {
            Type = type,
            ExType = exType,
            Close = false,
            Near = false,
            Far = false
        };
        var skill1 =  SkillSet.RandomSkillIDOfStone(filterForm);
        var skill2 = SkillSet.RandomSkillIDOfStone(filterForm, new List<string>(){ skill1 });
        var skill3 = SkillSet.RandomSkillIDOfStone(filterForm, new List<string>(){ skill1, skill2 });
        return new[] { skill1, skill2, skill3 };
    }
    
    public async UniTask ChangeSkill(Data_Center focusUnit, int targetSlotIndex, string skillId)
    {
        List<UniTask> tasks = new List<UniTask>();
        
        async UniTask _ChangeSkill(Data_Center unitDataCenter, int _targetSlotIndex, string _skillId)
        {
            unitDataCenter._MyBehaviorRunner.ChangeToWaitingState();
            unitDataCenter._MyBehaviorRunner.fixedSkillSequence.Clear();
            switch (_targetSlotIndex)
            {
                case 1:
                    unitDataCenter.UnitInfo.set.a1 = _skillId;
                    break;
                case 2:
                    unitDataCenter.UnitInfo.set.a2 = _skillId;
                    break;
                case 3:
                    unitDataCenter.UnitInfo.set.a3 = _skillId;
                    break;
                case 4:
                    unitDataCenter.UnitInfo.set.b1 = _skillId;
                    break;
                case 5:
                    unitDataCenter.UnitInfo.set.b2 = _skillId;
                    break;
                case 6:
                    unitDataCenter.UnitInfo.set.b3 = _skillId;
                    break;
                case 7:
                    unitDataCenter.UnitInfo.set.c1 = _skillId;
                    break;
                case 8:
                    unitDataCenter.UnitInfo.set.c2 = _skillId;
                    break;
                case 9:
                    unitDataCenter.UnitInfo.set.c3 = _skillId;
                    break;
            }
            var unitConfig = Units.RowToUnitConfigInfo(Units.Find_RECORD_ID(unitDataCenter.UnitInfo.r_id));
            var _layer = UILayerLoader.Get<FightingStepLayer>();
            await UniTask.WhenAll(
                unitDataCenter.Step2Initialize(unitConfig.TYPE, unitConfig.element, unitDataCenter.UnitInfo.set, 1),
                _layer.InputsManager.ElementRegister(unitConfig.element, unitDataCenter.UnitInfo)
            );
        
            unitDataCenter.SetAT();
            
            if (EvolutionCount == 2)
            {
                unitDataCenter.FightDataRef.CriticalGaugeMode = CriticalGaugeMode.DoubleGain;
            }
            if (EvolutionCount >= 3)
            {
                unitDataCenter.FightDataRef.CriticalGaugeMode = CriticalGaugeMode.Unlimited;
            }
        }
        tasks.Add(_ChangeSkill(focusUnit, targetSlotIndex, skillId));
        var subUnit = RTFightManager.Target.FindSubUnit(focusUnit);
        if (subUnit != null)
        {
            tasks.Add(_ChangeSkill(subUnit, targetSlotIndex, skillId));
        }
        await UniTask.WhenAll(tasks);
        focusUnit._MyBehaviorRunner.ChangeToWaitingState();
    }
}
