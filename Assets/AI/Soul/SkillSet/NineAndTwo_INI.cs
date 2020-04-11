using System.Collections.Generic;
using UnityEngine;
using Skill;

public partial class NineAndTwo
{
    Behavior_Transition_Set A1, A2, A3, B1, B2, B3, C1, C2, C3, D, M, R;
    List<Behavior_Transition_Set> StateTransitionSetList;//这个的作用是发生在StateDictionary的生成阶段。见AIStateRunner之FormFightingSetsByNineAndTwo
    List<Behavior_Transition_Set> H1_list = new List<Behavior_Transition_Set>();
    List<Behavior_Transition_Set> H2_list = new List<Behavior_Transition_Set>();
    List<Behavior_Transition_Set> H3_list = new List<Behavior_Transition_Set>();
    
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

        A1 = AConfig1 != null ? FromConfigToSTS(AConfig1, A1level) : null;
        A2 = AConfig2 != null ? FromConfigToSTS(AConfig2, A2level) : null;
        A3 = AConfig3 != null ? FromConfigToSTS(AConfig3, A3level) : null;

        B1 = BConfig1 != null ? FromConfigToSTS(BConfig1, B1level) : null;
        B2 = BConfig2 != null ? FromConfigToSTS(BConfig2, B2level) : null;
        B3 = BConfig3 != null ? FromConfigToSTS(BConfig3, B3level) : null;

        C1 = CConfig1 != null ? FromConfigToSTS(CConfig1, C1level) : null;
        C2 = CConfig2 != null ? FromConfigToSTS(CConfig2, C2level) : null;
        C3 = CConfig3 != null ? FromConfigToSTS(CConfig3, C3level) : null;
        
        ////////////  关于DMR 的处理，和角色本身被动有关，有别于现在的9宫  ////////////
        PassiveSkillConfigs passiveSkillConfigs = new PassiveSkillConfigs(moveType,canDefend,rushType);
        DConfig = passiveSkillConfigs.DConfig;
        MConfig = passiveSkillConfigs.MConfig;
        RConfig = passiveSkillConfigs.RConfig;
        if (DConfig != null)
        {
            D = FromConfigToSTS(DConfig,0);
            D.enterInput = Inputs_defined.Defend;
            D.exitInput = Inputs_defined.Defend_Cancel;
        }else{
            D = null;
        }

        if (MConfig != null)
        {
            M = FromConfigToSTS(MConfig,0);
        }
        else
        {
            //之后的GenerateBeheviourSets环节如果检测到缺乏移动，会添加默认move状态
            //M怎么也不能是null。
        }
        if (RConfig != null)
        {
            R = FromConfigToSTS(RConfig,0);
            R.enterInput = Inputs_defined.Acc;
        }
        else
        {
            R = null;
        }
        //////////////////////////////////////////////////////////////////////////
        
        H1_list.Clear();
        H2_list.Clear();
        H3_list.Clear();

        if (A1 != null)
        {
            H1_list.Add(A1);
            A1.enterInput = Inputs_defined.Attack;
            A1.exitInput = Inputs_defined.Null;
        }
        if (A2 != null)
        {
            H2_list.Add(A2);
            A2.enterInput = Inputs_defined.Attack;
            A2.exitInput = Inputs_defined.Null;
        }
        if (A3 != null)
        {
            H3_list.Add(A3);
            A3.enterInput = Inputs_defined.Attack;
            A3.exitInput = Inputs_defined.Null;
        }

        if (B1 != null)
        {
            H1_list.Add(B1);
            B1.enterInput = Inputs_defined.Fire1;
            B1.exitInput = Inputs_defined.Null;
        }
        if (B2 != null)
        {
            H2_list.Add(B2);
            B2.enterInput = Inputs_defined.Fire1;
            B2.exitInput = Inputs_defined.Null;
        }
        if (B3 != null)
        {
            H3_list.Add(B3);
            B3.enterInput = Inputs_defined.Fire1;
            B3.exitInput = Inputs_defined.Null;
        }

        if (C1 != null)
        {
            H1_list.Add(C1);
            C1.enterInput = Inputs_defined.Fire2;
            C1.exitInput = Inputs_defined.Null;
        }
        if (C2 != null)
        {
            H2_list.Add(C2);
            C2.enterInput = Inputs_defined.Fire2;
            C2.exitInput = Inputs_defined.Null;
        }
        if (C3 != null)
        {
            H3_list.Add(C3);
            C3.enterInput = Inputs_defined.Fire2;
            C3.exitInput = Inputs_defined.Null;
        }
        
        // --
        if (R != null)
        {
            H1_list.Add(R);
            H2_list.Add(R);
            H3_list.Add(R);
        }
        if (D != null)
        {
            H1_list.Add(D);
            H2_list.Add(D);
            H3_list.Add(D);
        }

        for (int i = 0; i < H1_list.Count; i++)
        {
            H1_list[i].Casual_To_Behaviours = H2_list.ToArray();
        }
        
        for (int i = 0; i < H2_list.Count; i++)
        {
            H2_list[i].Casual_To_Behaviours = H3_list.ToArray();
        }
        
        for (int i = 0; i < H3_list.Count; i++)
        {
            H3_list[i].Casual_To_Behaviours = H1_list.ToArray();
        }
        
