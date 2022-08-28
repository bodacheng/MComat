using System.Collections.Generic;
using Skill;

public partial class SkillSet
{
    public List<string> SkillIDList()
    {
        var IDs = new List<string>();
        
        if (a1 != null)
            IDs.Add(a1);
        if (a2 != null)
            IDs.Add(a2);
        if (a3 != null)
            IDs.Add(a3);
            
        if (b1 != null)
            IDs.Add(b1);
        if (b2 != null)
            IDs.Add(b2);
        if (b3 != null)
            IDs.Add(b3);
            
        if (c1 != null)
            IDs.Add(c1);
        if (c2 != null)
            IDs.Add(c2);
        if (c3 != null)
            IDs.Add(c3);
        
        return IDs;
    }
    
    // 获取平均技能等级
    public int GetAerLevel(List<float> levels)
    {
        float aver = 0;
        foreach (var t in levels)
        {
            aver += t;
        }
        aver /= levels.Count;
        // 取小数点后一位
        var intValue = (int)(aver * 10);
        intValue /= 10;
        return intValue;
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
    public IDictionary<int, SkillEntity> GetAttack1Chan()
    {
        IDictionary<int, SkillEntity> chain = new Dictionary<int, SkillEntity>
        {
            { 1, A1 },
            { 2, A2 },
            { 3, A3 }
        };
        return chain;
    }
    public IDictionary<int, SkillEntity> GetAttack2Chan()
    {
        IDictionary<int, SkillEntity> chain = new Dictionary<int, SkillEntity>
        {
            { 1, B1 },
            { 2, B2 },
            { 3, B3 }
        };
        return chain;
    }
    public IDictionary<int, SkillEntity> GetAttack3Chan()
    {
        IDictionary<int, SkillEntity> chain = new Dictionary<int, SkillEntity>
        {
            { 1, C1 },
            { 2, C2 },
            { 3, C3 }
        };
        return chain;
    }
    
    public SkillConfig GetA1Config()
    {
        return SkillConfigTable.GetSkillConfig(a1);
    }
    public SkillConfig GetA2Config()
    {
        return SkillConfigTable.GetSkillConfig(a2);
    }
    public SkillConfig GetA3Config()
    {
        return SkillConfigTable.GetSkillConfig(a3);
    }
    public SkillConfig GetB1Config()
    {
        return SkillConfigTable.GetSkillConfig(b1);
    }
    public SkillConfig GetB2Config()
    {
        return SkillConfigTable.GetSkillConfig(b2);
    }
    public SkillConfig GetB3Config()
    {
        return SkillConfigTable.GetSkillConfig(b3);
    }
    public SkillConfig GetC1Config()
    {
        return SkillConfigTable.GetSkillConfig(c1);
    }
    public SkillConfig GetC2Config()
    {
        return SkillConfigTable.GetSkillConfig(c2);
    }
    public SkillConfig GetC3Config()
    {
        return SkillConfigTable.GetSkillConfig(c3);
    }
        
    public SkillEntity GetM_STS()
    {
        return M;
    }
}
