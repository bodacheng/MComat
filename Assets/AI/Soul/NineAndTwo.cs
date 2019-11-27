using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif


//这个模块也将扮演数据库和AI模块接口的作用。
// 玩家存档中的各个角色信息最后会转化出这样一个类的实例。从而很重要一点————要看明白哪些信息是能保存数据库的。
// 实际上D,M,R按照现在的企划看全是角色被动，那么原则上他们确实不应该和其他技能登陆在一个技能配置文件里，也不需要有对应ID
// 既然D,M,R是被动，那按理说九宫格信息的各种处理应该是在角色读取前执行，先决定DMR,再批处理12宫技能。

[System.Serializable]
public class NineAndTwo {

    public int level;

    public string A1skillid, A2skillid, A3skillid;
    public string B1skillid, B2skillid, B3skillid;
    public string C1skillid, C2skillid, C3skillid;

    // 以下这三条，出于本地关卡信息的考虑，也放在这里。但是一般来说这些是由角色自身被动决定。所以存在索引角色被动和依据九宫格固有信息这两种读取方式。
    public bool canDefend;
    public MoveType moveType;
    public RushType rushType;

    private SkillConfig AConfig1, AConfig2, AConfig3, BConfig1, BConfig2, BConfig3, CConfig1, CConfig2, CConfig3;
    private SkillConfig DConfig, MConfig, RConfig;//这三个其实对应了monster表里定义的三个被动技能
    private State_Transition_Set A1, A2, A3, B1, B2, B3, C1, C2, C3, D, M, R;
    private List<State_Transition_Set> StateTransitionSetList;//这个的作用是发生在StateDictionary的生成阶段。见AIStateRunner之FormFightingSetsByNineAndTwo

