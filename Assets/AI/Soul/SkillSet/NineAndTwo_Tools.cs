using System.Collections.Generic;
using Skill;

public partial class NineAndTwo
{
    public List<string> SkillIDList()
    {
        List<string> IDs = new List<string>();

        if (A1skillid != null)
            IDs.Add(A1skillid);
        if (A2skillid != null)
            IDs.Add(A2skillid);
        if (A3skillid != null)
            IDs.Add(A3skillid);
            
        if (B1skillid != null)
            IDs.Add(B1skillid);
        if (B2skillid != null)
            IDs.Add(B2skillid);
        if (B3skillid != null)
            IDs.Add(B3skillid);
            
        if (C1skillid != null)
            IDs.Add(C1skillid);
        if (C2skillid != null)
            IDs.Add(C2skillid);
        if (C3skillid != null)
            IDs.Add(C3skillid);
            
        return IDs;
    }
    
    // 获取平均技能等级
    float GetAerLevel()
    {
        List<int> levels = new List<int>();

        if (A1skillid != null)
            levels.Add(A1level);
        if (A2skillid != null)
            levels.Add(A2level);
        if (A3skillid != null)
            levels.Add(A3level);
            
        if (B1skillid != null)
            levels.Add(B1level);
        if (B2skillid != null)
            levels.Add(B2level);
        if (B3skillid != null)
            levels.Add(B3level);
            
        if (C1skillid != null)
            levels.Add(C1level);
        if (C2skillid != null)
            levels.Add(C2level);
        if (C3skillid != null)
            levels.Add(C3level);

        float aver = 0;
        for (int i = 0; i < levels.Count; i++)
        {
            aver += (float)levels[i];
        }
        aver = aver / levels.Count;
        // 取小数点后一位
        int intValue =(int)(aver * 10);
        aver = (float)(intValue * 1.0)/10;
        return aver;
    }
    
    public static float INI_Hp(List<SkillEntity> sklist)
    {
        float WholeHP = 0;
        for (int index = 0; index < sklist.Count; index++)
        {
            WholeHP += sklist[index].HP;
        }
        return WholeHP;
    }
    
    // 获取技能实体列表，调用必须在SortNineAndTwo之后
    public List<SkillEntity> SkillEntityList()
    {
        List<SkillEntity> behavior_Transition_Sets = new List<SkillEntity>();
        
        if (A1 != null)
            behavior_Transition_Sets.Add(A1);
        if (A2 != null)
            behavior_Transition_Sets.Add(A2);
        if (A3 != null)
            behavior_Transition_Sets.Add(A3);
        
        if (B1 != null)
            behavior_Transition_Sets.Add(B1);
        if (B2 != null)
            behavior_Transition_Sets.Add(B2);
        if (B3 != null)
            behavior_Transition_Sets.Add(B3);
        
        if (C1 != null)
            behavior_Transition_Sets.Add(C1);
        if (C2 != null)
            behavior_Transition_Sets.Add(C2);
        if (C3 != null)
            behavior_Transition_Sets.Add(C3);
            
        if (D != null)
            behavior_Transition_Sets.Add(D);
        if (M != null)
            behavior_Transition_Sets.Add(M);
        if (R != null)
            behavior_Transition_Sets.Add(R);
        if (Empty != null)
            behavior_Transition_Sets.Add(Empty);
        if (zhuangbi != null)
            behavior_Transition_Sets.Add(zhuangbi);
        if (Victory != null)
            behavior_Transition_Sets.Add(Victory);    
        if (Death != null)
            behavior_Transition_Sets.Add(Death);
        if (Hit != null)
            behavior_Transition_Sets.Add(Hit);
        if (getUp != null)
            behavior_Transition_Sets.Add(getUp);
        if (KnockOff != null)
            behavior_Transition_Sets.Add(KnockOff);
            
        return behavior_Transition_Sets;
    }
    
    //下面的环节纯粹是针对SkillPrintOut的一些处理
    public IDictionary<int, SkillEntity> GetAttack1Chuan()
    {
        IDictionary<int, SkillEntity> attack_chuan = new Dictionary<int, SkillEntity>
        {
            { 1, A1 },
            { 2, A2 },
            { 3, A3 }
        };
        return attack_chuan;
    }
    public IDictionary<int, SkillEntity> GetAttack2Chuan()
    {
        IDictionary<int, SkillEntity> B_chuan = new Dictionary<int, SkillEntity>
        {
            { 1, B1 },
            { 2, B2 },
            { 3, B3 }
        };
        return B_chuan;
    }
    public IDictionary<int, SkillEntity> GetAttack3Chuan()
    {
        IDictionary<int, SkillEntity> C_chuan = new Dictionary<int, SkillEntity>
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
    
    public SkillEntity GetM_STS()
    {
        return M;
    }
    
    // 平均设置所有技能的等级，只能用于关卡制作等等
    public void SetSkillLevel(int level)
    {
        A1level = level;
        A2level = level;
        A3level = level;
        B1level = level;
        B2level = level;
        B3level = level;
        C1level = level;
        C2level = level;
        C3level = level;
    }
}
