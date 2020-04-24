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
        AConfig1 = A1skillid != null ? FixConfigByReference(A1skillid) : new SkillConfig();
        AConfig2 = A2skillid != null ? FixConfigByReference(A2skillid) : new SkillConfig();
        AConfig3 = A3skillid != null ? FixConfigByReference(A3skillid) : new SkillConfig();
        BConfig1 = B1skillid != null ? FixConfigByReference(B1skillid) : new SkillConfig();
        BConfig2 = B2skillid != null ? FixConfigByReference(B2skillid) : new SkillConfig();
        BConfig3 = B3skillid != null ? FixConfigByReference(B3skillid) : new SkillConfig();
        CConfig1 = C1skillid != null ? FixConfigByReference(C1skillid) : new SkillConfig();
        CConfig2 = C2skillid != null ? FixConfigByReference(C2skillid) : new SkillConfig();
        CConfig3 = C3skillid != null ? FixConfigByReference(C3skillid) : new SkillConfig();
        
        A1 = AConfig1 != null ? FromConfigToEntity(AConfig1, A1level) : null;
        A2 = AConfig2 != null ? FromConfigToEntity(AConfig2, A2level) : null;
        A3 = AConfig3 != null ? FromConfigToEntity(AConfig3, A3level) : null;
        
        B1 = BConfig1 != null ? FromConfigToEntity(BConfig1, B1level) : null;
        B2 = BConfig2 != null ? FromConfigToEntity(BConfig2, B2level) : null;
        B3 = BConfig3 != null ? FromConfigToEntity(BConfig3, B3level) : null;
        
        C1 = CConfig1 != null ? FromConfigToEntity(CConfig1, C1level) : null;
        C2 = CConfig2 != null ? FromConfigToEntity(CConfig2, C2level) : null;
        C3 = CConfig3 != null ? FromConfigToEntity(CConfig3, C3level) : null;
        
        ////////////  关于DMR 的处理，和角色本身被动有关，有别于现在的9宫  ////////////
        PassiveSkillConfigs passiveSkillConfigs = new PassiveSkillConfigs(moveType, canDefend, rushType);
        DConfig = passiveSkillConfigs.DConfig;
        MConfig = passiveSkillConfigs.MConfig;
        RConfig = passiveSkillConfigs.RConfig;
        if (DConfig != null)
        {
            D = FromConfigToEntity(DConfig,0);
            D.EnterInput = InputKey.Defend;
            D.ExitInput = InputKey.Defend_Cancel;
        }else{
            D = null;
        }

        if (MConfig != null)
        {
            M = FromConfigToEntity(MConfig,0);
        }
        else
        {
            //之后的GenerateBeheviourSets环节如果检测到缺乏移动，会添加默认move状态 M怎么也不能是null。
        }
        
        if (RConfig != null)
        {
            R = FromConfigToEntity(RConfig,0);
            R.EnterInput = InputKey.Acc;
        }
        else
        {
            R = null;
        }
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
        
        SkillEntity Empty = new SkillEntity("Empty", 0, 0, 0, 0, 0, null, null, InputKey.Null, InputKey.Null, 0, 0);
        SkillEntity Victory = new SkillEntity("Victory",0, 0, 0, 0, 0, null, null, InputKey.Null, InputKey.Null, 0, 0);
        SkillEntity Death = new SkillEntity("Death", 0, 0, 0, 0, 0, null, null, InputKey.Null, InputKey.Null, 0, 0);
        SkillEntity Defend = new SkillEntity("Defend", 0,BehaviorType.Def,0,0,0,H1_list.ToArray(),null,InputKey.Defend, InputKey.Defend_Cancel,0,0);
        SkillEntity Move = new SkillEntity("Move_normal", 0, BehaviorType.NONE,0,0,0,H1_list.ToArray(),null,InputKey.Null, InputKey.Null,0,0);
        SkillEntity Hit = new SkillEntity("Hit", 0, BehaviorType.Hit,0,0,0,H1_list.ToArray(),null,InputKey.Null, InputKey.Null,0,0);
                                                            
        StateTransitionSetList.Add(Empty);
        StateTransitionSetList.Add(Victory);
        StateTransitionSetList.Add(Death);
        StateTransitionSetList.Add(Hit);
        
        if (this.D != null)
        {
            StateTransitionSetList.Add(Defend);//这里的逻辑是这样：如果在sortNineAndTwo执行后，this.D不是null，那说明角色有防御状态，而防御状态是固定的。
        }
        if (this.R != null)
        {
            this.R.StateType = BehaviorType.AC;
            StateTransitionSetList.Add(R);//这个是只能根据角色被动来。
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
        
        if (this.M != null) //怕数据库里九宫格里的M记载有错。
        {
            M.SP_LEVEL = -1;
            M.CasualTo = H1_list.ToArray();
            StateTransitionSetList.Add(M);
        }
        else
        {
            StateTransitionSetList.Add(Move);// 这个地方是说，要么你自定义移动类状态，要么加默认移动状态。因为移动状态其实可能根据角色被动而不同。
        }
        
        SkillEntity getUp = new SkillEntity("getUp", 0, BehaviorType.GetUp, 0, 0, 0, H1_list.ToArray(), null, InputKey.Any, InputKey.Null,0,0);
        SkillEntity KnockOff = new SkillEntity("KnockOff", 0, BehaviorType.KnockOff,0,0,0,new string[] { "getUp" },null,InputKey.Null, InputKey.Null,0,0);
        StateTransitionSetList.Add(getUp);
        StateTransitionSetList.Add(KnockOff);
        
        //从下面这个地方可以看到我们需要在sort阶段把RMD全部准备好，而且必须是要么为null要么是一个完整STS信息。
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
    SkillEntity FromConfigToEntity(SkillConfig _SC, int level)
    {
        if (_SC.RECORD_ID != null)
        {
            SkillConfigTable.Instance.SkillConfigRefDic.TryGetValue(_SC.RECORD_ID, out SkillConfig refSkillConfig);
            if (refSkillConfig != null)
            {
                _SC.REAL_NAME = refSkillConfig.REAL_NAME;
                _SC.SHOW_NAME = refSkillConfig.SHOW_NAME;
                _SC.AI_MIN_DIS = refSkillConfig.AI_MIN_DIS;
                _SC.AI_MAX_DIS = refSkillConfig.AI_MAX_DIS;
                _SC.SP_LEVEL = refSkillConfig.SP_LEVEL;
                _SC.STATE_TYPE = refSkillConfig.STATE_TYPE;
            }
        }
        else
        {
            //防御，受伤等固定技能，他们没有id，直接放行来进行之后的处理。
        }

        SkillEntity _SE = null;
        if (_SC != null && !string.IsNullOrEmpty(_SC.REAL_NAME))
        {
            _SE = new SkillEntity(
                _SC.REAL_NAME,
                0,
                _SC.STATE_TYPE,
                ATCal(_SC.ATTACK_WEIGHT,level),
                _SC.AI_MIN_DIS,
                _SC.AI_MAX_DIS,
                null,
                null,
                InputKey.Null,
                InputKey.Null,
                _SC.SP_LEVEL,
                _SC.RARITY_LEVEL
            );
        }
        return _SE;
    }
    
    SkillConfig FixConfigByReference(string skillid)
    {
        if (skillid == null)
        {
            return new SkillConfig();
        }
        SkillConfigTable.Instance.SkillConfigRefDic.TryGetValue(skillid, out SkillConfig REF);
        return REF;
    }
    
    // 900血，10攻击力，1打1的话接近40秒左右游戏结束。但如果存在大量远距离对火立回那么就不太好说这个时间。。
    // 那么level 是1的情况下，攻击力是1
    // 从而在技能定义表里，技能标准攻击值应该是1，存在超迅速多连击的情况多半应该少于1，而一些比较赌的重攻击则是大于1
    public static float ATCal(float originAT,int level)
    {
        return originAT * (10 + level) / 11;
    }
    
    // HP和攻击力等比缩放。攻击是从1涨到10，HP是从10涨到100
    public static float StoneHpCal(int level)
    {
        return 10 * (10 + level) / 11;
    }
}