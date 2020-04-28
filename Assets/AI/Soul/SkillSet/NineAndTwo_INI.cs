using System.Collections.Generic;
using UnityEngine;
using Skill;

public partial class NineAndTwo
{
    SkillEntity A1, A2, A3, B1, B2, B3, C1, C2, C3, D, M, R;
    List<SkillEntity> StateTransitionSetList;//这个的作用是发生在StateDictionary的生成阶段。见AIStateRunner之FormFightingSetsByNineAndTwo
    
    List<SkillEntity> H1_E_list = new List<SkillEntity>();
    List<SkillEntity> H2_E_list = new List<SkillEntity>();
    List<SkillEntity> H3_E_list = new List<SkillEntity>();
    
    List<string> H1_list = new List<string>();
    List<string> H2_list = new List<string>();
    List<string> H3_list = new List<string>();
    
    public void SortNineAndTwo()
    {
        AConfig1 = A1skillid != null ? GetSkillConfigBySkillId(A1skillid) : new SkillConfig();
        AConfig2 = A2skillid != null ? GetSkillConfigBySkillId(A2skillid) : new SkillConfig();
        AConfig3 = A3skillid != null ? GetSkillConfigBySkillId(A3skillid) : new SkillConfig();
        BConfig1 = B1skillid != null ? GetSkillConfigBySkillId(B1skillid) : new SkillConfig();
        BConfig2 = B2skillid != null ? GetSkillConfigBySkillId(B2skillid) : new SkillConfig();
        BConfig3 = B3skillid != null ? GetSkillConfigBySkillId(B3skillid) : new SkillConfig();
        CConfig1 = C1skillid != null ? GetSkillConfigBySkillId(C1skillid) : new SkillConfig();
        CConfig2 = C2skillid != null ? GetSkillConfigBySkillId(C2skillid) : new SkillConfig();
        CConfig3 = C3skillid != null ? GetSkillConfigBySkillId(C3skillid) : new SkillConfig();
        
        A1 = AConfig1 != null ? GetSE(A1skillid, A1level) : null;
        A2 = AConfig2 != null ? GetSE(A2skillid, A2level) : null;
        A3 = AConfig3 != null ? GetSE(A3skillid, A3level) : null;
        
        B1 = BConfig1 != null ? GetSE(B1skillid, B1level) : null;
        B2 = BConfig2 != null ? GetSE(B2skillid, B2level) : null;
        B3 = BConfig3 != null ? GetSE(B3skillid, B3level) : null;
        
        C1 = CConfig1 != null ? GetSE(C1skillid, C1level) : null;
        C2 = CConfig2 != null ? GetSE(C2skillid, C2level) : null;
        C3 = CConfig3 != null ? GetSE(C3skillid, C3level) : null;
        
        ////////////  关于DMR 的处理，和角色本身被动有关，有别于现在的9宫  ////////////
        PassiveSkillEntitys passiveSkillConfigs = new PassiveSkillEntitys(moveType, canDefend, rushType);
        D = passiveSkillConfigs.D_SE;
        M = passiveSkillConfigs.M_SE;
        R = passiveSkillConfigs.R_SE;
        //////////////////////////////////////////////////////////////////////////
        
        H1_E_list.Clear();
        H2_E_list.Clear();
        H3_E_list.Clear();
        
        H1_list.Clear();
        H2_list.Clear();
        H3_list.Clear();
        
        if (A1 != null)
        {
            H1_E_list.Add(A1);
            H1_list.Add(A1.REAL_NAME);
            A1.EnterInput = InputKey.Attack1;
            A1.ExitInput = InputKey.Null;
        }
        if (A2 != null)
        {
            H2_E_list.Add(A2);
            H2_list.Add(A2.REAL_NAME);
            A2.EnterInput = InputKey.Attack1;
            A2.ExitInput = InputKey.Null;
        }
        if (A3 != null)
        {
            H3_E_list.Add(A3);
            H3_list.Add(A3.REAL_NAME);
            A3.EnterInput = InputKey.Attack1;
            A3.ExitInput = InputKey.Null;
        }

        if (B1 != null)
        {
            H1_E_list.Add(B1);
            H1_list.Add(B1.REAL_NAME);
            B1.EnterInput = InputKey.Attack2;
            B1.ExitInput = InputKey.Null;
        }
        if (B2 != null)
        {
            H2_E_list.Add(B2);
            H2_list.Add(B2.REAL_NAME);
            B2.EnterInput = InputKey.Attack2;
            B2.ExitInput = InputKey.Null;
        }
        if (B3 != null)
        {
            H3_E_list.Add(B3);
            H3_list.Add(B3.REAL_NAME);
            B3.EnterInput = InputKey.Attack2;
            B3.ExitInput = InputKey.Null;
        }

        if (C1 != null)
        {
            H1_E_list.Add(C1);
            H1_list.Add(C1.REAL_NAME);
            C1.EnterInput = InputKey.Attack3;
            C1.ExitInput = InputKey.Null;
        }
        if (C2 != null)
        {
            H2_E_list.Add(C2);
            H2_list.Add(C2.REAL_NAME);
            C2.EnterInput = InputKey.Attack3;
            C2.ExitInput = InputKey.Null;
        }
        if (C3 != null)
        {
            H3_E_list.Add(C3);
            H3_list.Add(C3.REAL_NAME);
            C3.EnterInput = InputKey.Attack3;
            C3.ExitInput = InputKey.Null;
        }
        
        if (R != null)
        {
            H1_E_list.Add(R);
            H2_E_list.Add(R);
            H3_E_list.Add(R);
            
            H1_list.Add(R.REAL_NAME);
            H2_list.Add(R.REAL_NAME);
            H3_list.Add(R.REAL_NAME);
        }
        if (D != null)
        {
            H1_E_list.Add(D);
            H2_E_list.Add(D);
            H3_E_list.Add(D);
            
            H1_list.Add(D.REAL_NAME);
            H2_list.Add(D.REAL_NAME);
            H3_list.Add(D.REAL_NAME);
        }
        
        for (int i = 0; i < H1_E_list.Count; i++)
        {
            H1_E_list[i].CasualTo = H2_list.ToArray();
        }
        for (int i = 0; i < H2_E_list.Count; i++)
        {
            H2_E_list[i].CasualTo = H3_list.ToArray();
        }        
        for (int i = 0; i < H3_E_list.Count; i++)
        {
            H3_E_list[i].CasualTo = H1_list.ToArray();
        }
        
        M.CasualTo = H1_list.ToArray();
        if (D != null)
            D.CasualTo = H1_list.ToArray();
        if (R != null)
            R.CasualTo = H1_list.ToArray();
    }
    
