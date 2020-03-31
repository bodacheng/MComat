using Skill;

[System.Serializable]
public partial class NineAndTwo {

    public string A1skillid, A2skillid, A3skillid;
    public string B1skillid, B2skillid, B3skillid;
    public string C1skillid, C2skillid, C3skillid;
    
    public int A1level = 0, A2level = 0, A3level = 0, B1level = 0, B2level = 0, B3level = 0, C1level = 0, C2level = 0, C3level = 0;

    public bool canDefend;
    public MoveType moveType;
    public RushType rushType;

    SkillConfig AConfig1, AConfig2, AConfig3, BConfig1, BConfig2, BConfig3, CConfig1, CConfig2, CConfig3;
    SkillConfig DConfig, MConfig, RConfig;

    public NineAndTwo()
    {
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
        
        A1level = 0; 
        A2level = 0;
        A3level = 0;
        B1level = 0;
        B2level = 0;
        B3level = 0;
        C1level = 0;
        C2level = 0; 
        C3level = 0;
    }
    
    public NineAndTwo(MoveType moveType,bool canDefend, RushType rushType)
    {
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
    
    NineAndTwo Clone()
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
}
