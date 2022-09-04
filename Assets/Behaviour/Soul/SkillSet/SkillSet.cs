using Skill;
using System;

[Serializable]
public partial class SkillSet {

    public string a1, a2, a3;
    public string b1, b2, b3;
    public string c1, c2, c3;
    
    private bool Def;
    private MoveType MoveType;
    private RushType RushType;

    public bool GetD()
    {
        return Def;
    }

    public MoveType GetM()
    {
        return MoveType;
    }

    public RushType GetR()
    {
        return RushType;
    }
    
    public SkillSet()
    {
        a1 = null; a2 = null; a3 = null;
        b1 = null; b2 = null; b3 = null;
        c1 = null; c2 = null; c3 = null;
        
        MoveType = MoveType.Move_normal;
        Def = false;
        RushType = RushType.Rush;
    }

    public SkillSet(MoveType moveType, bool canDefend, RushType rushType)
    {
        a1 = null; a2 = null; a3 = null;
        b1 = null; b2 = null; b3 = null;
        c1 = null; c2 = null; c3 = null;

        this.MoveType = moveType;
        this.Def = canDefend;
        this.RushType = rushType;
    }

    public SkillSet DeepCopy()
    {
        return (SkillSet)MemberwiseClone();
    }

    public void SetPassive(bool _Def, MoveType _MoveType, RushType _RushType)
    {
        Def = _Def;
        MoveType = _MoveType;
        RushType = _RushType;
    }
}