    // FormFightingSetsByNineAndTwo(string type,NineAndTwo nineAndTwo, passiveSkillConfigs passiveSkillConfigs, int AI_level) -->
    // 1.sortNineAndTwo(passiveSkillConfigs):整理三连击的连续关系。根据数据库配置好相应技能的属性。
    // 2.GenerateBeheviourSets():正式配置各State_Transition_Set，并且适配好所有技能组的force和casual迁移。
    public IDictionary<string, SkillEntity> GenerateBeheviourSets()
    {
        IDictionary<string, SkillEntity> _SEDic = new Dictionary<string, SkillEntity>();
        StateTransitionSetList = new List<SkillEntity>();
        
        SkillEntity Empty = new SkillEntity("Empty", 0, 0, 0, 0, 0, 0, null, null, InputKey.Null, InputKey.Null, 0, 0);
        SkillEntity Victory = new SkillEntity("Victory",0, 0, 0, 0, 0, 0, null, null, InputKey.Null, InputKey.Null, 0, 0);
        SkillEntity Death = new SkillEntity("Death", 0, 0, 0, 0, 0, 0, null, null, InputKey.Null, InputKey.Null, 0, 0);
        SkillEntity Hit = new SkillEntity("Hit", 0, BehaviorType.Hit, 0, 0, 0, 0, H1_list.ToArray(),null,InputKey.Null, InputKey.Null,0,0);
                                                            
        StateTransitionSetList.Add(Empty);
        StateTransitionSetList.Add(Victory);
        StateTransitionSetList.Add(Death);
        StateTransitionSetList.Add(Hit);
        StateTransitionSetList.Add(M);
        if (D != null)
        {
            StateTransitionSetList.Add(D);
        }
        if (R != null)
        {
            StateTransitionSetList.Add(R);
        }
        
        if(this.A1 != null)
        {
            StateTransitionSetList.Add(A1);
        }
        if (this.A2 != null)
        {
            StateTransitionSetList.Add(A2);
        }            
        if (this.A3 != null)
        {
            StateTransitionSetList.Add(A3);
        }
        if (this.B1 != null)
        {
            StateTransitionSetList.Add(B1);
        }            
        if (this.B2 != null)
        {
            StateTransitionSetList.Add(B2);
        }
        if (this.B3 != null)
        {
            StateTransitionSetList.Add(B3);
        }
        if (this.C1 != null)
        {
            StateTransitionSetList.Add(C1);
        }                    
        if (this.C2 != null)
        {
            StateTransitionSetList.Add(C2);
        }            
        if (this.C3 != null)
        {
            StateTransitionSetList.Add(C3);
        }
        
        SkillEntity getUp = new SkillEntity("getUp", 0, BehaviorType.GetUp, 0, 0, 0, 0, H1_list.ToArray(), null, InputKey.Any, InputKey.Null,0,0);
        SkillEntity KnockOff = new SkillEntity("KnockOff", 0, BehaviorType.KnockOff, 0, 0, 0, 0, new string[] { "getUp" },null,InputKey.Null, InputKey.Null,0,0);
        StateTransitionSetList.Add(getUp);
        StateTransitionSetList.Add(KnockOff);
        
        foreach (SkillEntity _SE in StateTransitionSetList)
        {
            if (_SE.REAL_NAME != null && !_SEDic.ContainsKey(_SE.REAL_NAME))
            {
                _SEDic.Add(new KeyValuePair<string, SkillEntity>(_SE.REAL_NAME,_SE));
            }
            else
            {
                if (_SE.REAL_NAME == null)
                {
                    Debug.Log("键值为空？？");
                }else{
                    Debug.Log("角色自身技能产生键值重复："+_SE.REAL_NAME);
                }
            }
        }
        return _SEDic;
    }
    
    // 这个应该是所谓技能等级的着手点
    SkillEntity GetSE(string skillid, int level)
    {
        SkillConfig SC = GetSkillConfigBySkillId(skillid);
        if (SC == null)
            return null;
            
        if (!string.IsNullOrEmpty(SC.REAL_NAME))
        {
            SkillEntity _SE = new SkillEntity(
                SC.REAL_NAME,
                0,
                SC.STATE_TYPE,
                SkillEntity.ATCal(SC.ATTACK_WEIGHT,level),
                SkillEntity.StoneHpCal(SC.HP_WEIGHT,level),
                SC.AI_MIN_DIS,
                SC.AI_MAX_DIS,
                null,
                null,
                InputKey.Null,
                InputKey.Null,
                SC.SP_LEVEL,
                SC.RARITY_LEVEL
            );
            return _SE;
        }
        return null;
    }
        
    SkillConfig GetSkillConfigBySkillId(string skillid)
    {
        if (skillid == null)
        {
            return null;
        }
        SkillConfigTable.SkillConfigRefDic.TryGetValue(skillid, out SkillConfig REF);
        return REF;
    }
}