    public State_Transition_Set GetD_STS()
    {
        return D;
    }
    public State_Transition_Set GetM_STS()
    {
        return M;
    }
    public State_Transition_Set GetR_STS()
    {
        return R;
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

    private NineAndTwo Clone()
    {
        return (NineAndTwo)MemberwiseClone();
    }

    public NineAndTwo DeepCopy()
    {
        NineAndTwo Copy = this.Clone();

        Copy.AConfig1 = Copy.AConfig1 != null ? Copy.AConfig1.Clone() : new SkillConfig();
        Copy.AConfig2 = Copy.AConfig2 != null ? Copy.AConfig2.Clone() : new SkillConfig();
        Copy.AConfig3 = Copy.AConfig3 != null ? Copy.AConfig3.Clone() : new SkillConfig();
        Copy.BConfig1 = Copy.BConfig1 != null ? Copy.BConfig1.Clone() : new SkillConfig();
        Copy.BConfig2 = Copy.BConfig2 != null ? Copy.BConfig2.Clone() : new SkillConfig();
        Copy.BConfig3 = Copy.BConfig3 != null ? Copy.BConfig3.Clone() : new SkillConfig();
        Copy.CConfig1 = Copy.CConfig1 != null ? Copy.CConfig1.Clone() : new SkillConfig();
        Copy.CConfig2 = Copy.CConfig2 != null ? Copy.CConfig2.Clone() : new SkillConfig();
        Copy.CConfig3 = Copy.CConfig3 != null ? Copy.CConfig3.Clone() : new SkillConfig();
        Copy.DConfig = Copy.DConfig != null ? Copy.DConfig.Clone() : new SkillConfig();
        Copy.MConfig = Copy.MConfig != null ? Copy.MConfig.Clone() : new SkillConfig();
        Copy.RConfig = Copy.RConfig != null ? Copy.RConfig.Clone() : new SkillConfig();

        return Copy;
    }

    public NineAndTwo()
    {
        level = 1;

        A1skillid = null; A2skillid = null; A3skillid = null;
        B1skillid = null; B2skillid = null; B3skillid = null;
        C1skillid = null; C2skillid = null; C3skillid = null;

        this.moveType = MoveType.Test;
        this.canDefend = false;
        this.rushType = RushType.None;

        AConfig1 = new SkillConfig();
        AConfig2 = new SkillConfig();
        AConfig3 = new SkillConfig();
        BConfig1 = new SkillConfig();
        BConfig2 = new SkillConfig();
        BConfig3 = new SkillConfig();
        CConfig1 = new SkillConfig();
        CConfig2 = new SkillConfig();
        CConfig3 = new SkillConfig();
        DConfig = new SkillConfig();
        MConfig = new SkillConfig();
        RConfig = new SkillConfig();
    }
    
    public NineAndTwo(MoveType moveType,bool canDefend, RushType rushType)
    {
        level = 1;

        A1skillid = null; A2skillid = null; A3skillid = null;
        B1skillid = null; B2skillid = null; B3skillid = null;
        C1skillid = null; C2skillid = null; C3skillid = null;

        this.moveType = moveType;
        this.canDefend = canDefend;
        this.rushType = rushType;

        AConfig1 = new SkillConfig();
        AConfig2 = new SkillConfig();
        AConfig3 = new SkillConfig();
        BConfig1 = new SkillConfig();
        BConfig2 = new SkillConfig();
        BConfig3 = new SkillConfig();
        CConfig1 = new SkillConfig();
        CConfig2 = new SkillConfig();
        CConfig3 = new SkillConfig();
        DConfig = new SkillConfig();
        MConfig = new SkillConfig();
        RConfig = new SkillConfig();
    }

    public List<State_Transition_Set> ReturnSTSlist()
    {
        return StateTransitionSetList;
    }

    private SkillConfig FixConfigByReference(string skillid)
    {
        if (skillid == null)
            return null;
        SkillConfigTable.Instance.SkillConfigDicForReference.TryGetValue(skillid, out SkillConfig referenceStandardSkillConfig);
        return referenceStandardSkillConfig;
    }

    private State_Transition_Set FromConfigToSTS(SkillConfig _SkillConfig)
    {
        if (_SkillConfig == null)
            return null;

        if (_SkillConfig.RECORD_ID != null)
        {
            SkillConfigTable.Instance.SkillConfigDicForReference.TryGetValue(_SkillConfig.RECORD_ID, out SkillConfig referenceStandardSkillConfig);
            if (referenceStandardSkillConfig != null)
            {
                _SkillConfig.REAL_NAME = referenceStandardSkillConfig.REAL_NAME;
                _SkillConfig.ShowName = referenceStandardSkillConfig.ShowName;
                _SkillConfig.ai_trigger_ranges = referenceStandardSkillConfig.ai_trigger_ranges;
                _SkillConfig.SP_LEVEL = referenceStandardSkillConfig.SP_LEVEL;
                _SkillConfig.stateType = referenceStandardSkillConfig.stateType;
            }
        }
        else
        {
            //防御，受伤等固定技能，他们没有id，直接放行来进行之后的处理。
        }

        State_Transition_Set STS = null;
        if (_SkillConfig != null && _SkillConfig.REAL_NAME != null && _SkillConfig.REAL_NAME != "")
        {
            try
            {
                STS = new State_Transition_Set(_SkillConfig.REAL_NAME,
                                               _SkillConfig.stateType,
                                               _SkillConfig.ATTACK_WEIGHT,
                                               _SkillConfig.ai_trigger_ranges,
                                                null,
                                                null,
                                               Inputs_defined.Null, 
                                               Inputs_defined.Null,
                                               _SkillConfig.SP_LEVEL,
                                               int.Parse(_SkillConfig.AI_PRIORITY),
                                               _SkillConfig.RARITY_LEVEL);
                return STS;
            }
            catch(Exception e)
            {
                Debug.Log(e);
                return null;
            }
        }
        else
        {
            return STS;
        }
    }

    // 为了正确表现DMR和其他状态的处理顺序，这个函数应该把DMR的config作为参数。表示这几个是被动，要附加在其他技能存档上执行。
    public void SortNineAndTwo()
    {
        // 如果本地配置文件没有加载正确上面这个环节就要出问题。D，M，R不需要进行上述操作，
        // 理由是这三者有固定性，而且所依靠的动画包是基础动画包而不是各type角色的攻击技能动画包，所以加载方式有不同的地方。

        AConfig1 = A1skillid != null ? FixConfigByReference(A1skillid) : null;
        AConfig2 = A2skillid != null ? FixConfigByReference(A2skillid) : null;
        AConfig3 = A3skillid != null ? FixConfigByReference(A3skillid) : null;
        BConfig1 = B1skillid != null ? FixConfigByReference(B1skillid) : null;
        BConfig2 = B2skillid != null ? FixConfigByReference(B2skillid) : null;
        BConfig3 = B3skillid != null ? FixConfigByReference(B3skillid) : null;
        CConfig1 = C1skillid != null ? FixConfigByReference(C1skillid) : null;
        CConfig2 = C2skillid != null ? FixConfigByReference(C2skillid) : null;
        CConfig3 = C3skillid != null ? FixConfigByReference(C3skillid) : null;

        A1 = AConfig1 != null ? FromConfigToSTS(AConfig1) : null;
        A2 = AConfig2 != null ? FromConfigToSTS(AConfig2) : null;
        A3 = AConfig3 != null ? FromConfigToSTS(AConfig3) : null;

        B1 = BConfig1 != null ? FromConfigToSTS(BConfig1) : null;
        B2 = BConfig2 != null ? FromConfigToSTS(BConfig2) : null;
        B3 = BConfig3 != null ? FromConfigToSTS(BConfig3) : null;

        C1 = CConfig1 != null ? FromConfigToSTS(CConfig1) : null;
        C2 = CConfig2 != null ? FromConfigToSTS(CConfig2) : null;
        C3 = CConfig3 != null ? FromConfigToSTS(CConfig3) : null;

        ////////////  关于DMR 的处理，和角色本身被动有关，有别于现在的9宫  ///////////////////

        PassiveSkillConfigs passiveSkillConfigs = new PassiveSkillConfigs(this.moveType,this.canDefend,this.rushType);

        this.DConfig = passiveSkillConfigs.DConfig;
        this.MConfig = passiveSkillConfigs.MConfig;
        this.RConfig = passiveSkillConfigs.RConfig;

        if (this.DConfig != null)
        {
            this.D = FromConfigToSTS(this.DConfig);
            this.D.enterInput = Inputs_defined.Defend;
            this.D.exitInput = Inputs_defined.Defend_Cancel;
        }else{
            this.D = null;
        }

        if (this.MConfig != null)
        {
            this.M = FromConfigToSTS(this.MConfig);
        }
        else
        {
            //之后的GenerateBeheviourSets环节如果检测到缺乏移动，会添加默认move状态
            //M怎么也不能是null。
        }

        if (this.RConfig != null)
        {
            this.R = FromConfigToSTS(this.RConfig);
            this.R.enterInput = Inputs_defined.Dash;
        }
        else
        {
            this.R = null;
        }

        //////////////////////////////////////////////////////////////////////////////
        List<State_Transition_Set> H1_list = new List<State_Transition_Set>();
        List<State_Transition_Set> H2_list = new List<State_Transition_Set>();
        List<State_Transition_Set> H3_list = new List<State_Transition_Set>();

        List<State_Transition_Set> A_list = new List<State_Transition_Set>();
        List<State_Transition_Set> B_list = new List<State_Transition_Set>();
        List<State_Transition_Set> C_list = new List<State_Transition_Set>();

        if (A1 != null)
        {
            A_list.Add(A1);
            H1_list.Add(A1);
        }
        if (A2 != null)
        {
            A_list.Add(A2);
            H2_list.Add(A2);
        }
        if (A3 != null)
        {
            A_list.Add(A3);
            H3_list.Add(A3);
        }

        if (B1 != null)
        {
            B_list.Add(B1);
            H1_list.Add(B1);
        }
        if (B2 != null)
        {
            B_list.Add(B2);
            H2_list.Add(B2);
        }
        if (B3 != null)
        {
            B_list.Add(B3);
            H3_list.Add(B3);
        }

        if (C1 != null)
        {
            C_list.Add(C1);
            H1_list.Add(C1);
        }
        if (C2 != null)
        {
            C_list.Add(C2);
            H2_list.Add(C2);
        }
        if (C3 != null)
        {
            C_list.Add(C3);
            H3_list.Add(C3);
        }

        for (int i = 0; i < A_list.Count; i++)
        {
            List<State_Rate_Set> casualT0 = new List<State_Rate_Set>();
            if (i == 0)
            {
                A_list[i].enterInput = Inputs_defined.Attack;
                A_list[i].exitInput = Inputs_defined.Null;
            }else{
                A_list[i].enterInput = Inputs_defined.Null;
                A_list[i].exitInput = Inputs_defined.Null;
            }

            if (i + 1 < A_list.Count)
            {
                State_Rate_Set State_Rate_Set =
                    new State_Rate_Set(
                    A_list[i + 1].StateKey,
                    A_list[i + 1].stateType,
                    A_list[i + 1].AT,
                    A_list[i + 1].ai_trigger_ranges,
                    true, 
                    Inputs_defined.Attack, Inputs_defined.Null, A_list[i + 1].SPLevel,
                    A_list[i + 1].skillEmergentLevel);
                casualT0.Add(State_Rate_Set);
            }

            switch (i)
            {
                case 0://查H2
                    for (int y = 0; y < H2_list.Count; y++)
                    {
                        if (!A_list.Contains(H2_list[y]))//每个横行由上面的代码处理。
                        {
                            Inputs_defined casualtokey = Inputs_defined.Null;
                            if (B_list.Contains(H2_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire1;
                            }
                            if (C_list.Contains(H2_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire2;
                            }

                            State_Rate_Set State_Rate_Set =
                                new State_Rate_Set(
                                H2_list[y].StateKey,
                                H2_list[y].stateType,
                                H2_list[y].AT,
                                H2_list[y].ai_trigger_ranges,
                                true,
                                casualtokey, Inputs_defined.Null, 
                                H2_list[y].SPLevel,
                                H2_list[y].skillEmergentLevel);

                            casualT0.Add(State_Rate_Set);
                        }
                    }
                    break;
                case 1://查H3
                    for (int y = 0; y < H3_list.Count; y++)
                    {
                        if (!A_list.Contains(H3_list[y]))//每个横行由上面的代码处理。
                        {
                            Inputs_defined casualtokey = Inputs_defined.Null;
                            if (B_list.Contains(H3_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire1;
                            }
                            if (C_list.Contains(H3_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire2;
                            }

                            State_Rate_Set State_Rate_Set =
                                new State_Rate_Set(
                                H3_list[y].StateKey,
                                H3_list[y].stateType,
                                H3_list[y].AT,
                                H3_list[y].ai_trigger_ranges,
                                true,
                                casualtokey, Inputs_defined.Null,
                                H3_list[y].SPLevel,
                                H3_list[y].skillEmergentLevel);

                            casualT0.Add(State_Rate_Set);
                        }
                    }
                    break;
                case 2:
                    break;
            }

            if (this.R != null)
                casualT0.Add(this.R.GetStateRateSet());
            if (this.D != null)
                casualT0.Add(this.D.GetStateRateSet());

            A_list[i].casual_to_state_Sets = casualT0.ToArray();
        }

        //////////////
        for (int i = 0; i < B_list.Count; i++)
        {
            List<State_Rate_Set> casualT0 = new List<State_Rate_Set>();
            if (i == 0)
            {
                B_list[i].enterInput = Inputs_defined.Fire1;
                B_list[i].exitInput = Inputs_defined.Null;
            }
            else
            {
                B_list[i].enterInput = Inputs_defined.Null;
                B_list[i].exitInput = Inputs_defined.Null;
            }

            if (i + 1 < B_list.Count)
            {
                State_Rate_Set State_Rate_Set =
                    new State_Rate_Set(
                    B_list[i + 1].StateKey,
                    B_list[i + 1].stateType,
                    B_list[i + 1].AT,
                    B_list[i + 1].ai_trigger_ranges,
                    true, Inputs_defined.Fire1, Inputs_defined.Null, 
                    B_list[i + 1].SPLevel,
                    B_list[i + 1].skillEmergentLevel);
                casualT0.Add(State_Rate_Set);
            }

            switch (i)
            {
                case 0://查H2
                    for (int y = 0; y < H2_list.Count; y++)
                    {
                        if (!B_list.Contains(H2_list[y]))//每个横行由上面的代码处理。
                        {
                            Inputs_defined casualtokey = Inputs_defined.Null;
                            if (A_list.Contains(H2_list[y]))
                            {
                                casualtokey = Inputs_defined.Attack;
                            }
                            if (C_list.Contains(H2_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire2;
                            }

                            State_Rate_Set State_Rate_Set =
                                new State_Rate_Set(
                                H2_list[y].StateKey,
                                H2_list[y].stateType,
                                H2_list[y].AT,
                                H2_list[y].ai_trigger_ranges,
                                true,
                                casualtokey, Inputs_defined.Null,
                                H2_list[y].SPLevel,
                                H2_list[y].skillEmergentLevel);

                            casualT0.Add(State_Rate_Set);
                        }
                    }
                    break;
                case 1://查H3
                    for (int y = 0; y < H3_list.Count; y++)
                    {
                        if (!B_list.Contains(H3_list[y]))//每个横行由上面的代码处理。
                        {
                            Inputs_defined casualtokey = Inputs_defined.Null;
                            if (A_list.Contains(H3_list[y]))
                            {
                                casualtokey = Inputs_defined.Attack;
                            }
                            if (C_list.Contains(H3_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire2;
                            }

                            State_Rate_Set State_Rate_Set =
                                new State_Rate_Set(
                                H3_list[y].StateKey,
                                H3_list[y].stateType,
                                H3_list[y].AT,
                                H3_list[y].ai_trigger_ranges,
                                true,
                                casualtokey, Inputs_defined.Null,
                                H3_list[y].SPLevel,
                                H3_list[y].skillEmergentLevel);

                            casualT0.Add(State_Rate_Set);
                        }
                    }
                    break;
                case 2:
                    break;
            }

            if (this.R != null)
                casualT0.Add(this.R.GetStateRateSet());
            if (this.D != null)
                casualT0.Add(this.D.GetStateRateSet());

            B_list[i].casual_to_state_Sets = casualT0.ToArray();
        }
        //////////////////////////////////

        for (int i = 0; i < C_list.Count; i++)
        {
            List<State_Rate_Set> casualT0 = new List<State_Rate_Set>();
            if (i == 0)
            {
                C_list[i].enterInput = Inputs_defined.Fire2;
                C_list[i].exitInput = Inputs_defined.Null;
            }
            else
            {
                C_list[i].enterInput = Inputs_defined.Null;
                C_list[i].exitInput = Inputs_defined.Null;
            }

            if (i + 1 < C_list.Count)
            {
                State_Rate_Set State_Rate_Set =
                    new State_Rate_Set(
                    C_list[i + 1].StateKey,
                    C_list[i + 1].stateType,
                    C_list[i + 1].AT,
                    C_list[i + 1].ai_trigger_ranges,
                    true,Inputs_defined.Fire2, Inputs_defined.Null, 
                    C_list[i + 1].SPLevel,
                    C_list[i + 1].skillEmergentLevel);
                casualT0.Add(State_Rate_Set);
            }

            switch (i)
            {
                case 0://查H2
                    for (int y = 0; y < H2_list.Count; y++)
                    {
                        if (!C_list.Contains(H2_list[y]))//每个横行由上面的代码处理。
                        {
                            Inputs_defined casualtokey = Inputs_defined.Null;
                            if (A_list.Contains(H2_list[y]))
                            {
                                casualtokey = Inputs_defined.Attack;
                            }
                            if (B_list.Contains(H2_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire1;
                            }

                            State_Rate_Set State_Rate_Set =
                                new State_Rate_Set(
                                H2_list[y].StateKey,
                                H2_list[y].stateType,
                                H2_list[y].AT,
                                H2_list[y].ai_trigger_ranges,
                                true,
                                casualtokey, Inputs_defined.Null,
                                H2_list[y].SPLevel,
                                H2_list[y].skillEmergentLevel);

                            casualT0.Add(State_Rate_Set);
                        }
                    }
                    break;
                case 1://查H3
                    for (int y = 0; y < H3_list.Count; y++)
                    {
                        if (!C_list.Contains(H3_list[y]))//每个横行由上面的代码处理。
                        {
                            Inputs_defined casualtokey = Inputs_defined.Null;
                            if (A_list.Contains(H3_list[y]))
                            {
                                casualtokey = Inputs_defined.Attack;
                            }
                            if (B_list.Contains(H3_list[y]))
                            {
                                casualtokey = Inputs_defined.Fire1;
                            }

                            State_Rate_Set State_Rate_Set =
                                new State_Rate_Set(
                                H3_list[y].StateKey,
                                H3_list[y].stateType,
                                H3_list[y].AT,
                                H3_list[y].ai_trigger_ranges,
                                true,
                                casualtokey, Inputs_defined.Null,
                                H3_list[y].SPLevel,
                                H3_list[y].skillEmergentLevel);
                            casualT0.Add(State_Rate_Set);
                        }
                    }
                    break;
                case 2:
                    break;
            }

            if (this.R != null)
                casualT0.Add(this.R.GetStateRateSet());
            if (this.D != null)
                casualT0.Add(this.D.GetStateRateSet());

            C_list[i].casual_to_state_Sets = casualT0.ToArray();
        }

        List<State_Rate_Set> chuanEndCasualT0 = new List<State_Rate_Set>();
        if (A1 != null)
            chuanEndCasualT0.Add(A1.GetStateRateSet());
        if (B1 != null)
            chuanEndCasualT0.Add(B1.GetStateRateSet());
        if (C1 != null)
            chuanEndCasualT0.Add(C1.GetStateRateSet());
        if (D != null)
            chuanEndCasualT0.Add(D.GetStateRateSet());

        if (this.R != null)//意思就是说，机动类技能不再能取消迁移至机动类技能
        {
            this.R.casual_to_state_Sets = chuanEndCasualT0.ToArray();
            chuanEndCasualT0.Add(R.GetStateRateSet());
        }

        if (A3 != null)
        {
            A3.casual_to_state_Sets = chuanEndCasualT0.ToArray();
        }
        if (B3 != null)
        {
            B3.casual_to_state_Sets = chuanEndCasualT0.ToArray();
        }
        if (C3 != null)
        {
            C3.casual_to_state_Sets = chuanEndCasualT0.ToArray();
        }


    }

    //FormFightingSetsByNineAndTwo(string type,NineAndTwo nineAndTwo, passiveSkillConfigs passiveSkillConfigs, int AI_level) -->
    // 1.sortNineAndTwo(passiveSkillConfigs):整理三连击的连续关系。根据数据库配置好相应技能的属性。
    // 2.GenerateBeheviourSets():正式配置各State_Transition_Set，并且适配好所有技能组的force和casual迁移。
    // 和以前的脚本读取方式相比，不再需要sortList函数(当时很多努力白费了呀。。)原因是现在几个技能都是按次序加入列表，保证了基础状态和技能状态的顺序是有调理的，不再需要整理
    // 包括连击排序那方面。
    public IDictionary<string, State_Transition_Set> GenerateBeheviourSets(int level)// level : 1~100
    {
        if (level <= 0)
            level = 1;

        IDictionary<string, State_Transition_Set> state_Transition_Dictionary = new Dictionary<string, State_Transition_Set>();
        this.StateTransitionSetList = new List<State_Transition_Set>();

        State_Transition_Set Empty = new State_Transition_Set("Empty",
                                                              stateType.NONE,
                                                              0,
                                                              null,
                                                              new State_Rate_Set[0], 
                                                              new string[0], 
                                                              Inputs_defined.Null, Inputs_defined.Null,
                                                              0,
                                                              0,
                                                              0);

        State_Transition_Set Victory = new State_Transition_Set("Victory",
                                                                stateType.NONE,
                                                                0,
                                                                null,
                                                                new State_Rate_Set[0],
                                                                new string[0],
                                                                Inputs_defined.Null, Inputs_defined.Null,
                                                                0,
                                                                0,
                                                                0);

        State_Transition_Set Death = new State_Transition_Set("Death",
                                                              stateType.NONE,
                                                              0,
                                                              null,
                                                              new State_Rate_Set[0],
                                                              new string[0],
                                                              Inputs_defined.Null, Inputs_defined.Null, 0,
                                                              0,
                                                              0);

        State_Transition_Set Defend = new State_Transition_Set("Defend",
                                                               stateType.Def,
                                                               0,
                                                                null,
                                                              (this.R != null)? new State_Rate_Set[1]{this.R.GetStateRateSet()}:new State_Rate_Set[0],
                                                               null,
                                                               Inputs_defined.Defend, Inputs_defined.Defend_Cancel, 0,
                                                               0,
                                                               0);
        
        State_Transition_Set Move = new State_Transition_Set("Move_normal",
                                                             stateType.NONE,
                                                             0,
                                                             null,
                                                             new State_Rate_Set[0], 
                                                             null,
                                                             Inputs_defined.Null, Inputs_defined.Null, 0,
                                                             0,
                                                             0);
                       
        State_Transition_Set Hit = new State_Transition_Set("Hit",
                                                            stateType.Hit,
                                                            0,
                                                            null,
                                                            new State_Rate_Set[0], 
                                                            null,
                                                            Inputs_defined.Null, Inputs_defined.Null, 0,
                                                            0,
                                                            0);
                                                            
        StateTransitionSetList.Add(Empty);
        StateTransitionSetList.Add(Victory);
        StateTransitionSetList.Add(Death);
        StateTransitionSetList.Add(Hit);
        
        //string[] regularforceTOSets = { "Hit", "KnockOff"};

        List<State_Rate_Set> GetUpCasualTransitions = new List<State_Rate_Set>();

        if (this.D != null)
        {
            //Defend.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(Defend);//这里的逻辑是这样：如果在sortNineAndTwo执行后，this.D不是null，那说明角色有防御状态，而防御状态是固定的。
            GetUpCasualTransitions.Add(Defend.GetStateRateSet());
        }
        if (this.M != null)
        {
            //下面这些就是怕数据库里九宫格里的M记载有错。
            //M.forced_to_state_nums = regularforceTOSets;
            M.SPLevel = -1;
            M.casual_to_state_Sets = null;
            M.ai_trigger_ranges = null;
            StateTransitionSetList.Add(M);
        }
        else
            StateTransitionSetList.Add(Move);// 这个地方是说，要么你自定义移动类状态，要么加默认移动状态。因为移动状态其实可能根据角色被动而不同。

        if (this.R != null)
        {
            //R.forced_to_state_nums = regularforceTOSets;
            this.R.stateType = stateType.AC;
            StateTransitionSetList.Add(R);//这个是只能根据角色被动来。
            GetUpCasualTransitions.Add(R.GetStateRateSet());
        }
                    
        if(this.A1 != null)
        {
            //A1.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(A1);
            GetUpCasualTransitions.Add(A1.GetStateRateSet());
        }
        if (this.A2 != null)
        {
            //A2.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.A2);
        }            
        if (this.A3 != null)
        {
            //A3.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.A3);
        }
        if (this.B1 != null)
        {
            //B1.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.B1);
            GetUpCasualTransitions.Add(B1.GetStateRateSet());
        }            
        if (this.B2 != null)
        {
            //B2.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.B2);
        }
        if (this.B3 != null)
        {
            //B3.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.B3);
        }
        if (this.C1 != null)
        {
            //C1.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.C1);
            GetUpCasualTransitions.Add(C1.GetStateRateSet());
        }                    
        if (this.C2 != null)
        {
            //C2.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.C2);
        }            
        if (this.C3 != null)
        {
            //C3.forced_to_state_nums = regularforceTOSets;
            StateTransitionSetList.Add(this.C3);
        }
        
       State_Transition_Set getUp = new State_Transition_Set("getUp",
                                                    stateType.getUp,
                                                    0,
                                                    null,
                                                    GetUpCasualTransitions.ToArray(),
                                                    null,
                                                    Inputs_defined.Any, Inputs_defined.Null, 0,
                                                    0,
                                                    0);
        StateTransitionSetList.Add(getUp);

        State_Transition_Set KnockOff = new State_Transition_Set("KnockOff",
                                                                 stateType.KnockOff,
                                                                 0,
                                                                 null,
                                                                 new State_Rate_Set[]{ getUp.GetStateRateSet() },
                                                                 null,
                                                                 Inputs_defined.Null, Inputs_defined.Null,
                                                                 0,
                                                                 0,
                                                                 0);
        StateTransitionSetList.Add(KnockOff);

        //从下面这个地方可以看到我们需要在sort阶段把RMD全部准备好，而且必须是要么为null要么是一个完整STS信息。

        /////////////////////
        foreach (State_Transition_Set _State_Transition_Set in StateTransitionSetList)
        {
            if (_State_Transition_Set.StateKey != null 
                &&
                !state_Transition_Dictionary.ContainsKey(_State_Transition_Set.StateKey))//&& _States_Dictionary.ifContainsKey(_State_Transition_Set.StateKey)) //我们要研究以下这第二个条件                
            {
                List<State_Rate_Set> new_casual_to = new List<State_Rate_Set>();
                if (_State_Transition_Set.casual_to_state_Sets == null)
                {
                    _State_Transition_Set.casual_to_state_Sets = new_casual_to.ToArray();
                }
                foreach (State_Rate_Set _State_Rate_Set in _State_Transition_Set.casual_to_state_Sets)
                {
                    new_casual_to.Add(_State_Rate_Set);
                }
                state_Transition_Dictionary.Add(
                    new KeyValuePair<string, State_Transition_Set>(
                        _State_Transition_Set.StateKey,
                        _State_Transition_Set
                    )
                );
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

    //下面的环节纯粹是针对SkillPrintOut的一些处理
    public IDictionary<int, State_Transition_Set> GetAttackChuan()
    {
        IDictionary<int, State_Transition_Set> attack_chuan = new Dictionary<int, State_Transition_Set>
        {
            { 1, A1 },
            { 2, A2 },
            { 3, A3 }
        };
        return attack_chuan;
    }
    public IDictionary<int, State_Transition_Set> GetFire1Chuan()
    {
        IDictionary<int, State_Transition_Set> B_chuan = new Dictionary<int, State_Transition_Set>
        {
            { 1, B1 },
            { 2, B2 },
            { 3, B3 }
        };
        return B_chuan;
    }
    public IDictionary<int, State_Transition_Set> GetFire2Chuan()
    {
        IDictionary<int, State_Transition_Set> C_chuan = new Dictionary<int, State_Transition_Set>
        {
            { 1, C1 },
            { 2, C2 },
            { 3, C3 }
        };
        return C_chuan;
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
}
