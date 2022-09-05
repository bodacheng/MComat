using System.Collections.Generic;
using UnityEngine;
using Skill;
using System.Linq;

public partial class SkillSet
{
    SkillEntity A1, A2, A3, B1, B2, B3, C1, C2, C3, D, M, R;

    #region 基础进程实体
    SkillEntity Empty, zhuangbi, Victory, Death, Hit, getUp, KnockOff;
    #endregion

    readonly List<SkillEntity> H1_E_list = new List<SkillEntity>();
    readonly List<SkillEntity> H2_E_list = new List<SkillEntity>();
    readonly List<SkillEntity> H3_E_list = new List<SkillEntity>();
    readonly List<string> H1_list = new List<string>();
    readonly List<string> H2_list = new List<string>();
    readonly List<string> H3_list = new List<string>();
    
    public void SortNineAndTwo(int lv)
    {
        var aConfig1 = a1 != null ? SkillConfigTable.GetSkillConfig(a1) : new SkillConfig();
        var aConfig2 = a2 != null ? SkillConfigTable.GetSkillConfig(a2) : new SkillConfig();
        var aConfig3 = a3 != null ? SkillConfigTable.GetSkillConfig(a3) : new SkillConfig();
        var bConfig1 = b1 != null ? SkillConfigTable.GetSkillConfig(b1) : new SkillConfig();
        var bConfig2 = b2 != null ? SkillConfigTable.GetSkillConfig(b2) : new SkillConfig();
        var bConfig3 = b3 != null ? SkillConfigTable.GetSkillConfig(b3) : new SkillConfig();
        var cConfig1 = c1 != null ? SkillConfigTable.GetSkillConfig(c1) : new SkillConfig();
        var cConfig2 = c2 != null ? SkillConfigTable.GetSkillConfig(c2) : new SkillConfig();
        var cConfig3 = c3 != null ? SkillConfigTable.GetSkillConfig(c3) : new SkillConfig();
        
        A1 = aConfig1 != null ? GetSE(a1, lv) : null;
        A2 = aConfig2 != null ? GetSE(a2, lv) : null;
        A3 = aConfig3 != null ? GetSE(a3, lv) : null;
        
        B1 = bConfig1 != null ? GetSE(b1, lv) : null;
        B2 = bConfig2 != null ? GetSE(b2, lv) : null;
        B3 = bConfig3 != null ? GetSE(b3, lv) : null;
        
        C1 = cConfig1 != null ? GetSE(c1, lv) : null;
        C2 = cConfig2 != null ? GetSE(c2, lv) : null;
        C3 = cConfig3 != null ? GetSE(c3, lv) : null;
        
        ////////////  关于DMR 的处理，和角色本身被动有关，有别于现在的9宫  ////////////
        D = Def ? SkillEntity.GetD_SE() : null;
        M = SkillEntity.GetM_SE(MoveType);
        M.CANBECANCELLEDTO = false;
        R = SkillEntity.GetR_SE(RushType);

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

        if (D != null && FightGlobalSetting._hasDefend)
        {
            H1_E_list.Add(D);
            H2_E_list.Add(D);
            H3_E_list.Add(D);
            
            H1_list.Add(D.REAL_NAME);
            H2_list.Add(D.REAL_NAME);
            H3_list.Add(D.REAL_NAME);
        }
        
        for (var i = 0; i < H1_E_list.Count; i++)
        {
            H1_E_list[i].CasualTo = H2_list.ToArray();
        }
        for (var i = 0; i < H2_E_list.Count; i++)
        {
            H2_E_list[i].CasualTo = H3_list.ToArray();
        }        
        for (var i = 0; i < H3_E_list.Count; i++)
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
    public IDictionary<string, SkillEntity> GenerateBehaviourSets()
    {
        IDictionary<string, SkillEntity> _SEDic = new Dictionary<string, SkillEntity>();
        var StateTransitionSetList = new List<SkillEntity>();
        
        Empty = new SkillEntity("Empty", 0, 0, 0, 0, new AIAttrs(), null, null, InputKey.Null, InputKey.Null, -1, 0);
        zhuangbi = new SkillEntity("zhuangbi", 0, 0, 0, 0, new AIAttrs(), null, null, InputKey.Null, InputKey.Null, -1, 0);
        Victory = new SkillEntity("Victory",0, 0, 0, 0, new AIAttrs(), null, null, InputKey.Null, InputKey.Null, -1, 0);
        Death = new SkillEntity("Death", 0, 0, 0, 0, new AIAttrs(), null, null, InputKey.Null, InputKey.Null, -1, 0);
        Hit = new SkillEntity("Hit", 0, BehaviorType.Hit, 0, 0,new AIAttrs(), H1_list.ToArray(),null,InputKey.Null, InputKey.Null, -1, 0);
        getUp = new SkillEntity("getUp", 0, BehaviorType.GetUp, 0, 0, new AIAttrs(), H1_list.ToArray(), null, InputKey.Any, InputKey.Null, -1, 0);
        KnockOff = new SkillEntity("KnockOff", 0, BehaviorType.KnockOff, 0, 0, new AIAttrs(), R != null ? new string[] { R.REAL_NAME } : new string[] {}, null, InputKey.Null, InputKey.Null, -1, 0);
        if (FightGlobalSetting._hasDefend)
        {
            D.CasualTo = H1_list.ToArray();
            StateTransitionSetList.Add(D);
        }
        
        StateTransitionSetList.Add(getUp);
        StateTransitionSetList.Add(KnockOff);
        StateTransitionSetList.Add(Empty);
        StateTransitionSetList.Add(zhuangbi);
        StateTransitionSetList.Add(Victory);
        StateTransitionSetList.Add(Death);
        StateTransitionSetList.Add(Hit);        
        StateTransitionSetList.Add(M);
        
        if (D != null && FightGlobalSetting._hasDefend)
        {
            StateTransitionSetList.Add(D);
        }
        if (R != null)
        {
            StateTransitionSetList.Add(R);
        }

        if(A1 != null)
        {
            StateTransitionSetList.Add(A1);
        }
        if (A2 != null)
        {
            StateTransitionSetList.Add(A2);
        }            
        if (A3 != null)
        {
            StateTransitionSetList.Add(A3);
        }
        if (B1 != null)
        {
            StateTransitionSetList.Add(B1);
        }            
        if (B2 != null)
        {
            StateTransitionSetList.Add(B2);
        }
        if (B3 != null)
        {
            StateTransitionSetList.Add(B3);
        }
        if (C1 != null)
        {
            StateTransitionSetList.Add(C1);
        }                    
        if (C2 != null)
        {
            StateTransitionSetList.Add(C2);
        }            
        if (C3 != null)
        {
            StateTransitionSetList.Add(C3);
        }
        
        foreach (var _SE in StateTransitionSetList)
        {
            if (_SE != M && _SE != KnockOff && _SE != Empty && _SE != Death && _SE != Victory)
            {
                var toOptions = _SE.CasualTo.ToList();
                if (!toOptions.Contains(M.REAL_NAME))
                {
                    toOptions.Add(M.REAL_NAME);
                }
                _SE.CasualTo = toOptions.ToArray();
            }
            if (_SE.REAL_NAME != null && !_SEDic.ContainsKey(_SE.REAL_NAME))
            {
                _SEDic.Add(new KeyValuePair<string, SkillEntity>(_SE.REAL_NAME, _SE));
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
    SkillEntity GetSE(string skillId, float level)
    {
        var SC = SkillConfigTable.GetSkillConfig(skillId);
        if (SC == null)
        {
            return null;
        }
        
        if (!string.IsNullOrEmpty(SC.REAL_NAME))
        {
            var _SE = new SkillEntity(
                SC.RECORD_ID,
                SC.REAL_NAME,
                0,
                SC.STATE_TYPE,
                SkillEntity.ATCal(SC.ATTACK_WEIGHT, level),
                SkillEntity.StoneHpCal(SC.HP_WEIGHT, level),
                SC.AIAttrs,
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
}