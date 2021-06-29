using Skill;
using System;

[Serializable]
public partial class NineAndTwo {

    public string a1, a2, a3;
    public string b1, b2, b3;
    public string c1, c2, c3;

    public int A1lv = 0, A2lv = 0, A3lv = 0, B1lv = 0, B2lv = 0, B3lv = 0, C1lv = 0, C2lv = 0, C3lv = 0;

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

    public NineAndTwo()
    {
        a1 = null; a2 = null; a3 = null;
        b1 = null; b2 = null; b3 = null;
        c1 = null; c2 = null; c3 = null;

        MoveType = MoveType.Move_normal;
        Def = false;
        RushType = RushType.Rush;

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

    public NineAndTwo(MoveType moveType, bool canDefend, RushType rushType)
    {
        a1 = null; a2 = null; a3 = null;
        b1 = null; b2 = null; b3 = null;
        c1 = null; c2 = null; c3 = null;

        this.MoveType = moveType;
        this.Def = canDefend;
        this.RushType = rushType;
    }

    public NineAndTwo DeepCopy()
    {
        return (NineAndTwo)MemberwiseClone();
    }

    public void SetPassive(bool _Def, MoveType _MoveType, RushType _RushType)
    {
        Def = _Def;
        MoveType = _MoveType;
        RushType = _RushType;
    }
}
