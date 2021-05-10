
[System.Serializable]
public class CharDataInfo
{
    public string monsterOfPlayerId;
    public string ResourceID;
    public NineAndTwo _NineAndTwo = new NineAndTwo();
    
    public CharDataInfo Clone()
    {
        return (CharDataInfo)MemberwiseClone();
    }

    public CharDataInfo DeepCopy()
    {
        CharDataInfo Copy = this.Clone();
        Copy._NineAndTwo = Copy._NineAndTwo.DeepCopy();
        return Copy;
    }

    public CharDataInfo()
    {
    }

    public CharDataInfo(string localID, string ResourceID,NineAndTwo _NineAndTwo)
    {
        monsterOfPlayerId = localID;
        this.ResourceID = ResourceID;
        this._NineAndTwo = _NineAndTwo;
    }
}