        if (D != null)
            D.Casual_To_Behaviours = H1_list.ToArray();
        if (R != null)
            R.Casual_To_Behaviours = H1_list.ToArray();
    }
    
    // FormFightingSetsByNineAndTwo(string type,NineAndTwo nineAndTwo, passiveSkillConfigs passiveSkillConfigs, int AI_level) -->
    // 1.sortNineAndTwo(passiveSkillConfigs):整理三连击的连续关系。根据数据库配置好相应技能的属性。
    // 2.GenerateBeheviourSets():正式配置各State_Transition_Set，并且适配好所有技能组的force和casual迁移。
    public IDictionary<string, Behavior_Transition_Set> GenerateBeheviourSets()
    {
        IDictionary<string, Behavior_Transition_Set> state_Transition_Dictionary = new Dictionary<string, Behavior_Transition_Set>();
        StateTransitionSetList = new List<Behavior_Transition_Set>();

        Behavior_Transition_Set Empty = new Behavior_Transition_Set("Empty", 0, 0, 0, 0, null, null, Inputs_defined.Null, Inputs_defined.Null, 0, 0);

        Behavior_Transition_Set Victory = new Behavior_Transition_Set("Victory",0, 0, 0, 0, null, null, Inputs_defined.Null, Inputs_defined.Null, 0, 0);

        Behavior_Transition_Set Death = new Behavior_Transition_Set("Death",0, 0, 0, 0, null, null, Inputs_defined.Null, Inputs_defined.Null, 0, 0);

        Behavior_Transition_Set Defend = new Behavior_Transition_Set("Defend",BehaviorType.Def,0,0,0,H1_list.ToArray(),null,Inputs_defined.Defend, Inputs_defined.Defend_Cancel,0,0);
        
        Behavior_Transition_Set Move = new Behavior_Transition_Set("Move_normal",BehaviorType.NONE,0,0,0,H1_list.ToArray(),null,Inputs_defined.Null, Inputs_defined.Null,0,0);

        Behavior_Transition_Set Hit = new Behavior_Transition_Set("Hit",BehaviorType.Hit,0,0,0,H1_list.ToArray(),null,Inputs_defined.Null, Inputs_defined.Null,0,0);
                                                            
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
            this.R.stateType = BehaviorType.AC;
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
        
        if (this.M != null)
        {
            //下面这些就是怕数据库里九宫格里的M记载有错。
            M.SPLevel = -1;
            M.Casual_To_Behaviours = H1_list.ToArray();
            StateTransitionSetList.Add(M);
        }
        else
        {
            StateTransitionSetList.Add(Move);// 这个地方是说，要么你自定义移动类状态，要么加默认移动状态。因为移动状态其实可能根据角色被动而不同。
        }
        
        Behavior_Transition_Set getUp = new Behavior_Transition_Set("getUp",BehaviorType.GetUp,0,0,0,H1_list.ToArray(),null,Inputs_defined.Any, Inputs_defined.Null,0,0);
        Behavior_Transition_Set KnockOff = new Behavior_Transition_Set("KnockOff",BehaviorType.KnockOff,0,0,0,new Behavior_Transition_Set[]{ getUp },null,Inputs_defined.Null, Inputs_defined.Null,0,0);
        StateTransitionSetList.Add(getUp);
        StateTransitionSetList.Add(KnockOff);
        
        //从下面这个地方可以看到我们需要在sort阶段把RMD全部准备好，而且必须是要么为null要么是一个完整STS信息。
        foreach (Behavior_Transition_Set _State_Transition_Set in StateTransitionSetList)
        {
            if (_State_Transition_Set.StateKey != null && !state_Transition_Dictionary.ContainsKey(_State_Transition_Set.StateKey))
            {
                state_Transition_Dictionary.Add(new KeyValuePair<string, Behavior_Transition_Set>(_State_Transition_Set.StateKey,_State_Transition_Set));
            }
            else
            {
                if (_State_Transition_Set.StateKey == null)
                {
                    Debug.Log("键值为空？？");
                }else{
                    Debug.Log("角色自身技能产生键值重复："+_State_Transition_Set.StateKey);
                }
            }
        }
        return state_Transition_Dictionary;
    }
    
    // 这个应该是所谓技能等级的着手点
    Behavior_Transition_Set FromConfigToSTS(SkillConfig _SC, int level)
    {
        if (_SC.RECORD_ID != null)
        {
            SkillConfigTable.Instance.SkillConfigDicForReference.TryGetValue(_SC.RECORD_ID, out SkillConfig refSkillConfig);
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

        Behavior_Transition_Set STS = null;
        if (_SC != null && !string.IsNullOrEmpty(_SC.REAL_NAME))
        {
            STS = new Behavior_Transition_Set(_SC.REAL_NAME,
                                           _SC.STATE_TYPE,
                                           ATCal(_SC.ATTACK_WEIGHT,level),
                                           _SC.AI_MIN_DIS,
                                           _SC.AI_MAX_DIS,
                                            null,
                                            null,
                                           Inputs_defined.Null,
                                           Inputs_defined.Null,
                                           _SC.SP_LEVEL,
                                           _SC.RARITY_LEVEL);
            return STS;
        }
        return STS;
    }
    
    SkillConfig FixConfigByReference(string skillid)
    {
        if (skillid == null)
        {
            return new SkillConfig();
        }
        SkillConfigTable.Instance.SkillConfigDicForReference.TryGetValue(skillid, out SkillConfig referenceStandardSkillConfig);
        return referenceStandardSkillConfig;
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
