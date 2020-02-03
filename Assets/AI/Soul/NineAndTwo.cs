using System.Collections.Generic;
using UnityEngine;
using System;

// 这个模块也将扮演数据库和AI模块接口的作用。
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

    SkillConfig AConfig1, AConfig2, AConfig3, BConfig1, BConfig2, BConfig3, CConfig1, CConfig2, CConfig3;
    SkillConfig DConfig, MConfig, RConfig;//这三个其实对应了monster表里定义的三个被动技能
    Behavior_Transition_Set A1, A2, A3, B1, B2, B3, C1, C2, C3, D, M, R;
    List<Behavior_Transition_Set> StateTransitionSetList;//这个的作用是发生在StateDictionary的生成阶段。见AIStateRunner之FormFightingSetsByNineAndTwo

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

        moveType = MoveType.Test;
        canDefend = false;
        rushType = RushType.None;

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

    public List<Behavior_Transition_Set> ReturnSTSlist()
    {
        return StateTransitionSetList;
    }

    SkillConfig FixConfigByReference(string skillid)
    {
        if (skillid == null)
            return null;
        SkillConfigTable.Instance.SkillConfigDicForReference.TryGetValue(skillid, out SkillConfig referenceStandardSkillConfig);
        return referenceStandardSkillConfig;
    }

    Behavior_Transition_Set FromConfigToSTS(SkillConfig _SkillConfig)
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

        Behavior_Transition_Set STS = null;
        if (_SkillConfig != null && _SkillConfig.REAL_NAME != null && _SkillConfig.REAL_NAME != "")
        {
            try
            {
                STS = new Behavior_Transition_Set(_SkillConfig.REAL_NAME,
                                               _SkillConfig.stateType,
                                               _SkillConfig.ATTACK_WEIGHT,
                                               _SkillConfig.ai_trigger_ranges,
                                                null,
                                                null,
                                               Inputs_defined.Null,
                                               Inputs_defined.Null,
                                               _SkillConfig.SP_LEVEL,
                                               _SkillConfig.RARITY_LEVEL);
                return STS;
            }
            catch (Exception e)
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

    List<Behavior_Transition_Set> chuanEndCasualT0 = new List<Behavior_Transition_Set>();
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

        DConfig = passiveSkillConfigs.DConfig;
        MConfig = passiveSkillConfigs.MConfig;
        RConfig = passiveSkillConfigs.RConfig;

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
        List<Behavior_Transition_Set> H1_list = new List<Behavior_Transition_Set>();
        List<Behavior_Transition_Set> H2_list = new List<Behavior_Transition_Set>();
        List<Behavior_Transition_Set> H3_list = new List<Behavior_Transition_Set>();

        List<Behavior_Transition_Set> A_list = new List<Behavior_Transition_Set>();
        List<Behavior_Transition_Set> B_list = new List<Behavior_Transition_Set>();
        List<Behavior_Transition_Set> C_list = new List<Behavior_Transition_Set>();

        if (A1 != null)
        {
            A_list.Add(A1);
            H1_list.Add(A1);
            A1.enterInput = Inputs_defined.Attack;
            A1.exitInput = Inputs_defined.Null;
        }
        if (A2 != null)
        {
            A_list.Add(A2);
            H2_list.Add(A2);
            A2.enterInput = Inputs_defined.Attack;
            A2.exitInput = Inputs_defined.Null;
        }
        if (A3 != null)
        {
            A_list.Add(A3);
            H3_list.Add(A3);
            A3.enterInput = Inputs_defined.Attack;
            A3.exitInput = Inputs_defined.Null;
        }

        if (B1 != null)
        {
            B_list.Add(B1);
            H1_list.Add(B1);
            B1.enterInput = Inputs_defined.Fire1;
            B1.exitInput = Inputs_defined.Null;
        }
        if (B2 != null)
        {
            B_list.Add(B2);
            H2_list.Add(B2);
            B2.enterInput = Inputs_defined.Fire1;
            B2.exitInput = Inputs_defined.Null;
        }
        if (B3 != null)
        {
            B_list.Add(B3);
            H3_list.Add(B3);
            B3.enterInput = Inputs_defined.Fire1;
            B3.exitInput = Inputs_defined.Null;
        }

        if (C1 != null)
        {
            C_list.Add(C1);
            H1_list.Add(C1);
            C1.enterInput = Inputs_defined.Fire2;
            C1.exitInput = Inputs_defined.Null;
        }
        if (C2 != null)
        {
            C_list.Add(C2);
            H2_list.Add(C2);
            C2.enterInput = Inputs_defined.Fire2;
            C2.exitInput = Inputs_defined.Null;
        }
        if (C3 != null)
        {
            C_list.Add(C3);
            H3_list.Add(C3);
            C3.enterInput = Inputs_defined.Fire2;
            C3.exitInput = Inputs_defined.Null;
        }

        for (int i = 0; i < A_list.Count; i++)
        {
            List<Behavior_Transition_Set> casualT0 = new List<Behavior_Transition_Set>();
            switch (i)
            {
                case 0://查H2
                    for (int y = 0; y < H2_list.Count; y++)
                    {
                        casualT0.Add(H2_list[y]);
                    }
                    break;
                case 1://查H3
                    for (int y = 0; y < H3_list.Count; y++)
                    {
                        casualT0.Add(H3_list[y]);
                    }
                    break;
                case 2://查H1
                    for (int y = 0; y < H1_list.Count; y++)
                    {
                        casualT0.Add(H1_list[y]);
                    }
                    break;
            }
            if (R != null)
            {
                casualT0.Add(R);
            }
            if (D != null)
            {
                casualT0.Add(D);
            }
            A_list[i].Casual_To_Behaviours = casualT0.ToArray();
        }

        //////////////
        for (int i = 0; i < B_list.Count; i++)
        {
            List<Behavior_Transition_Set> casualT0 = new List<Behavior_Transition_Set>();
            switch (i)
            {
                case 0://查H2
                    for (int y = 0; y < H2_list.Count; y++)
                    {
                        casualT0.Add(H2_list[y]);
                    }
                    break;
                case 1://查H3
                    for (int y = 0; y < H3_list.Count; y++)
                    {
                        casualT0.Add(H3_list[y]);
                    }
                    break;
                case 2://查H1
                    for (int y = 0; y < H1_list.Count; y++)
                    {
                        casualT0.Add(H1_list[y]);
                    }
                    break;
            }
            if (R != null)
            {
                casualT0.Add(R);
            }
            if (D != null)
            {
                casualT0.Add(D);
            }
            B_list[i].Casual_To_Behaviours = casualT0.ToArray();
        }
        //////////////////////////////////

        for (int i = 0; i < C_list.Count; i++)
        {
            List<Behavior_Transition_Set> casualT0 = new List<Behavior_Transition_Set>();
            switch (i)
            {
                case 0://查H2
                    for (int y = 0; y < H2_list.Count; y++)
                    {
                        casualT0.Add(H2_list[y]);
                    }
                    break;
                case 1://查H3
                    for (int y = 0; y < H3_list.Count; y++)
                    {
                        casualT0.Add(H3_list[y]);
                    }
                    break;
                case 2://查H1
                    for (int y = 0; y < H1_list.Count; y++)
                    {
                        casualT0.Add(H1_list[y]);
                    }
                    break;
            }
            if (R != null)
            {
                casualT0.Add(R);
            }
            if (D != null)
            {
                casualT0.Add(D);
            }
            C_list[i].Casual_To_Behaviours = casualT0.ToArray();
        }

        chuanEndCasualT0.Clear();

        if (A1 != null)
            chuanEndCasualT0.Add(A1);
        if (B1 != null)
            chuanEndCasualT0.Add(B1);
        if (C1 != null)
            chuanEndCasualT0.Add(C1);
        if (D != null)
            chuanEndCasualT0.Add(D);
        if (R != null)
            chuanEndCasualT0.Add(R);
        
        if (D != null)
            D.Casual_To_Behaviours = chuanEndCasualT0.ToArray();
        if (R != null)
            R.Casual_To_Behaviours = chuanEndCasualT0.ToArray();
    }

    //FormFightingSetsByNineAndTwo(string type,NineAndTwo nineAndTwo, passiveSkillConfigs passiveSkillConfigs, int AI_level) -->
    // 1.sortNineAndTwo(passiveSkillConfigs):整理三连击的连续关系。根据数据库配置好相应技能的属性。
    // 2.GenerateBeheviourSets():正式配置各State_Transition_Set，并且适配好所有技能组的force和casual迁移。
    public IDictionary<string, Behavior_Transition_Set> GenerateBeheviourSets()
    {
        IDictionary<string, Behavior_Transition_Set> state_Transition_Dictionary = new Dictionary<string, Behavior_Transition_Set>();
        StateTransitionSetList = new List<Behavior_Transition_Set>();

        Behavior_Transition_Set Empty = new Behavior_Transition_Set("Empty",
                                                              BehaviorType.NONE,
                                                              0,
                                                              null,
                                                              new Behavior_Transition_Set[0], 
                                                              new string[0], 
                                                              Inputs_defined.Null, Inputs_defined.Null,
                                                              0,
                                                              0);

        Behavior_Transition_Set Victory = new Behavior_Transition_Set("Victory",
                                                                BehaviorType.NONE,
                                                                0,
                                                                null,
                                                                new Behavior_Transition_Set[0],
                                                                new string[0],
                                                                Inputs_defined.Null, Inputs_defined.Null,
                                                                0,
                                                                0);

        Behavior_Transition_Set Death = new Behavior_Transition_Set("Death",
                                                              BehaviorType.NONE,
                                                              0,
                                                              null,
                                                              new Behavior_Transition_Set[0],
                                                              new string[0],
                                                              Inputs_defined.Null, Inputs_defined.Null,
                                                              0,
                                                              0);

        Behavior_Transition_Set Defend = new Behavior_Transition_Set("Defend",
                                                               BehaviorType.Def,
                                                               0,
                                                                null,
                                                              chuanEndCasualT0.ToArray(),
                                                               null,
                                                               Inputs_defined.Defend, Inputs_defined.Defend_Cancel,
                                                               0,
                                                               0);
        
        Behavior_Transition_Set Move = new Behavior_Transition_Set("Move_normal",
                                                             BehaviorType.NONE,
                                                             0,
                                                             null,
                                                             chuanEndCasualT0.ToArray(),
                                                             null,
                                                             Inputs_defined.Null, Inputs_defined.Null,
                                                             0,
                                                             0);
                       
        Behavior_Transition_Set Hit = new Behavior_Transition_Set("Hit",
                                                            BehaviorType.Hit,
                                                            0,
                                                            null,
                                                            chuanEndCasualT0.ToArray(),
                                                            null,
                                                            Inputs_defined.Null, Inputs_defined.Null,
                                                            0,
                                                            0);
                                                            
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
            StateTransitionSetList.Add(this.A2);
        }            
        if (this.A3 != null)
        {
            StateTransitionSetList.Add(this.A3);
        }
        if (this.B1 != null)
        {
            StateTransitionSetList.Add(this.B1);
        }            
        if (this.B2 != null)
        {
            StateTransitionSetList.Add(this.B2);
        }
        if (this.B3 != null)
        {
            StateTransitionSetList.Add(this.B3);
        }
        if (this.C1 != null)
        {
            StateTransitionSetList.Add(this.C1);
        }                    
        if (this.C2 != null)
        {
            StateTransitionSetList.Add(this.C2);
        }            
        if (this.C3 != null)
        {
            StateTransitionSetList.Add(this.C3);
        }
        
        if (this.M != null)
        {
            //下面这些就是怕数据库里九宫格里的M记载有错。
            M.SPLevel = -1;
            M.Casual_To_Behaviours = chuanEndCasualT0.ToArray();
            M.AI_trigger_ranges = null;
            StateTransitionSetList.Add(M);
        }
        else
            StateTransitionSetList.Add(Move);// 这个地方是说，要么你自定义移动类状态，要么加默认移动状态。因为移动状态其实可能根据角色被动而不同。
        
        Behavior_Transition_Set getUp = new Behavior_Transition_Set("getUp",
                                                                    BehaviorType.GetUp,
                                                                    0,
                                                                    null,
                                                                    chuanEndCasualT0.ToArray(),
                                                                    null,
                                                                    Inputs_defined.Any, Inputs_defined.Null,
                                                                    0,
                                                                    0);
        StateTransitionSetList.Add(getUp);

        Behavior_Transition_Set KnockOff = new Behavior_Transition_Set( "KnockOff",
                                                                        BehaviorType.KnockOff,
                                                                        0,
                                                                        null,
                                                                        new Behavior_Transition_Set[]{ getUp },
                                                                        null,
                                                                        Inputs_defined.Null, Inputs_defined.Null,
                                                                        0,
                                                                        0);
        StateTransitionSetList.Add(KnockOff);

        //从下面这个地方可以看到我们需要在sort阶段把RMD全部准备好，而且必须是要么为null要么是一个完整STS信息。

        /////////////////////
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
