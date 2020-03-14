using Api.Dto.Model;

// CharacterDataInfo 的各个成员在CharacterDataInfo做不同作用时候可能能用上的成员是不同的。
// 这个类本身可以看作是AccountCharacterInfo的“实际体”，它也可以序列化，也可以保存。
// GetMonsterOfPlayerDetailModel 更侧重玩家信息，以及与远程的交互，而这个类更侧重在游戏里代表一个角色系统性信息。
[System.Serializable]
public class CharacterDataInfo
{
    public string monsterOfPlayerId;
    public string ResourceID;
    public int level = 1;
    public int HP = 500; //通常来说玩家的角色HP和角色level应该有一个清晰的对应关系，而关卡敌人的HP应该是可以自由设置，这个HP必然不会出现在数据库的任何部位。    
    public NineAndTwo _NineAndTwo;//

    public CharacterDataInfo Clone()
    {
        return (CharacterDataInfo)MemberwiseClone();
    }

    public CharacterDataInfo DeepCopy()
    {
        CharacterDataInfo Copy = this.Clone();
        Copy._NineAndTwo = Copy._NineAndTwo.DeepCopy();
        return Copy;
    }

    public CharacterDataInfo()
    {
    }

    public CharacterDataInfo(string localID, string ResourceID,NineAndTwo _NineAndTwo)
    {
        monsterOfPlayerId = localID;
        this.ResourceID = ResourceID;
        this._NineAndTwo = _NineAndTwo;
    }

    public void Dissolve()
    {
        monsterOfPlayerId = (-1).ToString();
        ResourceID = null;
        _NineAndTwo = null;
    }

    public GetMonsterOfPlayerDetailModel GetCharacterDataInfoJson()
    {
        GetMonsterOfPlayerDetailModel characterDataInfoJson = new GetMonsterOfPlayerDetailModel
        {
            playerId = "1",
            monsterId = ResourceID,
            monsterOfPlayerId = monsterOfPlayerId,
            //characterDataInfoJson.experience = 
            level = level,
        };
        return characterDataInfoJson;
    }
}