using System.Collections.Generic;
using Skill;

public partial class NineAndTwo
{
    //下面的环节纯粹是针对SkillPrintOut的一些处理
    public IDictionary<int, Behavior_Transition_Set> GetAttackChuan()
    {
        IDictionary<int, Behavior_Transition_Set> attack_chuan = new Dictionary<int, Behavior_Transition_Set>
        {
            { 1, A1 },
            { 2, A2 },
            { 3, A3 }
        };
        return attack_chuan;
    }
    public IDictionary<int, Behavior_Transition_Set> GetFire1Chuan()
    {
        IDictionary<int, Behavior_Transition_Set> B_chuan = new Dictionary<int, Behavior_Transition_Set>
        {
            { 1, B1 },
            { 2, B2 },
            { 3, B3 }
        };
        return B_chuan;
    }
    public IDictionary<int, Behavior_Transition_Set> GetFire2Chuan()
    {
        IDictionary<int, Behavior_Transition_Set> C_chuan = new Dictionary<int, Behavior_Transition_Set>
        {
            { 1, C1 },
            { 2, C2 },
            { 3, C3 }
        };
        return C_chuan;
    }
    
    public SkillConfig GetA1Config()
    {
        return AConfig1;
    }
    public SkillConfig GetA2Config()
    {
        return AConfig2;
    }
    public SkillConfig GetA3Config()
    {
        return AConfig3;
    }
    public SkillConfig GetB1Config()
    {
        return BConfig1;
    }
    public SkillConfig GetB2Config()
    {
        return BConfig2;
    }
    public SkillConfig GetB3Config()
    {
        return BConfig3;
    }
    public SkillConfig GetC1Config()
    {
        return CConfig1;
    }
    public SkillConfig GetC2Config()
    {
        return CConfig2;
    }
    public SkillConfig GetC3Config()
    {
        return CConfig3;
    }
    public SkillConfig GetDConfig()
    {
        return DConfig;
    }
    public SkillConfig GetMConfig()
    {
        return MConfig;
    }
    public SkillConfig GetRConfig()
    {
        return RConfig;
    }
    
    //这个函数是服务于stagesmanager。因为编辑关卡的时候是直接去编辑九宫格的config
    public void RefreshSkillNumsByConfigs()
    {
        A1skillid = AConfig1?.RECORD_ID;
        A2skillid = AConfig2?.RECORD_ID;
        A3skillid = AConfig3?.RECORD_ID;

        B1skillid = BConfig1?.RECORD_ID;
        B2skillid = BConfig2?.RECORD_ID;
        B3skillid = BConfig3?.RECORD_ID;

        C1skillid = CConfig1?.RECORD_ID;
        C2skillid = CConfig2?.RECORD_ID;
        C3skillid = CConfig3?.RECORD_ID;
    }
    
    public List<Behavior_Transition_Set> ReturnSTSlist()
    {
        return StateTransitionSetList;
    }
    
    public Behavior_Transition_Set GetD_STS()
    {
        return D;
    }
    public Behavior_Transition_Set GetM_STS()
    {
        return M;
    }
    public Behavior_Transition_Set GetR_STS()
    {
        return R;
    }
}
