
[System.Serializable]
public class CharDataInfo
{
    public string id;
    public string r_id;
    public SkillSet set = new SkillSet();
    
    public CharDataInfo Clone()
    {
        return (CharDataInfo)MemberwiseClone();
    }

    public CharDataInfo DeepCopy()
    {
        CharDataInfo Copy = this.Clone();
        Copy.set = Copy.set.DeepCopy();
        return Copy;
    }

    public CharDataInfo()
    {
    }

    public CharDataInfo(string localID, string ResourceID,SkillSet _NineAndTwo)
    {
        id = localID;
        this.r_id = ResourceID;
        this.set = _NineAndTwo;
    }
}