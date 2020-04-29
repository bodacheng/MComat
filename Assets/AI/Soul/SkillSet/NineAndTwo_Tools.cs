using System.Collections.Generic;
using Skill;
using UnityEngine;

public partial class NineAndTwo
{
    public List<string> SkillIDList()
    {
        List<SkillEntity> sklist = SkillEntityList();
        List<string> IDs = new List<string>();
        for (int i = 0; i < sklist.Count; i++)
        {
            IDs.Add(sklist[i].REAL_NAME);
        }
        return IDs;
    }

    public List<int> SkillLevelList()
    {
        List<SkillEntity> sklist = SkillEntityList();
        List<int> levels = new List<int>();
        for (int i = 0; i < sklist.Count; i++)
        {
            levels.Add(sklist[i].LEVEL);
        }
        return levels;
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
    
    public List<SkillEntity> ReturnSTSlist()
    {
        Debug.Log("行为总数：" + StateTransitionSetList.Count);
        return StateTransitionSetList;
    }
    
    public SkillEntity GetM_STS()
    {
        return M;
    }
}
