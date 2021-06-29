using Skill;
using System;

[Serializable]
public partial class NineAndTwo {

    public string a1, a2, a3;
    public string b1, b2, b3;
    public string c1, c2, c3;
    
    public int A1lv = 0, A2lv = 0, A3lv = 0, B1lv = 0, B2lv = 0, B3lv = 0, C1lv = 0, C2lv = 0, C3lv = 0;
    
    public bool canDefend;
    public MoveType moveType;
    public RushType rushType;
    
    SkillConfig AConfig1, AConfig2, AConfig3, BConfig1, BConfig2, BConfig3, CConfig1, CConfig2, CConfig3;
    
    public NineAndTwo()
    {
        a1 = null; a2 = null; a3 = null;
        b1 = null; b2 = null; b3 = null;
        c1 = null; c2 = null; c3 = null;
        
        moveType = MoveType.Move_normal;
        canDefend = false;
        rushType = RushType.Rush;
        
        AConfig1 = new SkillConfig();
        AConfig2 = new SkillConfig();
        AConfig3 = new SkillConfig();
        BConfig1 = new SkillConfig();
        BConfig2 = new SkillConfig();
        BConfig3 = new SkillConfig();
        CConfig1 = new SkillConfig();
        CConfig2 = new SkillConfig();
        CConfig3 = new SkillConfig();
        
        A1lv = 0; 
        A2lv = 0;
        A3lv = 0;
        B1lv = 0;
        B2lv = 0;
        B3lv = 0;
        C1lv = 0;
        C2lv = 0; 
        C3lv = 0;
    }
    
    public NineAndTwo(MoveType moveType,bool canDefend, RushType rushType)
    {
        a1 = null; a2 = null; a3 = null;
        b1 = null; b2 = null; b3 = null;
        c1 = null; c2 = null; c3 = null;

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
        
        return Copy;
    }
}
