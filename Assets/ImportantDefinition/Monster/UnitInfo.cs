
[System.Serializable]
public class UnitInfo
{
    public string id;
    public string r_id;
    public SkillSet set = new SkillSet();
    
    public UnitInfo Clone()
    {
        return (UnitInfo)MemberwiseClone();
    }

    public UnitInfo DeepCopy()
    {
        UnitInfo Copy = this.Clone();
        Copy.set = Copy.set.DeepCopy();
        return Copy;
    }

    public UnitInfo()
    {
    }

    public UnitInfo(string localID, string ResourceID,SkillSet _NineAndTwo)
    {
        id = localID;
        this.r_id = ResourceID;
        this.set = _NineAndTwo;
    }
}