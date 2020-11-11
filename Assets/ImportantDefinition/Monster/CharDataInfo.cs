using Api.Dto.Model;
// 这个类本身可以看作是GetMonsterOfPlayerDetailModel的“实际体”，它也可以序列化，也可以保存。
// GetMonsterOfPlayerDetailModel 更侧重玩家信息，以及与远程的交互，而CharDataInfo更侧重在游戏里代表一个角色系统性信息。

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

    public MonsterOfPlayerDetailModel GetCharacterDataInfoJson()
    {
        MonsterOfPlayerDetailModel characterDataInfoJson = new MonsterOfPlayerDetailModel
        {
            playerId = "1",
            monsterId = ResourceID,
            monsterOfPlayerId = monsterOfPlayerId,
        };
        return characterDataInfoJson;
    }
}